using ServicePro.Module.Favorite;
using ServicePro.Module.Poem;
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
        public static void MsgFavorite(ClientState c, MsgBase msgBase)
        {
            MsgFavorite msgFavorite = (MsgFavorite)msgBase;
            if(msgFavorite.isFavorite == 2) //查询是否收藏诗词
            {
                if (FavoriteDbMgr.GetIsFavorite(msgFavorite.poemId,msgFavorite.userMail))
                {
                    msgFavorite.isFavorite = 1;
                }
                else
                {
                    msgFavorite.isFavorite = 0;
                }
                msgFavorite.result = 0;
            }
            else //收藏或者取消收藏诗词
            {
                FavoriteDbMgr.SetFavorite(msgFavorite.poemId, msgFavorite.userMail, msgFavorite.poemTitle, msgFavorite.isFavorite);
                Poem poem = PoemDbMgr.GetPoemsById(msgFavorite.poemId);
                
                string poemTitle = "";
                if(poem != null)
                {
                    poemTitle = poem.title;
                }
                if(msgFavorite.isFavorite == 1)
                {
                    UserBehaviorDbMgr.InsertBehavior(msgFavorite.userMail, "收藏诗词" + poemTitle);
                }
                else
                {
                    UserBehaviorDbMgr.InsertBehavior(msgFavorite.userMail, "取消收藏诗词" + poemTitle);
                }
               
                msgFavorite.result = 0;
            }
            NetManager.Send(c, msgFavorite);
        }
    }
}
