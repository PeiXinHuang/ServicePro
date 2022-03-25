using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.AdminUser
{
    class AdminUserMgr
    {
        public static string GetAllAdminUsersFromDb()
        {
            List<AdminUser> adminUsers = new List<AdminUser>();
            adminUsers = AdminUserDbMgr.GetAllAdminUsers();
            if (adminUsers.Count == 0)
            {
                Console.WriteLine("[AdminUserMgr]获取所有管理员用户信息失败");
                return "";
            }
            AdminUsersData adminUsersData = new AdminUsersData();
            adminUsersData.adminUsers = adminUsers.ToArray();
            adminUsersData.count = adminUsers.Count;

            string poemJsonStr = JsonConvert.SerializeObject(adminUsersData, Formatting.Indented);

            return poemJsonStr;
        }
    }

    class AdminUsersData
    {
        public AdminUser[] adminUsers;
        public int count = 0;
    }
}
