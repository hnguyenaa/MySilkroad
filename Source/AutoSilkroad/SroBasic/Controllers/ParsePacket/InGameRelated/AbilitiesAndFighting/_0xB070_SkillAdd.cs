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
    /// [_0xB070] Skill Add
    /// </summary>
    public static class _0xB070
    {
        struct Data
        {
            public uint Attacker_ID { get; set; }
            public uint Skill_ID { get; set; }
            public uint Temp_Skill_ID { get; set; }
            public SkillType Type { get; set; }
        }

        enum SkillType
        {
            None = 0,
            Attact = 0x0230,
            Buff = 0x0030
        }

        private static Data Parse(Packet packet)
        {
            Data data = new Data();

            byte isAccess = packet.ReadUInt8();
            if (isAccess == 0x01)
            {
                var code = packet.ReadUInt16();// code = 0x0030|0x0230
                var skill_id = packet.ReadUInt32();
                var attacker_id = packet.ReadUInt32(); //if attacker_id = Character.UnquiID => character attack else mob attack
                var temp_skill_id = packet.ReadUInt32();//unk = 0xB071.temp
                var obj_id = packet.ReadUInt32();
                var status = packet.ReadUInt8();//if type = 0x00 => mob die

                data.Attacker_ID = attacker_id;
                data.Skill_ID = skill_id;
                data.Temp_Skill_ID = temp_skill_id;
                data.Type = (SkillType)code;
            }

            return data;
        }

        //private static Data Parse(Packet packet)
        //{
        //    Data data = new Data();
        //    //try
        //    //{
        //    byte isAddSkill = packet.ReadUInt8();
        //    if (isAddSkill == 0x01)
        //    {
        //        var skillType = packet.ReadUInt8();
        //        if (skillType == 0x02)
        //        {
        //            #region skill attack
        //            var isSuccess = packet.ReadUInt8();
        //            if (isSuccess == 0x30)
        //            {
        //                uint skill_id = packet.ReadUInt32();
        //                uint attacker_id = packet.ReadUInt32(); //if attacker_id = Character.UnquiID => character attack else mob attack
        //                uint temp_skill_id = packet.ReadUInt32();//unk = 0xB071.temp
        //                //uint obj_id = packet.ReadUInt32();
        //                //byte type = packet.ReadUInt8();//if type = 0x00 => mob die

        //                data.Attacker_ID = attacker_id;
        //                data.Skill_ID = skill_id;
        //                data.Temp_Skill_ID = temp_skill_id;
        //                data.Type = 2;
        //            }
        //            #endregion
        //        }
        //        else if (skillType == 0x00)
        //        {
        //            #region skill buff
        //            var isSuccess = packet.ReadUInt8();
        //            if (isSuccess == 0x30)
        //            {
        //                uint skill_id = packet.ReadUInt32();
        //                uint attacker_id = packet.ReadUInt32(); //if attacker_id = Character.UnquiID => character attack else mob attack
        //                uint temp_skill_id = packet.ReadUInt32();//unk = 0xB071.temp
        //                //uint obj_id = packet.ReadUInt32();
        //                //byte type = packet.ReadUInt8();//if type = 0x00 => mob die

        //                data.Attacker_ID = attacker_id;
        //                data.Skill_ID = skill_id;
        //                data.Temp_Skill_ID = temp_skill_id;
        //                data.Type = 2;
        //            }
        //            #endregion
        //        }
        //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Views.BindingView.Write("[ERROR] [ParsePacketDebug::SkillBegin]");
        //    //    Views.BindingView.WriteLine(ex.Message);
        //    //}

        //    return data;
        //}
        private static void Share(Data data)
        {
            if (Globals.Character.UniqueID == data.Attacker_ID)
            {
                var skill = Globals.Character.StartUsingSkillTrain(data.Skill_ID, data.Temp_Skill_ID);
                Bot.BotInput.StartUsingSkillTrain(skill);

                //if (data.Type == 0x00)
                //{
                //    Bot.BotInput.SkillAdd_BeginCastBuffSkill(data.Skill_ID);
                //}
                //else if (data.Type == 0x02)
                //{
                //    Bot.BotInput.SetCurrentSkillUsing(data.Skill_ID);
                //}
            }
            else if (Metadata.Globals.MobSpawns.ContainsKey(data.Attacker_ID))
            {
                Metadata.Globals.MobSpawns[data.Attacker_ID].CountAttack += 1;

                Bot.BotInput.MobAttacking(data.Attacker_ID);
            }

        }

        
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
