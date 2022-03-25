using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Net
{
    class MsgUploadPoem : MsgBase
    {
        public MsgUploadPoem() { protoName = "MsgDownload"; }
        public string poemContent;
        //服务端回（0-成功，1-失败）
        public int result = 0;
    }

}
