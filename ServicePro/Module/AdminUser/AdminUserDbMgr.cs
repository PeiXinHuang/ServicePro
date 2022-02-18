using MySqlConnector;
using ServicePro.App.Module.AdminUser;
using ServicePro.Base.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.App.Db
{
    class AdminUserDbMgr:DbManager
    {
        public AdminUser GetAdminUserByUsername(string username)
        {
            List<AdminUser> adminUsers = new List<AdminUser>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from adminuser where username = '{0}'", username);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AdminUser admin = new AdminUser();
                    admin.username = reader[1].ToString();
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
    }
}
