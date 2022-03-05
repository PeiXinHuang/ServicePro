using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Net
{
	//注册
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

}
