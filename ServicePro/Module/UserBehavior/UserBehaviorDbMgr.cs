using MySqlConnector;
using ServicePro.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.UserBehavior
{
    class UserBehaviorDbMgr:DbManager
    {
        public static bool InsertBehavior(string mail , string behavior)
        { 
            bool hasInsert = false;  
            try
            {
                conn.Open();
                string sql = string.Format("insert into userBehavior(mail, behavior, date) " +
                    "values('{0}','{1}','{2}')",
                    mail, behavior,DateTime.Now.ToString("y-M-d h:m:s"));
                Console.WriteLine(sql);
                //执行插入语句
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
                hasInsert = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UserBehaviorDbMgr] Insert userBehavior db fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return hasInsert;
        }

        public static List<UserBehavior> GetAllUserRigisterBehavior()
        {
            List<UserBehavior> userBehaviors = new List<UserBehavior>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from userBehavior where behavior = '{0}' ", "注册账号");

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string mail = reader[1].ToString();
                    string behavior = reader[2].ToString();
                    string data = reader[3].ToString();
                    UserBehavior userBehavior = new UserBehavior(mail, behavior, data);
                    userBehaviors.Add(userBehavior);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UserBehaviorDbMgr] Get UserRigisterBehavior fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return userBehaviors;
        }

        public static List<UserBehavior> GetUserBehaviorByMail(string userMail)
        {
            List<UserBehavior> userBehaviors = new List<UserBehavior>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from userBehavior where mail = '{0}' ", userMail);

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string mail = reader[1].ToString();
                    string behavior = reader[2].ToString();
                    string data = reader[3].ToString();
                    UserBehavior userBehavior = new UserBehavior(mail, behavior, data);
                    userBehaviors.Add(userBehavior);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UserBehaviorDbMgr] Get User Behavior fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return userBehaviors;
        }

    }
}
