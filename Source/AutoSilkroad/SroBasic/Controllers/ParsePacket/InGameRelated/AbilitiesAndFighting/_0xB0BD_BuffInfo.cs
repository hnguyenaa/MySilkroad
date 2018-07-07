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
                Globals.Character.UsingSkill(data.BuffSkillID, data.TempSkillID);
            }
        }
        public static void DoWork(Packet packet)
        {
            if (Metadata.Globals.IsDebug)
            {
                ParseCompact(packet);
            }
            else
            {
                //var data = Parse(packet);
                //Share(data);
                ParseCompact(packet);
            }
            
            
        }

        private static void ParseCompact(Packet packet)
        {
            uint casterWorldId = packet.ReadUInt32();
            if (casterWorldId == Globals.Character.UniqueID)
            {
                uint skillTypeId = packet.ReadUInt32();
                uint tempId = packet.ReadUInt32();

                Views.BindingFrom.WriteLine("[0xB0BD] id, temp: " + skillTypeId + ", " + tempId);
                Globals.Character.UsingSkill(skillTypeId, tempId);
            }
        }
        
    }
}
