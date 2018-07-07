using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0xB072] Buff Dell
    /// </summary>
    public static class _0xB072
    {
        class Data
        {
            public uint TempSkillID { get; set; }
        }
        private static Data Parse(Packet packet)
        {
            Data data = new Data();

            var isAccess = packet.ReadUInt8();
            if (isAccess == 0x01)
            {
                uint SkillID = packet.ReadUInt32();//temp id

                data.TempSkillID = SkillID;
            }

            return data;
        }
        private static void Share(Data data)
        {
            if (data.TempSkillID > 0)
            {
                Bot.BotInput.RepeatBuffSkill(data.TempSkillID);
            }
        }
        public static void DoWork(Packet packet)
        {
            if (Metadata.Globals.IsDebug)
                ParseDebug(packet);
            else
            {
                //var data = Parse(packet);
                //Share(data);
                ParseCompact(packet);
            }
            
        }
        private static void ParseDebug(Packet packet)
        {
            uint statusCode = packet.ReadUInt8();
            if (statusCode == 0x01)
            {
                uint tempId = packet.ReadUInt32();
                Views.BindingFrom.WriteLine("[0xB072] buff End: " + tempId);

                Metadata.Globals.Character.RefreshBuffSkill(tempId);
                Bot.BotInput.RepeatBuffSkill(tempId);
            }
        }


        private static void ParseCompact(Packet packet)
        {
            uint statusCode = packet.ReadUInt8();
            if (statusCode == 0x01)
            {
                uint tempId = packet.ReadUInt32();

                Bot.BotInput.RepeatBuffSkill(tempId);
            }
        }
    }
}
