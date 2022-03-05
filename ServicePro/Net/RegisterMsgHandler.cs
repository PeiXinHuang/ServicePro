using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Net
{
    public partial class MsgHandler
    {
		//注册协议处理

		public static void MsgRegister(ClientState c, MsgBase msgBase)
		{
			MsgRegister msg = (MsgRegister)msgBase;

			//todo:写到数据库
			Console.WriteLine("写入数据库");
			msg.result = 0;
			
			NetManager.Send(c, msg);
		}
	}
}
