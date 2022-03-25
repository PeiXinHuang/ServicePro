using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.UserBehavior
{
    class UserBehaviorMgr
    {
        public static string GetAllUsersRigisterFromDb()
        {
            List<UserBehavior> userBehaviors = new List<UserBehavior>();
            userBehaviors = UserBehaviorDbMgr.GetAllUserRigisterBehavior();
            if (userBehaviors.Count == 0)
            {
                Console.WriteLine("[UserBehaviorDbMgr]获取所有用户注册信息失败");
                return "";
            }
            UserBehaviorData UserBehaviorData = new UserBehaviorData();
            UserBehaviorData.behaviors = userBehaviors.ToArray();
            UserBehaviorData.count = userBehaviors.Count;

            string poemJsonStr = JsonConvert.SerializeObject(UserBehaviorData, Formatting.Indented);

            return poemJsonStr;

        }

        public static string GetUserBehaviorsFromDb(string userMail)
        {
            List<UserBehavior> userBehaviors = new List<UserBehavior>();
            userBehaviors = UserBehaviorDbMgr.GetUserBehaviorByMail(userMail);
            if (userBehaviors.Count == 0)
            {
                Console.WriteLine("[UserBehaviorDbMgr]获取用户行为信息失败");
                return "";
            }
            UserBehaviorData userBehaviorData = new UserBehaviorData();
            userBehaviorData.behaviors = userBehaviors.ToArray();
            userBehaviorData.count = userBehaviors.Count;
            string poemJsonStr = JsonConvert.SerializeObject(userBehaviorData, Formatting.Indented);
            return poemJsonStr;
        }
    }


    class UserBehaviorData
    {
        public UserBehavior[] behaviors;
        public int count = 0;
    }

}
