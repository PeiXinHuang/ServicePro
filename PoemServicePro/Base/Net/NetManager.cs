using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PoemServicePro.Base.Net
{
    class NetManager
    {
        private static string _ipAddress = "172.24.151.27";
        private static int _port = 22;

		public static Socket listenSocket;

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
				// 监听Socket被连接,返回一个连接Socket用来处理客户端数据
				Socket connfd = listenSocket.Accept();

				byte[] readBuff = new byte[1024];

				// 连接Socket接受客户端数据
				int count = connfd.Receive(readBuff);

				string readStr = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);
				Console.WriteLine("[服务器接收]" + readStr);


				string sendStr = System.DateTime.Now.ToString();
				byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);

				// 连接Socket发送数据到客户端
				connfd.Send(sendBytes);
			}
		}
	}
}
