﻿using ServicePro.Db;
using ServicePro.Module.AdminUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Net
{
	public partial class MsgHandler
	{
		

		//登陆协议处理
		public static void MsgLogin(ClientState c, MsgBase msgBase)
		{
			MsgLogin msg = (MsgLogin)msgBase;
			//密码校验
			//if (!DbManager.CheckPassword(msg.id, msg.pw))
			//{
			//	msg.result = 1;
			//	NetManager.Send(c, msg);
			//	return;
			//}
			////不允许再次登陆
			//if (c.player != null)
			//{
			//	msg.result = 1;
			//	NetManager.Send(c, msg);
			//	return;
			//}
			////如果已经登陆，踢下线
			//if (PlayerManager.IsOnline(msg.id))
			//{
			//	//发送踢下线协议
			//	Player other = PlayerManager.GetPlayer(msg.id);
			//	MsgKick msgKick = new MsgKick();
			//	msgKick.reason = 0;
			//	other.Send(msgKick);
			//	//断开连接
			//	NetManager.Close(other.state);
			//}
			////获取玩家数据
			//PlayerData playerData = DbManager.GetPlayerData(msg.id);
			//if (playerData == null)
			//{
			//	msg.result = 1;
			//	NetManager.Send(c, msg);
			//	return;
			//}
			////构建Player
			//Player player = new Player(c);
			//player.id = msg.id;
			//player.data = playerData;
			//PlayerManager.AddPlayer(msg.id, player);
			//c.player = player;
			////返回协议
			msg.result = 0;
			NetManager.Send(c, msg);
			//player.Send(msg);
		}


		public static void MsgAdminLogin(ClientState c, MsgBase msgBase)
        {
			MsgAdminLogin msg = (MsgAdminLogin)msgBase;
			Console.WriteLine("管理员账号登陆：" + msg.username + msg.password);

			AdminUserDbMgr mgr = new AdminUserDbMgr();
			AdminUser admin = mgr.GetAdminUserByUsername(msg.username);
			if(admin == null)
            {
				msg.result = 1;
				
            }
            else
            {
				msg.username = admin.username;
				msg.password = admin.password;
				Console.WriteLine("数据库中获取到数据" + msg.username + msg.password);
				msg.result = 0;
			}
			NetManager.Send(c, msg);
        }
	}
}