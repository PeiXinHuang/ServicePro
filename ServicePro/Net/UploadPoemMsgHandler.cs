using ServicePro.Module.Poem;
using ServicePro.Module.User;
using ServicePro.Module.UserBehavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Net
{
    public partial class MsgHandler
    {
        public static void MsgUploadPoem(ClientState c, MsgBase msgBase)
        {
            MsgUploadPoem msg = (MsgUploadPoem)msgBase;
            bool result = PoemMgr.SetPoemJosnStrToDb(msg.poemContent);
            msg.result = result ? 0 : 1;
            NetManager.Send(c, msg);
        }
    }
}
