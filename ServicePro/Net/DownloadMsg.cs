using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Net
{
    class MsgDownload : MsgBase
    {
        public MsgDownload() { protoName = "MsgDownload"; }
        public string content;
        public int downloadType; //下载文件类型 ， 0 - 诗词文件，1 - 用户文件
        //服务端回（0-成功，1-失败）
        public int result = 0;
    }

}
