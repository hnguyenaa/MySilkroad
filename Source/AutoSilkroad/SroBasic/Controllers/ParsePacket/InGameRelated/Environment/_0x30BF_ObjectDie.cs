using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0x30BF] Object Die
    /// </summary>
    public static class _0x30BF
    {
        class Data
        {
            public uint ObjectDieId { get; set; }
        }
        private static Data Parse(Packet packet)
        {
            var data = new Data();

            uint objectID = packet.ReadUInt32();
            byte flag = packet.ReadUInt8();
            if (flag == 0x00)
            {
                flag = packet.ReadUInt8();
                if (flag == 0x02)
                {
                    data.ObjectDieId = objectID;
                }
            }

            return data;
        }
        private static void Share(Data data)
        {
            if (data.ObjectDieId > 0)
            {
                Metadata.Globals.SetMobDie(data.ObjectDieId);
                Bot.BotInput.DoWork_MobDie(data.ObjectDieId);
            }
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
