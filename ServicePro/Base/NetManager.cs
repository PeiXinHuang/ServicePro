using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Base
{
    class NetManager
    {

        private static string _ipAddress = "172.24.151.27";
		private static int _port = 22;

		//ping间隔
		public static long pingInterval = 30;

		public static Socket listenSocket;
		public static Dictionary<Socket, ClientState> clientStates = new Dictionary<Socket, ClientState>();
		public static List<Socket> selectSockets = new List<Socket>();
		public static void StartLoop()
		{
			//绑定本地ip地址和端口
			listenSocket = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);
			IPAddress ipAdr = IPAddress.Parse(_ipAddress);
			IPEndPoint ipEp = new IPEndPoint(ipAdr, _port);
			listenSocket.Bind(ipEp);

			//设置监听数为无限
			listenSocket.Listen(0);
			Console.WriteLine("[服务器]启动成功");

			//开始监听
			while (true)
			{
				ResetSelectSockets();  
				Socket.Select(selectSockets, null, null, 1000);
				//检查Select可读Socket
				for (int i = selectSockets.Count - 1; i >= 0; i--)
				{
					Socket s = selectSockets[i];
					if (s == listenSocket) // 可读Socket为服务端Socket
					{
						ReadListenSocket(s);
					}
					else // 可读Socket为客户端Socket
					{
						ReadClientSocket(s);
					}
				}
				//超时
				Timer();
			}
		}

		//重置Select，获取到所有的Socket
		private static void ResetSelectSockets()
		{
			selectSockets.Clear();
			selectSockets.Add(listenSocket);
			foreach (ClientState s in clientStates.Values)
			{
				selectSockets.Add(s.socket);
			}
		}

		//关闭连接
		public static void Close(ClientState state)
		{
			//消息分发
			MethodInfo mei = typeof(EventHandler).GetMethod("OnDisconnect");
			object[] ob = { state };
			mei.Invoke(null, ob);
			//关闭
			state.socket.Close();
			clientStates.Remove(state.socket);
		}


		private static void ReadListenSocket(Socket listenfd)
		{
			try
			{
				Socket clientfd = listenfd.Accept();
				Console.WriteLine("服务端接收到客户端" + clientfd.RemoteEndPoint.ToString());
				ClientState state = new ClientState();
				state.socket = clientfd;
				state.lastPingTime = GetTimeStamp();
				clientStates.Add(clientfd, state);
			}
			catch (SocketException ex)
			{
				Console.WriteLine("Accept fail" + ex.ToString());
			}
		}


		//读取Clientfd
		public static void ReadClientSocket(Socket clientfd)
		{
			ClientState state = clientStates[clientfd];
			ByteArray readBuff = state.readBuff;
			//接收
			int count = 0;
			//缓冲区不够，清除，若依旧不够，只能返回
			//缓冲区长度只有1024，单条协议超过缓冲区长度时会发生错误，根据需要调整长度
			if (readBuff.remain <= 0)
			{
				OnReceiveData(state);
				readBuff.MoveBytes();
			};
			if (readBuff.remain <= 0)
			{
				Console.WriteLine("Receive fail , maybe msg length > buff capacity");
				Close(state);
				return;
			}

			try
			{
				count = clientfd.Receive(readBuff.bytes, readBuff.writeIdx, readBuff.remain, 0);
			}
			catch (SocketException ex)
			{
				Console.WriteLine("Receive SocketException " + ex.ToString());
				Close(state);
				return;
			}
			//客户端关闭
			if (count <= 0)
			{
				Console.WriteLine("Socket Close " + clientfd.RemoteEndPoint.ToString());
				Close(state);
				return;
			}
			//消息处理
			readBuff.writeIdx += count;
			//处理二进制消息
			OnReceiveData(state);
			//移动缓冲区
			readBuff.CheckAndMoveBytes();
		}


		//数据处理
		public static void OnReceiveData(ClientState state)
		{
			ByteArray readBuff = state.readBuff;
			//消息长度
			if (readBuff.length <= 2)
			{
				return;
			}
			Int16 bodyLength = readBuff.ReadInt16();
			//消息体
			if (readBuff.length < bodyLength)
			{
				return;
			}
			//解析协议名
			int nameCount = 0;
			string protoName = MsgBase.DecodeName(readBuff.bytes, readBuff.readIdx, out nameCount);
			if (protoName == "")
			{
				Console.WriteLine("OnReceiveData MsgBase.DecodeName fail");
				Close(state);
				return;
			}
			readBuff.readIdx += nameCount;
			//解析协议体
			int bodyCount = bodyLength - nameCount;
			MsgBase msgBase = MsgBase.Decode(protoName, readBuff.bytes, readBuff.readIdx, bodyCount);
			readBuff.readIdx += bodyCount;
			readBuff.CheckAndMoveBytes();
			//分发消息
			MethodInfo mi = typeof(MsgHandler).GetMethod(protoName);
			object[] o = { state, msgBase };
			Console.WriteLine("Receive " + protoName);
			if (mi != null)
			{
				mi.Invoke(null, o);
			}
			else
			{
				Console.WriteLine("OnReceiveData Invoke fail " + protoName);
			}
			//继续读取消息
			if (readBuff.length > 2)
			{
				OnReceiveData(state);
			}
		}

		//发送
		public static void Send(ClientState cs, MsgBase msg)
		{
			//状态判断
			if (cs == null)
			{
				return;
			}
			if (!cs.socket.Connected)
			{
				return;
			}
			//数据编码
			byte[] nameBytes = MsgBase.EncodeName(msg);
			byte[] bodyBytes = MsgBase.Encode(msg);
			int len = nameBytes.Length + bodyBytes.Length;
			byte[] sendBytes = new byte[2 + len];
			//组装长度
			sendBytes[0] = (byte)(len % 256);
			sendBytes[1] = (byte)(len / 256);
			//组装名字
			Array.Copy(nameBytes, 0, sendBytes, 2, nameBytes.Length);
			//组装消息体
			Array.Copy(bodyBytes, 0, sendBytes, 2 + nameBytes.Length, bodyBytes.Length);
			//为简化代码，不设置回调
			try
			{
				cs.socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, null, null);
			}
			catch (SocketException ex)
			{
				Console.WriteLine("Socket Close on BeginSend" + ex.ToString());
			}
		}



		//定时器
		static void Timer()
		{
			//消息分发
			MethodInfo mei = typeof(EventHandler).GetMethod("OnTimer");
			object[] ob = { };
			mei.Invoke(null, ob);
		}

		//获取时间戳
		public static long GetTimeStamp()
		{
			TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return Convert.ToInt64(ts.TotalSeconds);
		}
	}
}
