using SroBasic.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class Skilltrain
    {
        public uint ID { get; set; }
        public string Name { get; set; }
        public string GroupType { get; set; }
        public ushort MPRequest { get; set; }
        public ushort CastTime { get; set; }
        public uint Cooldown { get; set; }
        public uint TemporaryID { get; set; }
        public DateTime TimeUsing { get; set; }
        public bool IsRemainInEffect { get; set; }
        public bool IsWaitingCastSkill { get; set; }
        public byte UsingType { get; set; }

        public Skilltrain()
        {
            ID = 0;
            CastTime = 0;
            Cooldown = 0;
            TemporaryID = 0;
            TimeUsing = new DateTime();
            IsRemainInEffect = false;
            IsWaitingCastSkill = false;
        }

        public Skilltrain(uint id)
        {
            ID = id;
            TemporaryID = 0;
            TimeUsing = new DateTime();
            IsRemainInEffect = false;
            IsWaitingCastSkill = false;

            var skill = MediaData.Skills[id];
            //MPRequest = skill.MPRequest;
            CastTime = skill.CastTime;
            Cooldown = skill.Cooldown;
            Name = skill.Name;
            GroupType = skill.GroupType;
            UsingType = skill.UsingType;

            var listSkills = MediaData.Skills.Where(a => a.Key == skill.ID && a.Value.GroupType == skill.GroupType).ToList();
            if (listSkills.Count > 1)
            {
                var castTime = listSkills.Sum(a => a.Value.CastTime);
                var cooldown = listSkills.Max(a => a.Value.Cooldown);
                //var mpRequest = listSkills.Max(a => a.Value.MPRequest);

                CastTime = (ushort)castTime;
                Cooldown = cooldown;
                //MPRequest = mpRequest;
            }
        }

        public Skilltrain(Skill skill)
        {

            ID = skill.ID;
            //MPRequest = skill.MPRequest;
            CastTime = skill.CastTime;
            Cooldown = skill.Cooldown;
            TemporaryID = 0;
            TimeUsing = new DateTime();
            IsRemainInEffect = false;
            IsWaitingCastSkill = false;
            Name = skill.Name;
            GroupType = skill.GroupType;
            UsingType = skill.UsingType;

            var listSkills = MediaData.Skills.Where(a => a.Key == skill.ID && a.Value.GroupType == skill.GroupType).ToList();
            if (listSkills.Count > 1)
            {
                var castTime = listSkills.Sum(a => a.Value.CastTime);
                var cooldown = listSkills.Max(a => a.Value.Cooldown);
                //var mpRequest = listSkills.Max(a => a.Value.MPRequest);

                CastTime = (ushort)castTime;
                Cooldown = cooldown;
                //MPRequest = mpRequest;
            }
        }

        public void Clear()
        {
            ID = 0;
            MPRequest = 0;
            CastTime = 0;
            Cooldown = 0;
            TemporaryID = 0;
            TimeUsing = new DateTime();
            IsRemainInEffect = false;
            IsWaitingCastSkill = false;
        }
    }
}
