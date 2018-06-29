using SroBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Component.Logic
{
    static class Globals
    {
        public static ClientInfo clientInfo;
        public static string loginUser;
        public static string loginPass;
        


        static Globals()
        {
            clientInfo = new ClientInfo
            {
                Version = 281,
                Locale = 22
            };

            loginUser = "hnguyenaa";
            loginPass = "12021989";
        }
    }
}
