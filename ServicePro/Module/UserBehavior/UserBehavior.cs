using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.UserBehavior
{
    class UserBehavior
    {
        public string mail;
        public string behavior;
        public string  date;
        public UserBehavior(string _mail, string _behavior, string _date)
        {
            mail = _mail;
            behavior = _behavior;
            date = _date;
        }
    }
}
