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
    /// [_0xB071] Skill Casted
    /// </summary>
    public static class _0xB071
    {
        class Data
        {
            public uint TempSkillID { get; set; }
            public uint ObjectBeAttacked { get; set; }
            public ObjectWasAttackedType Status { get; set; }

            public Data()
            {
                Status = ObjectWasAttackedType.None;
            }
        }

        enum ObjectWasAttackedType
        {
            Alive = 0x00,
            None = 0x01,
            Die = 0x80,

        }

        private static Data Parse(Packet packet)
        {
            Data data = new Data();

            var isAccess = packet.ReadUInt8();
            if (isAccess == 0x01)
            {
                uint temp_skill_id = packet.ReadUInt32();//unk away = 0xB070.temp
                uint objectWasAttacked = packet.ReadUInt32();
                var isNextRead = packet.ReadUInt8();
                if (isNextRead == 0x01)
                {
                    isAccess = packet.ReadUInt8();
                    var countObject = packet.ReadUInt8(); // = 01/02/03 (Number of mobs was attacked)
                    var objectId = packet.ReadUInt32();// = objectWasAttacked
                    var objectStatus = packet.ReadUInt8();// status
                    //packet.ReadUInt32(); //not need
                    //packet.ReadUInt32(); //not need

                    //else if (mobStatus == 0x04 || mobStatus == 0x08)//nock down
                    //{
                    //}
                    //else //unk
                    //{
                    //}
                    data.Status = (ObjectWasAttackedType)objectStatus;
                }

                data.TempSkillID = temp_skill_id;
                data.ObjectBeAttacked = objectWasAttacked;
            }
            
            return data;
        }

        //private static Data Parse(Packet packet)
        //{
        //    Data data = new Data();
        //    //try
        //    //{
        //    if (packet.ReadUInt8() == 0x01)
        //    {
        //        uint temp_skill_id = packet.ReadUInt32();//unk away = 0xB070.temp
        //        uint objectBeingAttacked = packet.ReadUInt32();
        //        if (objectBeingAttacked > 0 && objectBeingAttacked != Globals.Character.UniqueID)
        //        {
        //            packet.ReadUInt8();//uk alway = 01
        //            packet.ReadUInt8();//uk alway = 01
        //            byte mobCount = packet.ReadUInt8();// = 01/02/03 (Number of mobs was attacked)
        //            objectBeingAttacked = packet.ReadUInt32();//temp skill id
        //            byte mobStatus = packet.ReadUInt8();

        //            data.TempSkillID = temp_skill_id;
        //            if (mobStatus == 0x80) //monster die
        //            {
        //                data.MobIsAlive = false;
        //            }
        //            else
        //            {
        //                data.MobIsAlive = true;
        //            }
        //            //else if (mobStatus == 0x04 || mobStatus == 0x08)//nock down
        //            //{
        //            //}
        //            //else //unk
        //            //{
        //            //}
        //        }
        //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Views.BindingView.Write("[ERROR] [SkillCasted::ParsePacketOpcode0xB070]");
        //    //    Views.BindingView.WriteLine(ex.Message);
        //    //}
        //    return data;
        //}
        private static void Share(Data data)
        {
            //Views.BindingView.WriteLine("[_0xB071_SkillCasted::ShareData][" + data.TempSkillID + "][" + data.MobIsAlive + "]");
            if (data.Status == ObjectWasAttackedType.Die)
            {
                Views.BindingFrom.WriteLine("[0xB071][Skill Casted]: mod die id=" + data.ObjectBeAttacked);
                //Bot.BotInput.SelectNextMobForAttack();
            }
            else if (data.Status == ObjectWasAttackedType.Alive)
            {
                //Views.BindingFrom.WriteLine("[0xB071][Skill Casted]: mod Alive id=" + data.ObjectBeAttacked);
                //BotController.ContinueAttack();
                //Bot.BotInput.MobAttackedStillAlive();
            }

            if (data.ObjectBeAttacked == Globals.Character.UniqueID) { }
            
        }
        public static void DoWork(Packet packet)
        {
            var data = DebugParse(packet);
            //var data = Parse(packet);
            Share(data);
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
        private static Data DebugParse(Packet packet)
        {
            #region Struct
            // 0xB071 Server_WorldObject_Cast_End
            //1 byte isSuccess 
            //if (isSuccess == 1)
            //{
            //    4 byte castWorldId 
            //    4 byte castTargetWorldId 

            //    ParseDmg();
            //}
            //else if (isSuccess == 2)
            //{
            //    2 byte ErrorCode
            //    4 byte castWorldId
            //}

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

            Data data = new Data();

            Views.BindingFrom.WriteLine(Environment.NewLine + "[0xB071] ==== Start debug ====");
            try
            {
                byte result = packet.ReadUInt8();

                ResultType resultCode = (ResultType)result; Views.BindingFrom.WriteDebug("resultCode = " + resultCode + " | 0x" + result.ToString("X2"));
                if (resultCode == ResultType.Success)
                {
                    uint castWorldId = packet.ReadUInt32(); Views.BindingFrom.WriteDebug("castWorldId = " + castWorldId + " | 0x" + castWorldId.ToString("X8"));
                    uint castTargetWorldId = packet.ReadUInt32(); Views.BindingFrom.WriteDebug("castTargetWorldId = " + castTargetWorldId + " | 0x" + castTargetWorldId.ToString("X8"));

                    ParseDmg(packet);
                    //byte hasDmgCode = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("hasDmgCode = " + hasDmgCode + " | 0x" + hasDmgCode.ToString("X2"));
                    //if (hasDmgCode == 0x01)
                    //{
                    //    byte hitCount = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("hitCount = " + hitCount + " | 0x" + hitCount.ToString("X2"));
                    //    byte targetCount = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("targetCount = " + targetCount + " | 0x" + targetCount.ToString("X2"));

                    //    Views.BindingFrom.WriteDebug("foreach targetCount : ");
                    //    for (int i = 0; i < targetCount; i++)
                    //    {
                    //        uint targetWorldId = packet.ReadUInt32(); Views.BindingFrom.WriteDebug("targetWorldId = " + targetWorldId + " | 0x" + targetWorldId.ToString("X8"));
                    //        Views.BindingFrom.WriteDebug("foreach hitCount : ");
                    //        for (int ii = 0; ii < hitCount; ii++)
                    //        {
                    //            byte effects = packet.ReadUInt8();
                    //            AttackState attackState = (AttackState)effects; Views.BindingFrom.WriteDebug("attackState = " + attackState + " | 0x" + effects.ToString("X2"));
                    //            byte dmgSource = packet.ReadUInt8();
                    //            DamageState damageState = (DamageState)dmgSource; Views.BindingFrom.WriteDebug("damageState = " + damageState + " | 0x" + dmgSource.ToString("X2"));

                    //            Views.BindingFrom.WriteDebug("effects & 0x0A = 0x" + (effects & 0x0A).ToString("X2"));
                    //            if ((effects & 0x0A) != 0x00)
                    //            {
                    //                continue;
                    //            }

                    //            uint dmg = packet.ReadUInt32(); Views.BindingFrom.WriteDebug("dmg = " + dmg + " | 0x" + dmg.ToString("X8"));
                    //            uint unk1 = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("unk1 = " + unk1 + " | 0x" + unk1.ToString("X2"));
                    //            uint unk2 = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("unk2 = " + unk2 + " | 0x" + unk2.ToString("X2"));
                    //            uint unk3 = packet.ReadUInt8(); Views.BindingFrom.WriteDebug("unk3 = " + unk3 + " | 0x" + unk3.ToString("X2"));
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Views.BindingFrom.WriteDebug("[0xB070][hasDmgCode != 0x01]");
                    //    Views.BindingFrom.WriteDebugPacket(packet);
                    //}
                }
                else
                {
                    ushort statusCode = packet.ReadUInt16(); Views.BindingFrom.WriteDebug("statusCode = " + statusCode + " | 0x" + statusCode.ToString("X2"));
                    Views.BindingFrom.WriteDebug(Environment.NewLine + "[0xB070][resultCode != ResultType.Success]");
                    Views.BindingFrom.WriteDebugPacket(packet);
                }
            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine("[Error][0xB071][Skill Casted] " + ex.Message);
                Views.BindingFrom.WriteDebugPacket(packet);
            }

            Views.BindingFrom.WriteLine("[0xB071] ==== End debug ====" + Environment.NewLine);

            return data;
        }
    }
}
