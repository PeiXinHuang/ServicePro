using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Net
{
    public class MsgFavorite : MsgBase
    {
        public MsgFavorite() { protoName = "MsgFavorite"; }
        public string userMail = "";
        public int poemId = 0;
        public int isFavorite = 0;//1 代表收藏，0代表取消收藏，2代表查询收藏
        public int result = 0;
    }
}
