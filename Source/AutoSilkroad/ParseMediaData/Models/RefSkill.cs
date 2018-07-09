using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RefSkill
    {
        public uint ID { get; set; }
        public uint GroupID { get; set; }
        public string Basic_Code { get; set; }
        public string Basic_Group { get; set; }
        public uint Basic_Original { get; set; }
        public byte Basic_Level { get; set; }
        public byte Basic_Activity { get; set; }
        public uint Basic_ChainCode { get; set; }
        public uint Action_PreparingTime { get; set; }
        public uint Action_CastingTime { get; set; }
        public uint Action_ActionDuration { get; set; }
        public uint Action_ReuseDelay { get; set; }
        public uint Action_CoolTime { get; set; }
        public uint Action_FlyingSpeed { get; set; }
        public uint Action_Interruptable { get; set; }
        public uint Action_Overlap { get; set; }
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
        public byte ReqCast_Weapon1 { get; set; }
        public byte ReqCast_Weapon2 { get; set; }
        public ushort Consume_MP { get; set; }
        public byte UI_SkillTab { get; set; }
        public byte UI_SkillPage { get; set; }
        public byte UI_SkillColumn { get; set; }
        public byte UI_SkillRow { get; set; }
        public string UI_SkillName { get; set; }
        public ushort AI_AttackChance { get; set; }
    }
}
