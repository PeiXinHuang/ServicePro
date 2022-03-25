using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.Favorite
{
    class Favorite
    {
        public int poemId;
        public string userMail;
        public string poemTitle;
        public bool isFavorite;
    }

    class FavoriteData {
        public Favorite[] favorites;
        public int count;
    }
}
