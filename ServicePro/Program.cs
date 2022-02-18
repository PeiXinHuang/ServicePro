using ServicePro.Base.Db;
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
        public static void Main(string[] args)
        {
            bool result = DbManager.instance.InitDatabaseMgr();
            if(result)
            {
                NetManager.StartLoop();
            }
        }
    }
}
