using SilkroadSecurityApi;
using SroBasic.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.Bot
{
    
    public static class BotInput
    {
        enum BotStatus
        {
            None,
            Start,
            Stop
        }

        enum BotAction
        {
            None,
            RequestSelectMob,
            SelectSuccess,
            SelectFall,
            UsingSkill
        }


        internal static void Start()
        {
            _status = BotStatus.Start;
            SelectNextMobForAttack();
        }
        internal static void Stop()
        {
            _status = BotStatus.Stop;
        }

        static BotAction _BotAction = BotAction.None;
        static BotStatus _status = BotStatus.None;
        static System.Windows.Forms.Timer timerWaitingCastedSkill = new System.Windows.Forms.Timer();
        static System.Windows.Forms.Timer timerDelayFindMob = new System.Windows.Forms.Timer();
        static uint _mobId = 0;
        static BotInput()
        {
            timerWaitingCastedSkill.Tick += new System.EventHandler(timerWaitingCastedSkill_Elapsed);
        }

        static void timerWaitingCastedSkill_Elapsed(object sender, EventArgs e)
        {
            Packet packet = new Packet(0x7074);
            uint objectId = _mobId;
            var nextSkill = Globals.Character.GetNextSkillTrain();
            if (nextSkill != null && nextSkill.ID > 0)
            {
                if (nextSkill.UsingType != 2)
                {
                    packet = GeneratePacket.BuffSkill(nextSkill.ID);
                }
                else
                {
                    packet = GeneratePacket.AttackSkill(nextSkill.ID, objectId);
                }
            }
            else
            {
                packet = GeneratePacket.AttackNormal(objectId);
            }

            ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            timerWaitingCastedSkill.Enabled = false;
        }


        /// <summary>
        /// <para>From: _0x3015_SingleSpawn, _0x3019_GroupeSpawn</para>
        /// when mob spawn, if not select then select new mob
        /// </summary>
        public static void CheckMobSpawn()
        {
            
            //if (BotMedia.BotType == TypeOfBotWhenRun.None) return;
            //if (BotMedia.MobAttacked.Status == StatusOfMobAttacked.None ||
            //    BotMedia.MobAttacked.Status == StatusOfMobAttacked.Died)
            //{
            //    //Views.BindingView.WriteLine("[BotInput::CheckMobSpawn] call SelectNewMob()");
            //    BotOutput.SelectNewMob();
            //}
            ////else
            ////   // Views.BindingView.WriteLine("[BotInput::CheckMobSpawn][" + BotMedia.MobAttacked.Status + "] check fall");
        }

        public static void CheckMobDespawn(uint mobId)
        {
            CheckMobDie(mobId);
        }

        /// <summary>
        /// <para>From: _0x30BF_ObjectDie</para>
        /// <para>if mob die = mob attacked, select new mob</para>
        /// <para>else continue attack</para>
        /// </summary>
        /// <param name="mobID"></param>
        public static void CheckMobDie(uint mobID)
        {
            //if (BotMedia.BotType == TypeOfBotWhenRun.None) return;

            //if (BotMedia.MobAttacked.ID == mobID)
            //{
            //    BotMedia.CurrentCastSkill.Resert();
            //    BotMedia.MobAttacked.SetMobDie();

            //    //CurrentCastTimeSkill = 0;
            //    //Views.BindingView.WriteLine("[BotInput:CheckMobDie] mob die=> SelectNewMob");

            //    BotOutput.SelectNewMob();
            //}
        }

        /// <summary>
        /// <para>From: _0xB070_SkillAdd</para>
        /// kiem tra mob dang tan cong, neu chua select mob nao thi select
        /// </summary>
        /// <param name="mobId"></param>
        public static void MobAttacking(uint mobId)
        {
            if (_status == BotStatus.Start) return;

            if (_BotAction != BotAction.RequestSelectMob)
            {
                if (Metadata.Globals.MobSpawns.ContainsKey(mobId))
                {
                    if (Metadata.Globals.MobSpawns[mobId].CountAttack > 5)
                    {
                        RequestSelectMob(mobId);
                    }
                }
            }
        }


        /// <summary>
        /// <para>From: _0xB045_ObjectSelect</para>
        /// <para>Server accept client select this object</para>
        /// <para>---------------------------------------</para>
        /// <para>update CurrentAttackMobID</para>
        /// <para>update BotStatus = SelectMob</para>
        /// <para>run attack mob</para>
        /// </summary>
        /// <param name="mobID"></param>
        public static void AttackThisMob(uint mobId)
        {
            _mobId = mobId;
            timerWaitingCastedSkill_Elapsed(null, null);
        }


        /// <summary>
        /// <para>From: _0xB071_SkillCasted</para>
        /// Continue attack if mob doesn't die
        /// </summary>
        public static void MobAttackedStillAlive()
        {
            //if (BotMedia.BotType == TypeOfBotWhenRun.None) return;

            ////Views.BindingView.WriteLine("[BotInput:MobAttackedStillAlive] call ContinueAttack()");

            //BotOutput.ContinueAttack();
        }

        public static void ContinueAfterCastSkill()
        {
            BotOutput.CastSkill_Buff();
        }

        public static void SkillAdd_BeginCastBuffSkill(uint skillID)
        {
            //var skilltrain = Globals.Character.UpdateBuffSkill_BeginCastSkill(skillID);

        }

        /// <summary>
        /// <para>From: _0xB072_BuffDell</para>
        /// </summary>
        /// <param name="tempId"></param>
        public static void RepeatBuffSkill(uint tempId)
        {
            if (_status != BotStatus.Start) return;
            //if (!timerWaitingCastedSkill.Enabled)
            //{
                Packet packet = new Packet(0x7074);
                var skill = Globals.Character.GetSkillTrainByTempId(tempId);

                if (skill != null && skill.UsingType != 2)
                {
                    Views.BindingFrom.WriteLine("RepeatBuffSkill :" + skill.Name);
                    packet = GeneratePacket.BuffSkill(skill.ID);
                    ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
                }
            //}
        }


        /// <summary>
        /// <para>From: _0xB070_SkillAdd, _0xB0BD_BuffInfo</para>
        /// <para>if skill has cast time > 0 then update current casttime and time using</para>
        /// <para>If skill is attack skill, and status is select then update status = attack</para>
        /// </summary>
        /// <param name="skillID"></param>
        public static void SetCurrentSkillUsing(uint skillID)
        {
            //if (BotMedia.BotType == TypeOfBotWhenRun.None) return;

            //if (MediaData.Skills.ContainsKey(skillID))
            //{
            //    Models.Skill skill = MediaData.Skills[skillID];
            //    if (skill.CastTime > 0)
            //    {
            //        if (skill.Type.Contains("BASE")) return;
            //        // Views.BindingView.Write("[BotInput::SetCurrentSkillUsing][CastTime = " + skill.CastTime + "][attack][skill= " + skillID);

            //        //BotMedia.CurrentCastSkill.SetCastTime(skill.CastTime);

            //        if (skill.UsingType == 1)//buff
            //        {
            //            // Views.BindingView.WriteLine("[BotInput::SetCurrentSkillUsing][CastTime = " + skill.CastTime + "][buff][skill= " + skillID + "] waiting " + skill.CastTime + " cast time, then call AttackMob()");
            //            BotMedia.CurrentCastSkill.SetIsWaitingTaskDelay(true);

            //            Task.Factory.StartNew(() =>
            //            {
            //                System.Threading.Thread.Sleep(skill.CastTime);
            //                BotMedia.CurrentCastSkill.SetIsWaitingTaskDelay(false);
            //                BotOutput.AttackMob();
            //            });
            //        }
            //    }
            //    else
            //    {
            //        //if (skill.UsingType == 1 && skill.ID != Globals.Character.ImbueSkill.ID)//buff
            //        //{
            //        //    BotOutput.AttackMob();
            //        //}
            //        ////Views.BindingView.WriteLine("[BotInput::SetCurrentSkillUsing][" + skill.CastTime + "][" + skill.Type + "] don't set");
            //    }

            //    if (skill.UsingType == 2 && BotMedia.MobAttacked.Status == StatusOfMobAttacked.Selecting)
            //    {
            //        // Views.BindingView.WriteLine("[BotInput::SetCurrentSkillUsing] Update status = attack");
            //        BotMedia.MobAttacked.SetStatusAttacked();
            //    }
            //}
        }

        /// <summary>
        /// <para>From: _0x30D2_BadEffect</para>
        /// cap nhat di trang
        /// <para>Mot so di trang nhu choang, nock-down, mu se khong the tan cong, doi het di trang tan cong tiep</para>
        /// </summary>
        /// <param name="badStatus"></param>
        public static void UpdateBadStatus(bool badStatus)
        {
            if (badStatus)
            {
                BotMedia.SetBadStatus(true);
            }
            else
            {
                if (BotMedia.BadStatus)
                {
                    BotMedia.SetBadStatus(false);

                    //if (BotMedia.BotType == TypeOfBotWhenRun.None) return;
                    //BotOutput.AttackMob();
                }
            }
        }

        internal static void StartUsingSkillTrain(Models.Skilltrain skill)
        {
            if (skill.CastTime > 0 && !timerWaitingCastedSkill.Enabled)
            {
                timerWaitingCastedSkill.Interval = skill.CastTime;
                timerWaitingCastedSkill.Enabled = true;
            }
            else
            {
                timerWaitingCastedSkill_Elapsed(null, null);
            }
        }

        internal static void UpdateDistanceAllMob()
        {
            foreach (var item in Metadata.Globals.MobSpawns)
            {
                double distance = Models.Coordinate.Distance(item.Value.Coordinate, Metadata.Globals.Character.Coordinate);
                item.Value.Distance = distance;
            }
        }
        internal static void SelectNextMobForAttack()
        {
            if (_status != BotStatus.Start) return;

            var mob = GetNearMobForAttack();
            if (mob != null && mob.UniqueID > 0)
            {
                RequestSelectMob(mob.UniqueID);
            }
        }

        private static Models.MobSpawn GetNearMobForAttack()
        {
            var mob = Globals.GetMobMinDistance();


            //if (Globals.MobSpawns.Count > 0)
            //{
            //    var minDistance = Globals.MobSpawns.Min(a => a.Distance);
            //    var mob = Globals.MobSpawns.FirstOrDefault(a => a.Distance == minDistance);
            //    if (mob != null && mob.UniqueID > 0)
            //        mob.IsSelected = true;

            //    return mob;
            //}
            //else
            //{
            //    return null;
            //}

            return mob;
        }



        internal static void AutoIncreaseStatPoint()
        {
            if (_status != BotStatus.Start) return;

            if (Configs.IncreaseStatPointType == IncreaseStatPointType.FullStrength)
            {
                Packet packet = GeneratePacket.IncreaseStrength();
                ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            }
            else if (Configs.IncreaseStatPointType == IncreaseStatPointType.FullIntellect)
            {
                Packet packet = GeneratePacket.IncreaseIntellect();
                ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            }
        }

        internal static void AutoUsingZerk()
        {
            if (_status != BotStatus.Start) return;

            if (Globals.Character.Zerk == 5 && Configs.IsAutoZerk)
            {
                Packet packet = GeneratePacket.Berserk();
                ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            }
        }

        internal static void DoWork_SelectMobSuccess(uint mobId)
        {
            if (_status != BotStatus.Start) return;

            _BotAction = BotAction.SelectSuccess;
            _mobId = mobId;

            Bot.BotInput.AttackThisMob(mobId);
        }

        internal static void DoWork_SelectMobFall()
        {
            if (_status != BotStatus.Start) return;

            _BotAction = BotAction.SelectFall;
            Bot.BotInput.SelectNextMobForAttack();
        }

        private static void RequestSelectMob(uint mobId)
        {
            //Create packet  select mob
            Packet packet = new Packet(0x7045);//CLIENT_OBJECTSELECT
            packet.WriteUInt32(mobId);

            //Sent packet  select mob
            ThreadProxy.Proxy.SendPacketToAgentRemote(packet);

            //change Bot Action
            _BotAction = BotAction.RequestSelectMob;
        }

        internal static void DoWork_MobDie(uint mobId)
        {
            if (_status != BotStatus.Start) return;

            if (mobId == _mobId || mobId == 0)
            {
                Views.BindingFrom.WriteLine("[0x30BF][DoWork_MobDie] id = " + mobId);
                _mobId = 0;
                Bot.BotInput.UpdateDistanceAllMob();
                Bot.BotInput.SelectNextMobForAttack();
            }
        }

        internal static void DoWord_MobBehindObstacle()
        {
            if (_status != BotStatus.Start) return;

            if (Metadata.Globals.MobSpawns.ContainsKey(_mobId))
            {
                Metadata.Globals.MobSpawns[_mobId].IsBehindObstacle = true;
                _mobId = 0;

                Views.BindingFrom.WriteLine("[DoWord_MobBehindObstacle] BotAction = " + _BotAction.ToString());

                SroBasic.Models.MobSpawn mob = new SroBasic.Models.MobSpawn();
                if (Metadata.Globals.MobSpawns.Count > 0)
                    mob = Metadata.Globals.MobSpawns
                        .Where(a => !a.Value.IsDie && !a.Value.IsBehindObstacle)
                        .OrderBy(a => a.Value.Distance)
                        .FirstOrDefault().Value;
                if(mob != null && mob.UniqueID > 0)
                    RequestSelectMob(mob.UniqueID);
            }
        }
    }
}
