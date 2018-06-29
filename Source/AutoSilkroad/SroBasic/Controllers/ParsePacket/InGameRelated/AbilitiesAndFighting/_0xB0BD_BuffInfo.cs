using SilkroadSecurityApi;
using SroBasic.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0xB0BD] Buff Info
    /// </summary>
    public static class _0xB0BD
    {
        struct Data
        {
            public uint BuffSkillID { get; set; }
            public uint TempSkillID { get; set; }
        }
        private static Data Parse(Packet packet)
        {
            Data data = new Data();

            uint objectID = packet.ReadUInt32();
            if (objectID == Globals.Character.UniqueID)
            {
                uint skillID = packet.ReadUInt32();
                uint tempSkillID = packet.ReadUInt32();

                data.BuffSkillID = skillID;
                data.TempSkillID = tempSkillID;
            }

            return data;
        }
        private static void Share(Data data)
        {
            if (data.BuffSkillID > 0)
            {
                Globals.Character.StartUsingSkillTrain(data.BuffSkillID, data.TempSkillID);
            }
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
