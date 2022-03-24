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
		public string mail = "";
		public string name = "";
		public string password = "";
		//服务端回（0-成功，1-登陆失败，账号不存在，2-登陆失败，密码不正确）
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
		//客户端发
		public string mail = "";
		public string name = "";
		public string password = "";
		//服务端回（0-成功，1-失败）
		public int result = 0;
	}


	public class MsgAdminRegister : MsgBase
	{
		public MsgAdminRegister() { protoName = "MsgAdminRegister"; }
		//客户端发
		public string name = "";
		public string password = "";
		public string mail = "";
		//服务端回（0-成功，1-失败）
		public int result = 0;
	}

	public class MsgAdminModifyPassword : MsgBase
	{
		public MsgAdminModifyPassword() { protoName = "MsgAdminModifyPassword"; }
		public string name = "";
		public string mail = "";
		public string password = "";
		//服务端回（0-成功，1-失败, 2-邮箱已经存在，因此注册失败）
		public int result = 0;
	}


	//注册协议
	public class MsgRegister : MsgBase
	{
		public MsgRegister() { protoName = "MsgRegister"; }
		//客户端发
		public string mail = "";
		public string password = "";
		public string name = "";
		//服务端回（0-成功，1-失败, 2-邮箱已经存在，因此注册失败）
		public int result = 0;
	}

	public class MsgModifyPassword : MsgBase
    {
		public MsgModifyPassword() { protoName = "MsgModifyPassword"; }
		public string mail = "";
		public string password = "";
		public string name = "";
		//服务端回（0-成功，1-失败, 2-邮箱已经存在，因此注册失败）
		public int result = 0;
	} 
}
