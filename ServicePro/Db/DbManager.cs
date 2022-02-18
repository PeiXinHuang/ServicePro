using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Base.Db
{
    class DbManager
    {
        public string serverIp = "localhost"; // 服务器地址
        public string userId = "root"; // 用户Id
        public string password = "04285733Hpx@"; //用户密码
        public string databaseName = "poemapp"; // 数据库名称
        public string port = "3306"; // 端口号
        public string charSet = "utf8"; // 编码格式

        public static MySqlConnection conn; //数据库连接对象

        private static DbManager _instance = null;
        public static DbManager instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new DbManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 初始化数据库控制器
        /// </summary>
        public bool InitDatabaseMgr()
        {
            //实例化数据库连接对象
            conn = new MySqlConnection(
                "Server = " + serverIp + ";" +
                "User Id = " + userId + ";" +
                "Password = " + password + ";" +
                "Database = " + databaseName + ";" +
                "Port = " + port + ";" +
                "CharSet = " + charSet + ";"
                );
            bool result = TestConnectDatabase();
            return result;
        }

        public bool TestConnectDatabase()
        {
        
            bool result = true ;
            try
            {
                conn.Open(); //打开数据库select * from texttable;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("数据库连接失败" + e.Message.ToString());
                result = false;
            }
            finally
            {
                conn.Close(); //关闭数据库连接
            }
            return result;
        }
    }
}
