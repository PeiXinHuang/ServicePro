using ServicePro.Module.AdminUser;
using ServicePro.Module.Md5;
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
        public static void MsgDownloadUserList(ClientState c, MsgBase msgBase)
        {
            MsgDownloadUserList msg = (MsgDownloadUserList)msgBase;
            msg.content = UserMgr.GetAllUsersFromDb();
            msg.result = 0;
            NetManager.Send(c, msg);
        }

        public static void MsgDownloadAdminUserList(ClientState c, MsgBase msgBase)
        {
            MsgDownloadAdminUserList msg = (MsgDownloadAdminUserList)msgBase;
            msg.content = AdminUserMgr.GetAllAdminUsersFromDb();
            NetManager.Send(c, msg);
        }

        public static void MsgDownloadUserBehaivor(ClientState c, MsgBase msgBase)
        {
            MsgDownloadUserBehaivor msg = (MsgDownloadUserBehaivor)msgBase;
            msg.content = UserBehaviorMgr.GetUserBehaviorsFromDb(msg.userMail);
            NetManager.Send(c, msg);
        }

        public static void MsgDownloadPoemAuthor(ClientState c, MsgBase msgBase)
        {
            MsgDownloadPoemAuthor msg = (MsgDownloadPoemAuthor)msgBase;
            msg.content = PoemMgr.GetPoemJsonByAuthor(msg.author);
            NetManager.Send(c, msg);
        }

        public static void MsgDownloadPoemDynasty(ClientState c, MsgBase msgBase)
        {
            MsgDownloadPoemDynasty msg = (MsgDownloadPoemDynasty)msgBase;
            msg.content = PoemMgr.GetPoemJsonByDynasty(msg.dynasty);
            NetManager.Send(c, msg);
        }
        public static void MsgDownloadPoemBook(ClientState c, MsgBase msgBase)
        {
            MsgDownloadPoemBook msg = (MsgDownloadPoemBook)msgBase;
            msg.content = PoemMgr.GetPoemJsonByBook(msg.book);
            NetManager.Send(c, msg);
        }
        public static void MsgDownloadPoemType(ClientState c, MsgBase msgBase)
        {
            MsgDownloadPoemType msg = (MsgDownloadPoemType)msgBase;
            msg.content = PoemMgr.GetPoemJsonByType(msg.type);
            NetManager.Send(c, msg);
        }

        //public static void MsgDownloadPoemAuthorList(ClientState c, MsgBase msgBase)
        //{
        //    MsgDownloadPoemAuthorList msg = (MsgDownloadPoemAuthorList)msgBase;
        //    msg.content = PoemMgr.GetPoemAuthorListJson();
        //    NetManager.Send(c, msg);
        //}

        //public static void MsgDownloadPoemDynastyList(ClientState c, MsgBase msgBase)
        //{
        //    MsgDownloadPoemDynastyList msg = (MsgDownloadPoemDynastyList)msgBase;
        //    msg.content = PoemMgr.GetPoemDynastyListJson();
        //    NetManager.Send(c, msg);
        //}

        //public static void MsgDownloadPoemBookList(ClientState c, MsgBase msgBase)
        //{
        //    MsgDownloadPoemBookList msg = (MsgDownloadPoemBookList)msgBase;
        //    msg.content = PoemMgr.GetPoemBookListJson();
        //    NetManager.Send(c, msg);
        //}

        //public static void MsgDownloadPoemTypeList(ClientState c, MsgBase msgBase)
        //{
        //    MsgDownloadPoemTypeList msg = (MsgDownloadPoemTypeList)msgBase;
        //    msg.content = PoemMgr.GetPoemTypeListJson();
        //    NetManager.Send(c, msg);
        //}

        public static void MsgDownloadPkgVersion(ClientState c, MsgBase msgBase)
        {
            MsgDownloadPkgVersion msg = (MsgDownloadPkgVersion)msgBase;
            msg.result = 0;
            msg.md5 = MdFiveMgr.GetMD5HashFromFile(PkgManager.globalVersionFilePath);
            NetManager.Send(c, msg);
        }
    }
}
