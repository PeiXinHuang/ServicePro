using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.User
{
    class UserMgr
    {
        public static string GetAllUsersFromDb()
        {
          
            List<User> users = new List<User>();
            users = UserDbMgr.GetAllUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("[UserMgr]获取所有用户信息失败");
                return "";
            }
            UsersData usersData = new UsersData();
            usersData.users = users.ToArray();
            usersData.count = users.Count;
            
            string poemJsonStr = JsonConvert.SerializeObject(usersData, Formatting.Indented);

            return poemJsonStr;
        }
    }

    class UsersData
    {
        public User[] users;
        public int count = 0;
    }
}
