using MySqlConnector;
using ServicePro.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.User
{
    class UserDbMgr:DbManager
    {
        public static bool InsertUser(User user)
        {
            bool hasContainThisUser = false;
            bool hasInsertUser = false;
            try
            {
                conn.Open();
                string sql = string.Format("select * from user where mail = '{0}'", user.mail);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    hasContainThisUser = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UserDbMgr] Charge user is exit or not faile cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            if (hasContainThisUser)
            {
                Console.WriteLine("[UserDbMgr]" + user.mail + " has exit in user db, pass");
                return false;
            }

            try
            {
                conn.Open();

                string sql = string.Format("insert into user(mail, name, password) " +
                    "values('{0}','{1}','{2}')",
                    user.mail, user.name, user.password);
                Console.WriteLine(sql);
                //执行插入语句
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
                hasInsertUser = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UserDbMgr] Insert user db fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            return hasInsertUser;
        }

        public static User GetUserByUserMail(string mail)
        {
            List<User> users = new List<User>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from user where mail = '{0}'", mail);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();
                    user.mail = reader[0].ToString();
                    user.name = reader[1].ToString();
                    user.password = reader[2].ToString();
                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UserDbMgr] Get AdminUser fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            if (users.Count == 1)
            {
                return users[0];
            }
            else
            {
                return null;
            }
        }


        public static bool UpdateUserPassword(User user)
        {
            bool updateResult = false;
            bool hasContainThisUser = false;
            try
            {
                conn.Open();
                string sql = string.Format("select * from user where mail = '{0}'", user.mail);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    hasContainThisUser = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UserDbMgr] Charge user is exit or not faile cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            if (!hasContainThisUser)
            {
                Console.WriteLine("[UserDbMgr]" + user.mail + " not exit in user db, pass");
                return false;
            }

            try
            {
                conn.Open();

                //数据库更新语句
                string sql = string.Format(
                    "Update user Set password = '{0}' where mail = '{1}'",
                    user.password, user.mail);

                //执行更新语句
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();

                updateResult = true;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Update User Failed: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return updateResult;
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from user");
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();
                    user.mail = reader[0].ToString();
                    user.name = reader[1].ToString();
                    user.password = reader[2].ToString();
                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[UserDbMgr] Get all users fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            return users;
        }
    }
}
