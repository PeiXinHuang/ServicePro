using ServicePro.Db;
using ServicePro.Module.AdminUser;
using ServicePro.Module.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicePro.Module.UserBehavior;
namespace ServicePro.Net
{
	public partial class MsgHandler
	{
		//登陆协议处理
		public static void MsgLogin(ClientState c, MsgBase msgBase)
		{
			MsgLogin msg = (MsgLogin)msgBase;
		
			//Console.WriteLine("用户账号登陆：" + msg.mail + msg.password);
			User user = UserDbMgr.GetUserByUserMail(msg.mail);
			if (user == null)
			{
				msg.result = 1;
			}
			else
			{
                if (msg.password.Equals(user.password))
                {
					msg.mail = user.mail;
					msg.password = user.password;
					msg.name = user.name;
					msg.result = 0;
                }
                else
                {
					msg.result = 2;
                }
			}
			NetManager.Send(c, msg);	
		}

		//注册协议处理
		public static void MsgRegister(ClientState c, MsgBase msgBase)
		{
			MsgRegister msg = (MsgRegister)msgBase;
			User user = new User();
			user.mail = msg.mail;
			user.name = msg.name;
			user.password = msg.password;
			
			bool registerResult = UserDbMgr.InsertUser(user);
			if (!registerResult)
			{
				msg.result = 1;
			}
			else
			{
				UserBehaviorDbMgr.InsertBehavior(user.mail, "注册账号");
				msg.result = 0;
			}
			NetManager.Send(c, msg);
		}

		//修改密码协议处理 
		public static void MsgModifyPassword(ClientState c, MsgBase msgBase)
        {
			MsgModifyPassword msg = (MsgModifyPassword)msgBase;
			User user = new User();
			user.mail = msg.mail;
			user.password = msg.password;
			bool modifyResult = UserDbMgr.UpdateUserPassword(user);
			if (!modifyResult)
			{
				msg.result = 1;
			}
			else
			{
				UserBehaviorDbMgr.InsertBehavior(user.mail, "修改账号密码");
				msg.result = 0;
			}
			NetManager.Send(c, msg);
		}



		public static void MsgAdminLogin(ClientState c, MsgBase msgBase)
		{
			MsgAdminLogin msg = (MsgAdminLogin)msgBase;
			//Console.WriteLine("管理员账号登陆：" + msg.mail + msg.password);

			AdminUser admin = AdminUserDbMgr.GetAdminUserByMail(msg.mail);
			if (admin == null)
			{
				msg.result = 1;
			}
			else
			{
				msg.mail = admin.mail;
				msg.name = admin.name;
				msg.password = admin.password;
				//Console.WriteLine("数据库中获取到数据" + msg.mail + msg.password);
				msg.result = 0;
			}
			NetManager.Send(c, msg);
		}


		//注册协议处理
		public static void MsgAdminRegister(ClientState c, MsgBase msgBase)
		{
			MsgAdminRegister msg = (MsgAdminRegister)msgBase;
			AdminUser user = new AdminUser();
			user.mail = msg.mail;
			user.name = msg.name;
			user.password = msg.password;

			bool registerResult = AdminUserDbMgr.InsertAdminUser(user);
			if (!registerResult)
			{
				msg.result = 1;
			}
			else
			{
				msg.result = 0;
			}
			NetManager.Send(c, msg);
		}

		//修改密码协议处理 
		public static void MsgAdminModifyPassword(ClientState c, MsgBase msgBase)
		{
			MsgAdminModifyPassword msg = (MsgAdminModifyPassword)msgBase;
			AdminUser user = new AdminUser();
			user.mail = msg.mail;
			user.name = msg.name;
			user.password = msg.password;
			bool modifyResult = AdminUserDbMgr.UpdateAdminUser(user);
			if (!modifyResult)
			{
				msg.result = 1;
			}
			else
			{
				msg.result = 0;
			}
			NetManager.Send(c, msg);
		}

	}
}
