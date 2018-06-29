using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0x3013] CharacterData
    /// </summary>
    public static class _0x3013
    {
        //public static void Parse(Packet packet)
        //{ }
        public static Packet _0x3013_Packet = new Packet(0x3013);
        //private static void Share(Packet packet)
        //{
        //    _0x3013_Packet = packet;
        //}
        public static void DoWork(Packet packet)
        {
            _0x3013_Packet = packet;
        }
    }
}
