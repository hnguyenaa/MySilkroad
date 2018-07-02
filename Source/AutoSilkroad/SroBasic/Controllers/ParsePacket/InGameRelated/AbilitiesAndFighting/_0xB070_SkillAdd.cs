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
            //var data = Parse(packet);
            var data = DebugParse(packet);
            Share(data);
        }

        public static void Parse_new(Packet packet)
        {
            #region structure
            // 0xB070 Server_WorldObject_Cast_Start 
            //1 byte isSuccess 
            //if (isSuccess == 1 )
            //{
            //    1 byte unk1 
            //    1 byte unk2  
            //    4 byte skillTypId 
            //    4 byte casterWorldId 
            //    4 byte unk3
            //    4 byte targetWorldId
            //    1 byte instantResponse

            //    if (instantResponse == 1)
            //    {
            //        ParseDmg()
            //    }
            //    else 
            //    {
            //        1 byte hasDmg ?? --> 1 --> yes ?? 
            //    }
            //}
            //else 
            //{
            //    2 byte ErrorCode
            //}
            #endregion

            byte resultCode = packet.ReadUInt8();
            if (resultCode == 0x01)
            {
                ushort statusCode = packet.ReadUInt16();
                uint skillTypeId = packet.ReadUInt32();
                uint casterWorldId = packet.ReadUInt32();
                uint currentUniqueId = packet.ReadUInt32();
                uint targetWorldId = packet.ReadUInt32();
                byte instantResponseCode = packet.ReadUInt8();
                if (instantResponseCode == 0x01)
                {
                     ParseDmg(packet);
                }
                else
                {
                    byte hasDmg = packet.ReadUInt8();
                }
            }
            else
            {
                ushort statusCode = packet.ReadUInt16();
            }
        }

        private static void ParseDmg(Packet packet)
        {
            #region structure
            // ParseDmg				
            //1 byte hasDmg
            //if (hasDmg == 1 )
            //{
            //    1 byte hitcount
            //    1 byte targetCount
            //    for (int i = 0; i < targetCount; i++)	
            //    {
            //        4 byte targetWorldId
            //        for (int ii = 0; ii < hitcount; ii++)
            //                {
            //            1 byte effects   // bitmask --> ATTACK_STATE_KNOCKBACK = 0x01,    ATTACK_STATE_BLOCK = 0x02,    ATTACK_STATE_POSITION = 0x04,   abort ? --> 0x08   ATTACK_STATE_DEAD = 0x80
            //                1 byte dmgSource // bitmask --> DAMAGE_STATE_NORMAL = 0x01,    DAMAGE_STATE_CRIT = 0x02,    DAMAGE_STATE_STATUS = 0x10

            //            if ((attackState & 0x0A) != 0x00)  // if ATTACK_STATE_BLOCK or abort --> no more data
            //            {
            //                continue;
            //            }

            //            4 byte dmg
            //            1 byte unk4
            //            1 byte unk5 
            //            1 byte unk6
            //         }		
            //    }
            //}
            #endregion

            //byte hasDmgCode = packet.ReadUInt8();
            //if (hasDmgCode == 0x01)
            //{
            //    byte hitCount = packet.ReadUInt8();
            //    byte targetCount = packet.ReadUInt8();

            //    for (int i = 0; i < targetCount; i++)
            //    {
            //        uint targetWorldId = packet.ReadUInt32();
            //        for (int ii = 0; ii < hitCount; ii++)
            //        {
            //            byte effects = packet.ReadUInt8();
            //            byte dmgSource = packet.ReadUInt8();

            //            if ((effects & 0x0A) != 0x00)
            //            {
            //                continue;
            //            }

            //            uint dmg = packet.ReadUInt32();
            //            uint unk1 = packet.ReadUInt8();
            //            uint unk2 = packet.ReadUInt8();
            //            uint unk3 = packet.ReadUInt8();
            //        }
            //    }
            //}

            byte hasDmgCode = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("hasDmgCode = " + hasDmgCode + " | 0x" + hasDmgCode.ToString("X2"));
            if (hasDmgCode == 0x01)
            {
                byte hitCount = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("hitCount = " + hitCount + " | 0x" + hitCount.ToString("X2"));
                byte targetCount = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("targetCount = " + targetCount + " | 0x" + targetCount.ToString("X2"));

                Views.BindingFrom.WriteDebug("foreach targetCount : ");
                for (int i = 0; i < targetCount; i++)
                {
                    uint targetWorldId = packet.ReadUInt32(); Views.BindingFrom.WriteDebug("targetWorldId = " + targetWorldId + " | 0x" + targetWorldId.ToString("X8"));
                    Views.BindingFrom.WriteDebug("foreach hitCount : ");
                    for (int ii = 0; ii < hitCount; ii++)
                    {
                        #region Old
                        //byte effects = packet.ReadUInt8();
                        //AttackState attackState = (AttackState)effects; Views.BindingFrom.WriteDebug("attackState = " + attackState + " | 0x" + effects.ToString("X2"));
                        //byte dmgSource = packet.ReadUInt8();
                        //DamageState damageState = (DamageState)dmgSource; Views.BindingFrom.WriteDebug("damageState = " + damageState + " | 0x" + dmgSource.ToString("X2"));

                        //Views.BindingFrom.WriteDebug("effects & 0x0A = 0x" + (effects & 0x0A).ToString("X2"));
                        //if ((effects & 0x0A) != 0x00)
                        //{
                        //    continue;
                        //}

                        //uint dmg = packet.ReadUInt32(); Views.BindingFrom.WriteDebug("dmg = " + dmg + " | 0x" + dmg.ToString("X8"));
                        //uint unk1 = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("unk1 = " + unk1 + " | 0x" + unk1.ToString("X2"));
                        //uint unk2 = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("unk2 = " + unk2 + " | 0x" + unk2.ToString("X2"));
                        //uint unk3 = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("unk3 = " + unk3 + " | 0x" + unk3.ToString("X2"));
                        #endregion

                        byte effects = packet.ReadUInt8();
                        AttackState attackState = (AttackState)effects; Views.BindingFrom.WriteDebug("attackState = " + attackState + " | 0x" + effects.ToString("X2"));
                        uint dmg = packet.ReadUInt32(); Views.BindingFrom.WriteDebug("dmg = " + dmg + " | 0x" + dmg.ToString("X8"));
                        uint unk = packet.ReadUInt32(); Views.BindingFrom.WriteDebug("unk = " + unk + " | 0x" + unk.ToString("X8"));

                    }
                }
            }
            else
            {
                Views.BindingFrom.WriteDebug(Environment.NewLine + "[0xB070][hasDmgCode != 0x01]");
                Views.BindingFrom.WriteDebugPacket(packet);
            }
        }

        [Flags]
        enum AttackState : byte
        {
            None = 0x00,
            KnockBack = 0x01,    
            Block = 0x02,
            Position = 0x04,   
            KnockDown = 0x08,  
            Dead = 0x80
        }

        enum DamageState : byte
        {
            None = 0x00,
            Normal = 0x01, 
            Crit = 0x02, 
            Status = 0x10
        }

        enum ResultType
        {
            Success = 0x01
        }

        class Data_0xB070
        {
            public ResultType Result { get; set; }
        }
        private static Data DebugParse(Packet packet)
        {
            Data data = new Data();

            try
            {
                Views.BindingFrom.WriteDebug(Environment.NewLine + "[0xB070] ==== Start debug ====");

                byte result = packet.ReadUInt8();

                ResultType resultCode = (ResultType)result; Views.BindingFrom.WriteDebug("resultCode = " + resultCode + " | 0x" + result.ToString("X2"));

                if (resultCode == ResultType.Success)
                {
                    ushort statusCode = packet.ReadUInt16(); Views.BindingFrom.WriteDebug("statusCode = " + statusCode + " | 0x" + statusCode.ToString("X4"));
                    uint skillTypeId = packet.ReadUInt32();// Views.BindingFrom.WriteDebug("skillTypeId = " + skillTypeId + " | 0x" + skillTypeId.ToString("X8"));
                    uint casterWorldId = packet.ReadUInt32();// Views.BindingFrom.WriteDebug("casterWorldId = " + casterWorldId + " | 0x" + casterWorldId.ToString("X8"));
                    uint currentUniqueId = packet.ReadUInt32();// Views.BindingFrom.WriteDebug("currentUniqueId = " + currentUniqueId + " | 0x" + currentUniqueId.ToString("X8"));
                    uint targetWorldId = packet.ReadUInt32();// Views.BindingFrom.WriteDebug("targetWorldId = " + targetWorldId + " | 0x" + targetWorldId.ToString("X8"));
                    byte isInstantResponse = packet.ReadUInt8();// Views.BindingFrom.WriteDebug("instantResponseCode = " + isInstantResponse + " | " + isInstantResponse.ToString("X2"));

                    data.Attacker_ID = casterWorldId;
                    data.Skill_ID = skillTypeId;
                    data.Temp_Skill_ID = currentUniqueId;
                    data.Type = (SkillType)isInstantResponse;

                    if (isInstantResponse == 0x01)
                    {
                        ParseDmg(packet);
                    }
                }
                else
                {
                    ushort statusCode = packet.ReadUInt16(); Views.BindingFrom.WriteDebug("statusCode = " + statusCode + " | 0x" + statusCode.ToString("X2"));
                    Views.BindingFrom.WriteDebug(Environment.NewLine + "[0xB070][resultCode != ResultType.Success]");
                    Views.BindingFrom.WriteDebugPacket(packet);
                }

                Views.BindingFrom.WriteDebug("[0xB070] ==== End debug ====" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine("[Error][0xB070][Skill Add] " + ex.Message);
                Views.BindingFrom.WriteDebugPacket(packet);
            }

            

            return data;
        }
    }
}
