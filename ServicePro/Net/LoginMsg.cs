using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Net
{
	//登陆
	public class MsgLogin : MsgBase
	{
		public MsgLogin() { protoName = "MsgLogin"; }
		//客户端发
		public string id = "";
		public string pw = "";
		//服务端回（0-成功，1-失败）
		public int result = 0;
	}


	//踢下线（服务端推送）
	public class MsgKick : MsgBase
	{
		public MsgKick() { protoName = "MsgKick"; }
		//原因（0-其他人登陆同一账号）
		public int reason = 0;
	}

	public class MsgAdminLogin : MsgBase
    {
		public MsgAdminLogin() { protoName = "MsgAdminLogin"; }
		public string username;
		public string password;
		public int result = 0;
    }
}
