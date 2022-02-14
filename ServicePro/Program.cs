using ServicePro.Base.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro
{
    class Program
    {
        /*
        // 服务端程序
        public static void Main(string[] args)
        {
            // 创建监听Socket
            Socket listenfd = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream, ProtocolType.Tcp);
            // 监听Socket绑定地址和端口
            IPAddress ipAdr = IPAddress.Parse("192.168.220.1");
            IPEndPoint ipEp = new IPEndPoint(ipAdr, 8888);
            listenfd.Bind(ipEp);

            // 监听Socket开始监听
            listenfd.Listen(0);

            Console.WriteLine("[服务器]启动成功");

            while (true)
            {

                // 监听Socket被连接,返回一个连接Socket用来处理客户端数据
                Socket connfd = listenfd.Accept();

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
        */
        public static void Main(string[] args)
        {
            NetManager.StartLoop();
        }
    }
}
