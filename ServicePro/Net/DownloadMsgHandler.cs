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
        public static void MsgDownload(ClientState c, MsgBase msgBase)
        {
            MsgDownload msg = (MsgDownload)msgBase;
            if(msg.downloadType == 0)
            {
                msg.content = PoemMgr.GetPoemJsonRangeFromDb();
                msg.downloadType = 0;
                msg.result = 0;
            }
            else if(msg.downloadType == 1)
            {
                msg.content = UserMgr.GetAllUsersFromDb();
                msg.downloadType = 1;
                msg.result = 0;
            }
            else if (msg.downloadType == 2)
            {
                msg.content = UserBehaviorMgr.GetAllUsersRigisterFromDb();
                msg.downloadType = 2;
                msg.result = 0;
            }
            NetManager.Send(c, msg);
        }
    }
}
