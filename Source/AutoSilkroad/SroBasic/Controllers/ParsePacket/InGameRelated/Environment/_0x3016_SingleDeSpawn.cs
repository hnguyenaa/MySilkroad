using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0x3016] Single Despawn
    /// </summary>
    public static class _0x3016
    {
        private static uint Parse(Packet packet)
        {
            uint objectID = packet.ReadUInt32();

            return objectID;
        }
        private static void Share(uint data)
        {
            //var index = Metadata.Globals.MobSpawns.FindIndex(a => a.UniqueID == data);
            //if (index != -1)
            //{
            //    Views.BindingFrom.WriteLine("[0x3016][Single Despawn] id = " + data);
            //    Metadata.Globals.MobSpawns.RemoveAt(index);
            //}

            //Views.BindingFrom.WriteLine("[0x3016][Single Despawn] id = " + data);
            Metadata.Globals.RemoveMob(data);
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
