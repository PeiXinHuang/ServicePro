using ServicePro.Module.Poem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicePro.Net
{
    class PkgManager
    {
		public static string globalVersionFilePath = @"C:\Users\Administrator\Desktop\Resources\GlobalVersion.json";
		public static string poemsFolder = @"C:\Users\Administrator\Desktop\Resources\Poems";

		private static string _ipAddress = "172.24.151.27";
        private static int _port = 8000;
		private static Socket listenSocket;
		private static Thread sendPkgThread = new Thread(SendPkg);

		//checkRead列表
		private static List<Socket>  checkRead = new List<Socket>();

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
			Console.WriteLine("开放资源下载端口");
			sendPkgThread.Start();
			
		}

		private static void SendPkg()
        {
            while (true)
            {
				if (listenSocket.Poll(0, SelectMode.SelectRead))
				{
					ReadListenfd(listenSocket);
				}

				foreach (Socket s in checkRead)
				{
					if (s.Poll(0, SelectMode.SelectRead))
					{
						if (!ReadClientfd(s))
						{
							break;
						}
					}
				}

				
				Thread.Sleep(1);
			}
		}

		//读取Listenfd
		public static void ReadListenfd(Socket listenfd)
		{
			Socket clientfd = listenfd.Accept();
			checkRead.Add(clientfd);
		}

		public static bool ReadClientfd(Socket connfd)
        {

			string readStr = "";
			try
			{
				connfd = listenSocket.Accept();
				byte[] readBuff = new byte[1024];
				int count = connfd.Receive(readBuff);
				readStr = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);
				if(count == 0)
                {
					connfd.Close();
					checkRead.Remove(connfd);
					return false;
				}
				Console.WriteLine("[下载管理器接收]" + readStr);
			}
			catch (Exception ex)
			{
				Console.WriteLine("[PkgManager] recv error " + ex.ToString());
				return  false;
			}

			Console.WriteLine(readStr);
			if (readStr.Equals("DownloadVersionPkg") && connfd != null)
			{
				if (File.Exists(globalVersionFilePath))
				{
					string sendStr = File.ReadAllText(globalVersionFilePath);
					byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(sendStr);
					connfd.Send(sendBytes);
				}
				else
				{
					Console.WriteLine("版本文件不存在");
				}
			}
			else if (readStr.StartsWith("DownloadPoemsByAuthor") && connfd != null)
			{
				string[] readStrs = readStr.Split();
				if (readStrs.Length == 2)
				{
					string author = readStrs[1];
					if (File.Exists(poemsFolder + "/" + author + ".json"))
					{
						string sendStr = File.ReadAllText(poemsFolder + "/" + author + ".json");
						byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(sendStr);
						connfd.Send(sendBytes);
					}
					else
					{
						string poems = PoemMgr.GetPoemJsonByAuthor(author);
						byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(poems);
						connfd.Send(sendBytes);
					}
				}

			}
			return true;
		}
	}
}
