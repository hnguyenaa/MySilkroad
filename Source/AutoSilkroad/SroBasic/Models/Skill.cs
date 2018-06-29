using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class Skill
    {
        public uint ID { get; private set; }
        public string HexID { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string GroupType { get; private set; }
        public byte Level { get; private set; }
        public ushort CastTime { get; private set; }
        public uint Cooldown { get; private set; }
        public byte ObjReq { get; private set; }
        public byte UsingType { get; private set; }
        public ushort Force { get; private set; }
        public ushort MP { get; private set; }

        public Skill() { }
        public Skill(uint id) 
        {
            ID = id;
            Name = "unknown";
            Type = "unknown";
            GroupType = "unknown";
            Level = 1;
            CastTime = 0;
            Cooldown = 0;
            ObjReq = 0;
            UsingType = 0;
            Force = 0;
            MP = 0;
        }
        public Skill(uint id, string name, string type, string groupType, byte level, ushort castTime,
            uint cooldown, byte objReq, byte usingType, ushort force, ushort mp)
        {
            ID = id;
            Name = name;
            Type = type;
            GroupType = groupType;
            Level = level;
            CastTime = castTime;
            Cooldown = cooldown;
            ObjReq = objReq;
            UsingType = usingType;
            Force = force;
            MP = mp;
        }

        //private static SkillUsingType IntToUsingType(int value)
        //{
        //    SkillUsingType resurt = SkillUsingType.Passive;

        //    switch (value)
        //    {
        //        case 0:
        //            resurt = SkillUsingType.Passive;
        //            break;
        //        case 1:
        //            resurt = SkillUsingType.Buff;
        //            break;
        //        case 2:
        //            resurt = SkillUsingType.Attack;
        //            break;
        //        default:
        //            resurt = SkillUsingType.Unknown;
        //            break;
        //    }

        //    return resurt;
        //}
    }
}
