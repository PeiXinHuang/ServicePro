using MySqlConnector;
using ServicePro.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.AdminUser
{
    class AdminUserDbMgr:DbManager
    {
        public static AdminUser GetAdminUserByMail(string mail)
        {
            List<AdminUser> adminUsers = new List<AdminUser>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from adminuser where mail = '{0}'", mail) ;
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminUser admin = new AdminUser();
                    admin.mail = reader[0].ToString();
                    admin.name = reader[1].ToString();
                    admin.password = reader[2].ToString();
                    adminUsers.Add(admin);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[AdminbUserDbMgr] Get AdminUser fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            if(adminUsers.Count == 1)
            {
                return adminUsers[0];
            }
            else
            {
                return null;
            }
        }

        public static bool InsertAdminUser(AdminUser user)
        {
            bool hasContainThisUser = false;
            bool hasInsertUser = false;
            try
            {
                conn.Open();
                string sql = string.Format("select * from adminuser where mail = '{0}'", user.mail);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    hasContainThisUser = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[AdminbUserDbMgr] Charge user is exit or not faile cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            if (hasContainThisUser)
            {
                Console.WriteLine("[AdminbUserDbMgr]" + user.mail + " has exit in adminUser db, pass");
                return false;
            }

            try
            {
                conn.Open();

                string sql = string.Format("insert into adminUser(mail, name, password) " +
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
                Console.WriteLine("[AdminbUserDbMgr] Insert adminUser db fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            return hasInsertUser;
        }

        public static bool UpdateAdminUser(AdminUser user)
        {
            bool updateResult = false;
            bool hasContainThisUser = false;
            try
            {
                conn.Open();
                string sql = string.Format("select * from adminuser where mail = '{0}'", user.mail);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    hasContainThisUser = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[AdminbUserDbMgr] Charge adminUser is exit or not faile cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            if (!hasContainThisUser)
            {
                Console.WriteLine("[AdminbUserDbMgr]" + user.mail + " not exit in adminUser db, pass");
                return false;
            }

            try
            {
                conn.Open();

                //数据库更新语句
                string sql = string.Format(
                    "Update adminUser Set password = '{0}' where mail = '{1}'",
                    user.password, user.mail);

                //执行更新语句
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();

                updateResult = true;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("[AdminbUserDbMgr]Update adminUser Failed: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            return updateResult;
        }

        public static List<AdminUser> GetAllAdminUsers()
        {
            List<AdminUser> adminUsers = new List<AdminUser>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from AdminUser");
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminUser adminUser = new AdminUser();
                    adminUser.mail = reader[0].ToString();
                    adminUser.name = reader[1].ToString();
                    adminUser.password = reader[2].ToString();
                    adminUsers.Add(adminUser);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[AdminbUserDbMgr] Get all AdminUser fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return adminUsers;
        }
    }
}
