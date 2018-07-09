using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class Character : SroBasic.Models.PacketData.CharacterData
    {
        public uint UniqueID { get; set; }
        public uint MaxHP { get; set; }
        public uint MaxMP { get; set; }

        public List<Skilltrain> SkillTains { get; set; }

        public Character() 
        {
            SkillTains = new List<Skilltrain>();
        }
        public Character(SroBasic.Models.PacketData.CharacterData characterData)
        {
            ID = characterData.ID;
            Name = characterData.Name;
            Level = characterData.Level;
            StatPoint = characterData.StatPoint;
            Zerk = characterData.Zerk;
            HP = characterData.HP;
            MP = characterData.MP;
            MaxHP = characterData.HP;
            MaxMP = characterData.MP;

            TotalInventorySlot = characterData.TotalInventorySlot;
            TotalItem = characterData.TotalItem;
            Inventories = characterData.Inventories;

            Skills = characterData.Skills;

            Coordinate = characterData.Coordinate;
            WalkSpeed = characterData.WalkSpeed;
            RunSpeed = characterData.RunSpeed;
            ZerkSpeed = characterData.ZerkSpeed;

            SkillTains = new List<Skilltrain>();
        }

        /// <summary>
        /// set this skill to using skill list, this list using for loop bot
        /// </summary>
        /// <param name="skillId"></param>
        public void SelectSkillTrain(uint skillId)
        {
            Skill skill = Skills.FirstOrDefault(a => a.ID == skillId);
            if (skill != null && skill.ID > 0)
            {
                if (SkillTains.Count(a => a.ID == skill.ID) == 0)
                {
                    string group = skill.GroupType.Substring(0, skill.GroupType.Length - 1);
                    if (SkillTains.Exists(a => a.GroupType.Contains(group)))
                    {
                        var temp = SkillTains.FirstOrDefault(x => x.GroupType.Contains(group));
                        SkillTains.Remove(temp);
                    }
                    SkillTains.Add(new Skilltrain(skill));
                }
            }
        }

        public void DeselectSkillTrain(uint skillId)
        {
            var skill = SkillTains.FirstOrDefault(a => a.ID == skillId);
            SkillTains.Remove(skill);
        }

        public void SkillUpdate(Skill newSkill)
        {
            Skill oldSkill = Skills.FirstOrDefault(a => a.GroupType == newSkill.GroupType);
            if (oldSkill != null && oldSkill.ID > 0)
            {
                Skills.Add(newSkill);
                Skills.Remove(oldSkill);

                Skilltrain oldSkilltrain = SkillTains.FirstOrDefault(a => a.ID == oldSkill.ID);
                if (oldSkilltrain != null && oldSkilltrain.ID > 0)
                {
                    Skilltrain newSkilltrain = new Skilltrain(newSkill);
                    newSkilltrain.TimeUsing = oldSkilltrain.TimeUsing;

                    oldSkilltrain = newSkilltrain;
                }
            }
            else
            {
                Skills.Add(newSkill);
            }
        }

        //public Skilltrain UpdateBuffSkill_BeginCastSkill(uint buffSkillID)
        //{
        //    if (buffSkillID == ImbueSkill.ID && ImbueSkill.ID > 0) //imbue
        //    {
        //        ImbueSkill.TimeUsing = DateTime.Now;
        //        ImbueSkill.IsRemainInEffect = true;

        //        return ImbueSkill;
        //    }
        //    else //buff
        //    {
        //        var buffSkill = BuffSkills.FirstOrDefault(a => a.ID == buffSkillID);
        //        if (buffSkill != null && buffSkill.ID > 0)
        //        {
        //            buffSkill.TimeUsing = DateTime.Now;
        //            buffSkill.IsRemainInEffect = true;
        //            buffSkill.IsWaitingCastSkill = true;
        //        }

        //        return buffSkill;
        //    }
        //}

        public Skilltrain UsingSkill(uint skillId, uint tempId)
        {
            Skilltrain result = new Skilltrain();
            for (int i = 0; i < SkillTains.Count; i++)
            {
                if (SkillTains[i].ID == skillId)
                {
                    SkillTains[i].TemporaryID = tempId;
                    SkillTains[i].TimeUsing = DateTime.Now;

                    result = SkillTains[i];
                    Views.BindingFrom.WriteLine("[Character]::[StartUsingSkillTrain] :" + SkillTains[i].ID + "|" + SkillTains[i].Name + "|" + tempId);
                    break;
                }
            }
                //foreach (var item in SkillTains)
                //{
                //    if (item.ID == skillId)
                //    {
                //        Views.BindingFrom.WriteLine("[Character]::[StartUsingSkillTrain] :" + item.ID + "|" + item.Name + "|" + tempSkillId);
                //        item.TemporaryID = tempSkillId;
                //        item.TimeUsing = DateTime.Now;

                //        return item;
                //    }
                //}

            return result;
        }


        public Skilltrain GetNextSkillTrain()
        {
            Skilltrain skill = new Skilltrain();

            var buffList = this.SkillTains.Where(a => a.UsingType == 1 && !a.Type.Contains("_GIGONGTA_")).OrderBy(a=>a.CastTime).ToList();

            if (buffList != null && buffList.Count > 0)
            {
                foreach (var item in buffList)
                {
                    if (item.MPRequest < MP && item.TemporaryID == 0)
                    {
                        double timePass = DateTime.Now.Subtract(item.TimeUsing).TotalMilliseconds;
                        uint timeWatting = item.Cooldown + item.CastTime;
                        if (timePass > timeWatting)
                        {
                            return item;
                        }
                    }
                }
            }

            var imbueList = this.SkillTains.Where(a => a.UsingType == 1 && a.Type.Contains("_GIGONGTA_")).ToList();

            foreach (var item in imbueList)
            {
                if (item.MPRequest < MP && item.TemporaryID == 0)
                {
                    double timePass = DateTime.Now.Subtract(item.TimeUsing).TotalMilliseconds;
                    uint timeWatting = item.Cooldown + item.CastTime;
                    if (timePass > timeWatting)
                    {
                        return item;
                    }
                }
            }

            var attackList = this.SkillTains.Where(a => a.UsingType == 2).OrderBy(a=>a.CastTime).ToList();

            foreach (var item in attackList)
            {
                if (item.MPRequest < MP)
                {
                    double timePass = DateTime.Now.Subtract(item.TimeUsing).TotalMilliseconds;
                    uint timeWatting = item.Cooldown + item.CastTime;

                    //Views.BindingFrom.WriteLine(item.Name + "|" + timeWatting + "|" + timePass);
                    if (timePass > timeWatting)
                    {
                        return item;
                    }
                }
            }


            return skill;
        }

        public Skilltrain GetSkillTrainByTempId(uint tempSkillID)
        {
            Skilltrain skill = SkillTains.FirstOrDefault(a => a.TemporaryID == tempSkillID);
            if (skill != null && skill.ID > 0)
            {
                if (skill.MPRequest > MP)
                { skill = new Skilltrain(); }
            }

            return skill;
        }

        internal void RefreshBuffSkill(uint tempId)
        {
            for (int i = 0; i < SkillTains.Count; i++)
            {
                if (SkillTains[i].TemporaryID == tempId)
                {
                    SkillTains[i].TemporaryID = 0;
                    break;
                }
            }
        }
    }
}
