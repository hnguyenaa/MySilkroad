using SilkroadSecurityApi;
using SroBasic.Metadata;
using SroBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0xB0A1] Character Skill Update
    /// </summary>
    public static class _0xB0A1
    {
        public static Skill Parse(Packet packet)
        {
            Skill new_skill = new Skill();
            byte flag = packet.ReadUInt8();
            if (flag == 0x01)
            {
                uint new_skill_id = packet.ReadUInt32();

                if (MediaData.Skills.ContainsKey(new_skill_id))
                {
                    new_skill = MediaData.Skills[new_skill_id];
                }
            }
            return new_skill;
        }
        private static void Share(Skill data)
        {
            if (data != null && data.ID > 0)
            {
                Metadata.Globals.Character.SkillUpdate(data);
                Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.Skill);
            }
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
