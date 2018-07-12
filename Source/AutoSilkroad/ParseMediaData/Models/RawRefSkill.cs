using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RawRefSkill
    {
        public byte Service { get; set; }
        public uint ID { get; set; }
        public uint GroupID { get; set; }
        public string Basic_Code { get; set; }
        public string Basic_Name { get; set; }
        public string Basic_Group { get; set; }
        public int Basic_Original { get; set; }
        public byte Basic_Level { get; set; }
        public byte Basic_Activity { get; set; }
        public int Basic_ChainCode { get; set; }
        public int Basic_RecycleCost { get; set; }
        public int Action_PreparingTime { get; set; }
        public int Action_CastingTime { get; set; }
        public int Action_ActionDuration { get; set; }
        public int Action_ReuseDelay { get; set; }
        public int Action_CoolTime { get; set; }
        public int Action_FlyingSpeed { get; set; }
        public int Action_Interruptable { get; set; }
        public int Action_Overlap { get; set; }
        public byte Action_AutoAttackType { get; set; }
        public byte Action_InTown { get; set; }
        public ushort Action_Range { get; set; }
        public byte Target_Required { get; set; }
        public byte TargetType_Animal { get; set; }
        public byte TargetType_Land { get; set; }
        public byte TargetType_Building { get; set; }
        public byte TargetGroup_Self { get; set; }
        public byte TargetGroup_Ally { get; set; }
        public byte TargetGroup_Enemy { get; set; }
        public byte TargetGroup_Neutral { get; set; }
        public byte TargetGroup_DontCare { get; set; }
        public byte TargetEtc_SelectDeadBody { get; set; }
        public int ReqCommon_Mastery1 { get; set; }
        public int ReqCommon_Mastery2 { get; set; }
        public byte ReqCommon_MasteryLevel1 { get; set; }
        public byte ReqCommon_MasteryLevel2 { get; set; }
        public ushort ReqCommon_Str { get; set; }
        public ushort ReqCommon_Int { get; set; }
        public int ReqLearn_Skill1 { get; set; }
        public int ReqLearn_Skill2 { get; set; }
        public int ReqLearn_Skill3 { get; set; }
        public byte ReqLearn_SkillLevel1 { get; set; }
        public byte ReqLearn_SkillLevel2 { get; set; }
        public byte ReqLearn_SkillLevel3 { get; set; }
        public int ReqLearn_SP { get; set; }
        public byte ReqLearn_Race { get; set; }
        public byte Req_Restriction1 { get; set; }
        public byte Req_Restriction2 { get; set; }
        public byte ReqCast_Weapon1 { get; set; }
        public byte ReqCast_Weapon2 { get; set; }
        public ushort Consume_HP { get; set; }
        public ushort Consume_MP { get; set; }
        public ushort Consume_HPRatio { get; set; }
        public ushort Consume_MPRatio { get; set; }
        public byte Consume_WHAN { get; set; }
        public byte UI_SkillTab { get; set; }
        public byte UI_SkillPage { get; set; }
        public byte UI_SkillColumn { get; set; }
        public byte UI_SkillRow { get; set; }
        public string UI_IconFile { get; set; }
        public string UI_SkillName { get; set; }
        public string UI_SkillToolTip { get; set; }
        public string UI_SkillToolTip_Desc { get; set; }
        public string UI_SkillStudy_Desc { get; set; }
        public ushort AI_AttackChance { get; set; }
        public int Param1 { get; set; }
        public int Param2 { get; set; }
        public int Param3 { get; set; }
        public int Param4 { get; set; }
        public int Param5 { get; set; }
        public int Param6 { get; set; }
        public int Param7 { get; set; }
        public int Param8 { get; set; }
        public int Param9 { get; set; }
        public int Param10 { get; set; }
        public int Param11 { get; set; }
        public int Param12 { get; set; }
        public int Param13 { get; set; }
        public int Param14 { get; set; }
        public int Param15 { get; set; }
        public int Param16 { get; set; }
        public int Param17 { get; set; }
        public int Param18 { get; set; }
        public int Param19 { get; set; }
        public int Param20 { get; set; }
        public int Param21 { get; set; }
        public int Param22 { get; set; }
        public int Param23 { get; set; }
        public int Param24 { get; set; }
        public int Param25 { get; set; }

        public static RawRefSkill Parse(string raw)
        {
            RawRefSkill rawSkill = new RawRefSkill();

            try
            {
                if (string.IsNullOrEmpty(raw)) return rawSkill;
                string[] split = raw.Split('\t');

                if (split.Length < 90) return rawSkill;

                rawSkill.Service = Convert.ToByte(split[0]);
                rawSkill.ID = Convert.ToUInt32(split[1]);
                rawSkill.GroupID = Convert.ToUInt32(split[2]);
                rawSkill.Basic_Code = Convert.ToString(split[3]);
                rawSkill.Basic_Name = Convert.ToString(split[4]);
                rawSkill.Basic_Group = Convert.ToString(split[5]);
                rawSkill.Basic_Original = Convert.ToInt32(split[6]);
                rawSkill.Basic_Level = Convert.ToByte(split[7]);
                rawSkill.Basic_Activity = Convert.ToByte(split[8]);
                rawSkill.Basic_ChainCode = Convert.ToInt32(split[9]);
                rawSkill.Basic_RecycleCost = Convert.ToInt32(split[10]);
                rawSkill.Action_PreparingTime = Convert.ToInt32(split[11]);
                rawSkill.Action_CastingTime = Convert.ToInt32(split[12]);
                rawSkill.Action_ActionDuration = Convert.ToInt32(split[13]);
                rawSkill.Action_ReuseDelay = Convert.ToInt32(split[14]);
                rawSkill.Action_CoolTime = Convert.ToInt32(split[15]);
                rawSkill.Action_FlyingSpeed = Convert.ToInt32(split[16]);
                rawSkill.Action_Interruptable = Convert.ToByte(split[17]);
                rawSkill.Action_Overlap = Convert.ToInt32(split[18]);
                rawSkill.Action_AutoAttackType = Convert.ToByte(split[19]);
                rawSkill.Action_InTown = Convert.ToByte(split[20]);
                rawSkill.Action_Range = Convert.ToUInt16(split[21]);
                rawSkill.Target_Required = Convert.ToByte(split[22]);
                rawSkill.TargetType_Animal = Convert.ToByte(split[23]);
                rawSkill.TargetType_Land = Convert.ToByte(split[24]);
                rawSkill.TargetType_Building = Convert.ToByte(split[25]);
                rawSkill.TargetGroup_Self = Convert.ToByte(split[26]);
                rawSkill.TargetGroup_Ally = Convert.ToByte(split[27]);
                rawSkill.TargetGroup_Enemy = Convert.ToByte(split[28]);
                rawSkill.TargetGroup_Neutral = Convert.ToByte(split[29]);
                rawSkill.TargetGroup_DontCare = Convert.ToByte(split[30]);
                rawSkill.TargetEtc_SelectDeadBody = Convert.ToByte(split[31]);
                //rawSkill.TargetEtc_SelectDeadBody = Convert.ToByte(split[32]);//unk
                //rawSkill.TargetEtc_SelectDeadBody = Convert.ToByte(split[33]);//unk
                rawSkill.ReqCommon_Mastery1 = Convert.ToInt32(split[34]);
                rawSkill.ReqCommon_Mastery2 = Convert.ToInt32(split[35]);
                rawSkill.ReqCommon_MasteryLevel1 = Convert.ToByte(split[36]);
                rawSkill.ReqCommon_MasteryLevel2 = Convert.ToByte(split[37]);
                rawSkill.ReqCommon_Str = Convert.ToUInt16(split[38]);
                rawSkill.ReqCommon_Int = Convert.ToUInt16(split[39]);
                rawSkill.ReqLearn_Skill1 = Convert.ToInt32(split[40]);
                rawSkill.ReqLearn_Skill2 = Convert.ToInt32(split[41]);
                rawSkill.ReqLearn_Skill3 = Convert.ToInt32(split[42]);
                rawSkill.ReqLearn_SkillLevel1 = Convert.ToByte(split[43]);
                rawSkill.ReqLearn_SkillLevel2 = Convert.ToByte(split[44]);
                rawSkill.ReqLearn_SkillLevel3 = Convert.ToByte(split[45]);
                rawSkill.ReqLearn_SP = Convert.ToInt32(split[46]);
                rawSkill.ReqLearn_Race = Convert.ToByte(split[47]);
                rawSkill.Req_Restriction1 = Convert.ToByte(split[48]);
                rawSkill.Req_Restriction2 = Convert.ToByte(split[49]);
                rawSkill.ReqCast_Weapon1 = Convert.ToByte(split[50]);
                rawSkill.ReqCast_Weapon2 = Convert.ToByte(split[51]);
                rawSkill.Consume_HP = Convert.ToUInt16(split[52]);
                rawSkill.Consume_MP = Convert.ToUInt16(split[53]);
                rawSkill.Consume_HPRatio = Convert.ToUInt16(split[54]);
                rawSkill.Consume_MPRatio = Convert.ToUInt16(split[55]);
                rawSkill.Consume_WHAN = Convert.ToByte(split[56]);
                rawSkill.UI_SkillTab = Convert.ToByte(split[57]);
                rawSkill.UI_SkillPage = Convert.ToByte(split[58]);
                rawSkill.UI_SkillColumn = Convert.ToByte(split[59]);
                rawSkill.UI_SkillRow = Convert.ToByte(split[60]);
                rawSkill.UI_IconFile = Convert.ToString(split[61]);
                rawSkill.UI_SkillName = Convert.ToString(split[62]);
                rawSkill.UI_SkillToolTip = Convert.ToString(split[63]);
                rawSkill.UI_SkillToolTip_Desc = Convert.ToString(split[64]);
                rawSkill.UI_SkillStudy_Desc = Convert.ToString(split[65]);
                rawSkill.AI_AttackChance = Convert.ToUInt16(split[66]);
                //rawSkill.Param1 = Convert.ToUInt32(split[65]);
                //rawSkill.Param2 = Convert.ToUInt32(split[66]);
                //rawSkill.Param3 = Convert.ToUInt32(split[67]);
                //rawSkill.Param4 = Convert.ToUInt32(split[68]);
                //rawSkill.Param5 = Convert.ToUInt32(split[69]);
                //rawSkill.Param6 = Convert.ToUInt32(split[70]);
                //rawSkill.Param7 = Convert.ToUInt32(split[71]);
                //rawSkill.Param8 = Convert.ToUInt32(split[72]);
                //rawSkill.Param9 = Convert.ToUInt32(split[73]);
                //rawSkill.Param10 = Convert.ToUInt32(split[74]);
                //rawSkill.Param11 = Convert.ToUInt32(split[75]);
                //rawSkill.Param12 = Convert.ToUInt32(split[76]);
                //rawSkill.Param13 = Convert.ToUInt32(split[77]);
                //rawSkill.Param14 = Convert.ToUInt32(split[78]);
                //rawSkill.Param15 = Convert.ToUInt32(split[79]);
                //rawSkill.Param16 = Convert.ToUInt32(split[80]);
                //rawSkill.Param17 = Convert.ToUInt32(split[81]);
                //rawSkill.Param18 = Convert.ToUInt32(split[82]);
                //rawSkill.Param19 = Convert.ToUInt32(split[83]);
                //rawSkill.Param20 = Convert.ToUInt32(split[84]);
                //rawSkill.Param21 = Convert.ToUInt32(split[85]);
                //rawSkill.Param22 = Convert.ToUInt32(split[86]);
                //rawSkill.Param23 = Convert.ToUInt32(split[87]);
                //rawSkill.Param24 = Convert.ToUInt32(split[88]);
                //rawSkill.Param25 = Convert.ToUInt32(split[89]);

            }
            catch (Exception ex)
            {
                string error = "[ErrorAt] " + raw + Environment.NewLine + ex.Message;
                throw new Exception(error);
            }
            return rawSkill;
        }

        public static string GenerateInsertSqlCode(RawRefSkill rawSkill)
        {
            if (rawSkill.ID <= 0) return string.Empty;
            string insert = @"INSERT INTO dbo.[_RefSkill] ( Service, ID, GroupID, Basic_Code, Basic_Name, Basic_Group, Basic_Original, Basic_Level, Basic_Activity, Basic_ChainCode, Basic_RecycleCost, Action_PreparingTime, Action_CastingTime, Action_ActionDuration, Action_ReuseDelay, Action_CoolTime, Action_FlyingSpeed, Action_Interruptable, Action_Overlap, Action_AutoAttackType, Action_InTown, Action_Range, Target_Required, TargetType_Animal, TargetType_Land, TargetType_Building, TargetGroup_Self, TargetGroup_Ally, TargetGroup_Enemy, TargetGroup_Neutral, TargetGroup_DontCare, TargetEtc_SelectDeadBody, ReqCommon_Mastery1, ReqCommon_Mastery2, ReqCommon_MasteryLevel1, ReqCommon_MasteryLevel2, ReqCommon_Str, ReqCommon_Int, ReqLearn_Skill1, ReqLearn_Skill2, ReqLearn_Skill3, ReqLearn_SkillLevel1, ReqLearn_SkillLevel2, ReqLearn_SkillLevel3, ReqLearn_SP, ReqLearn_Race, Req_Restriction1, Req_Restriction2, ReqCast_Weapon1, ReqCast_Weapon2, Consume_HP, Consume_MP, Consume_HPRatio, Consume_MPRatio, Consume_WHAN, UI_SkillTab, UI_SkillPage, UI_SkillColumn, UI_SkillRow, UI_IconFile, UI_SkillName, UI_SkillToolTip, UI_SkillToolTip_Desc, UI_SkillStudy_Desc, AI_AttackChance, Param1, Param2, Param3, Param4, Param5, Param6, Param7, Param8, Param9, Param10, Param11, Param12, Param13, Param14, Param15, Param16, Param17, Param18, Param19, Param20, Param21, Param22, Param23, Param24, Param25)
                              VALUES  (@prmValues);";
            string values = "";
            values += rawSkill.Service + ", ";
            values += rawSkill.ID + ", ";
            values += rawSkill.GroupID + ", ";
            values += "'" + rawSkill.Basic_Code + "', ";
            values += "'" + rawSkill.Basic_Name + "', ";
            values += "'" + rawSkill.Basic_Group + "', ";
            values += rawSkill.Basic_Original + ", ";
            values += rawSkill.Basic_Level + ", ";
            values += rawSkill.Basic_Activity + ", ";
            values += rawSkill.Basic_ChainCode + ", ";
            values += rawSkill.Basic_RecycleCost + ", ";
            values += rawSkill.Action_PreparingTime + ", ";
            values += rawSkill.Action_CastingTime + ", ";
            values += rawSkill.Action_ActionDuration + ", ";
            values += rawSkill.Action_ReuseDelay + ", ";
            values += rawSkill.Action_CoolTime + ", ";
            values += rawSkill.Action_FlyingSpeed + ", ";
            values += rawSkill.Action_Interruptable + ", ";
            values += rawSkill.Action_Overlap + ", ";
            values += rawSkill.Action_AutoAttackType + ", ";
            values += rawSkill.Action_InTown + ", ";
            values += rawSkill.Action_Range + ", ";
            values += rawSkill.Target_Required + ", ";
            values += rawSkill.TargetType_Animal + ", ";
            values += rawSkill.TargetType_Land + ", ";
            values += rawSkill.TargetType_Building + ", ";
            values += rawSkill.TargetGroup_Self + ", ";
            values += rawSkill.TargetGroup_Ally + ", ";
            values += rawSkill.TargetGroup_Enemy + ", ";
            values += rawSkill.TargetGroup_Neutral + ", ";
            values += rawSkill.TargetGroup_DontCare + ", ";
            values += rawSkill.TargetEtc_SelectDeadBody + ", ";
            values += rawSkill.ReqCommon_Mastery1 + ", ";
            values += rawSkill.ReqCommon_Mastery2 + ", ";
            values += rawSkill.ReqCommon_MasteryLevel1 + ", ";
            values += rawSkill.ReqCommon_MasteryLevel2 + ", ";
            values += rawSkill.ReqCommon_Str + ", ";
            values += rawSkill.ReqCommon_Int + ", ";
            values += rawSkill.ReqLearn_Skill1 + ", ";
            values += rawSkill.ReqLearn_Skill2 + ", ";
            values += rawSkill.ReqLearn_Skill3 + ", ";
            values += rawSkill.ReqLearn_SkillLevel1 + ", ";
            values += rawSkill.ReqLearn_SkillLevel2 + ", ";
            values += rawSkill.ReqLearn_SkillLevel3 + ", ";
            values += rawSkill.ReqLearn_SP + ", ";
            values += rawSkill.ReqLearn_Race + ", ";
            values += rawSkill.Req_Restriction1 + ", ";
            values += rawSkill.Req_Restriction2 + ", ";
            values += rawSkill.ReqCast_Weapon1 + ", ";
            values += rawSkill.ReqCast_Weapon2 + ", ";
            values += rawSkill.Consume_HP + ", ";
            values += rawSkill.Consume_MP + ", ";
            values += rawSkill.Consume_HPRatio + ", ";
            values += rawSkill.Consume_MPRatio + ", ";
            values += rawSkill.Consume_WHAN + ", ";
            values += rawSkill.UI_SkillTab + ", ";
            values += rawSkill.UI_SkillPage + ", ";
            values += rawSkill.UI_SkillColumn + ", ";
            values += rawSkill.UI_SkillRow + ", ";
            values += "'" + rawSkill.UI_IconFile + "', ";
            values += "'" + rawSkill.UI_SkillName + "', ";
            values += "'" + rawSkill.UI_SkillToolTip + "', ";
            values += "'" + rawSkill.UI_SkillToolTip_Desc + "', ";
            values += "'" + rawSkill.UI_SkillStudy_Desc + "', ";
            values += rawSkill.AI_AttackChance + ", ";
            values += rawSkill.Param1 + ", ";
            values += rawSkill.Param2 + ", ";
            values += rawSkill.Param3 + ", ";
            values += rawSkill.Param4 + ", ";
            values += rawSkill.Param5 + ", ";
            values += rawSkill.Param6 + ", ";
            values += rawSkill.Param7 + ", ";
            values += rawSkill.Param8 + ", ";
            values += rawSkill.Param9 + ", ";
            values += rawSkill.Param10 + ", ";
            values += rawSkill.Param11 + ", ";
            values += rawSkill.Param12 + ", ";
            values += rawSkill.Param13 + ", ";
            values += rawSkill.Param14 + ", ";
            values += rawSkill.Param15 + ", ";
            values += rawSkill.Param16 + ", ";
            values += rawSkill.Param17 + ", ";
            values += rawSkill.Param18 + ", ";
            values += rawSkill.Param19 + ", ";
            values += rawSkill.Param20 + ", ";
            values += rawSkill.Param21 + ", ";
            values += rawSkill.Param22 + ", ";
            values += rawSkill.Param23 + ", ";
            values += rawSkill.Param24 + ", ";
            values += rawSkill.Param25;

            insert = insert.Replace("@prmValues", values);

            return insert;
        }
    }
}
