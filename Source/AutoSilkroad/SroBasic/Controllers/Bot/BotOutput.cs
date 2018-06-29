using SilkroadSecurityApi;
using SroBasic.Metadata;
using SroBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.Bot
{
    public static class BotOutput
    { /// <summary>
        /// find new mob, sent packet request server select mob
        /// </summary>
        public static void SelectNewMob()
        {
            //////Views.BindingFrom.Write("[Looping::SelectNewMob]");

            //if (BotMedia.MobAttacked.Status == StatusOfMobAttacked.WaitingSelect) return;

            //MobSpawn nearestMob = Globals.Spawn.GetNearestMob(Globals.Character.Coordinate);
            //if (nearestMob != null && nearestMob.UniqueID > 0)
            //{
            //    //Create packet  select mob
            //    Packet packet = new Packet(0x7045);//CLIENT_OBJECTSELECT
            //    packet.WriteUInt32(nearestMob.UniqueID);

            //    BotMedia.MobAttacked.SetMobNeedSelect(nearestMob.UniqueID);

            //    //Sent packet  select mob
            //    ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            //    //Views.BindingFrom.WriteLine("[BotOutput::SelectNewMob] sent packet select mob = " + nearestMob.UniqueID);
            //}
            ////else
            ////Views.BindingFrom.WriteLine("[BotOutput::SelectNewMob][" + nearestMob.UniqueID + "]["+BotMedia.MobAttacked.Status+"] cannot select ");
        }

        public static void SelectThisMob(uint id)
        {
            ////Create packet  select mob
            //Packet packet = new Packet(0x7045);//CLIENT_OBJECTSELECT
            //packet.WriteUInt32(id);

            //BotMedia.MobAttacked.SetMobNeedSelect(id);

            ////Sent packet  select mob
            //ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            ////Views.BindingFrom.WriteLine("[BotOutput::SelectThisMob] sent packet select mob = " + id);

        }

        public static void AttackMob()
        {
            ////Views.BindingFrom.Write("[BotOutput::AttackMob]");
            //if (BotMedia.MobAttacked.Status == StatusOfMobAttacked.Attacked ||
            //    BotMedia.MobAttacked.Status == StatusOfMobAttacked.Selecting)
            //{
            //    //buff skill
            //    bool isBuff = BuffMamager();
            //    if (!isBuff)
            //    {
            //        //using imbue skill if exist
            //        ImbueManager();

            //        //attack 
            //        AttackManager();
            //    }

            //}
        }

        public static void CastSkill_Buff()
        {
            //if (Bot.BotMedia.UsingSkillStatus == TypeOfUsingSkill.WaitingCastSkill) return;
            //Skilltrain buffSkill = Globals.Character.GetSkillReadyUsing_Buff();
            //if (buffSkill.ID > 0)
            //{
            //    //create packet buff skill
            //    Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            //    packet.WriteUInt8(1);
            //    packet.WriteUInt8(4);
            //    packet.WriteUInt32(buffSkill.ID);
            //    packet.WriteUInt8(0);

            //    BotMedia.UsingSkillStatus = TypeOfUsingSkill.RequestUsingSkill;
            //    BotMedia.CurrentCastSkill.ID = buffSkill.ID;
            //    //sent packet to buff skill
            //    ThreadProxy.Proxy.SendPacketToAgentRemote(packet);

            //    Views.BindingFrom.WriteLine("[CastSkill_Buff] RequestUsingSkill: " + buffSkill.ID);
            //}
        }

        /// <summary>
        /// <para>from: MobAttackedStillAlive()</para>
        /// Continue attack if mob doesn't die
        /// </summary>
        public static void ContinueAttack()
        {
            //////Views.BindingFrom.WriteLine("[BotOutput:ContinueAttack]");
            ////neu con trong thoi gian cast time thi doi het thoi gian cast time

            //double timePass = BotMedia.CurrentCastSkill.GetTimePass();
            ////Views.BindingFrom.Write("[BotOutput::ContinueAttack][CastTime = " + BotMedia.CurrentCastSkill.CastTime + "] timePass = " + timePass);
            //if (timePass < BotMedia.CurrentCastSkill.CastTime)
            //{
            //    int timeDelay = (BotMedia.CurrentCastSkill.CastTime - (int)timePass);
            //    //Views.BindingFrom.Write("[BotOutput::ContinueAttack] waiting " + timeDelay + " then call AttackMob()");

            //    BotMedia.CurrentCastSkill.SetIsWaitingTaskDelay(true);
            //    Task.Factory.StartNew(() =>
            //    {
            //        System.Threading.Thread.Sleep(timeDelay);
            //        BotMedia.CurrentCastSkill.SetIsWaitingTaskDelay(false);
            //        AttackMob();
            //    });
            //}
        }

        private static async Task DelayedAttackMobAsync(int timeDelay)
        {
            await Task.Delay(timeDelay);
            //BotMedia.CurrentCastSkill.SetIsWaitingTaskDelay(false);
            //AttackMob();
        }

        private static bool BuffMamager()
        {
            //////Views.BindingFrom.Write("[BotOutput::BuffMamager]");

            //Skilltrain buffSkill = Globals.Character.GetBuffSkillReadyUsing();

            //if (buffSkill.ID > 0)
            //{
            //    if (buffSkill.CastTime > 0)
            //    {
            //        //if status is waiting cast skill out
            //        if (BotMedia.CurrentCastSkill.IsWaitingTaskDelay) return false;

            //        //check waiting cast time, if wating then pass
            //        //if (!BotMedia.CurrentCastSkill.IsPassCastTimeSkill()) return false;

            //        //create packet buff skill
            //        Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            //        packet.WriteUInt8(1);
            //        packet.WriteUInt8(4);
            //        packet.WriteUInt32(buffSkill.ID);
            //        packet.WriteUInt8(0);

            //        BotMedia.UsingSkillStatus = TypeOfUsingSkill.RequestUsingSkill;
            //        //sent packet to buff skill
            //        ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            //        //Views.BindingFrom.WriteLine("[BotOutput::BuffMamager][" + skill.ID + "][" + skill .Name+ "] sent packet buff ");
            //        return true;
            //    }
            //    else
            //    {
            //        //create packet buff skill
            //        Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            //        packet.WriteUInt8(1);
            //        packet.WriteUInt8(4);
            //        packet.WriteUInt32(buffSkill.ID);
            //        packet.WriteUInt8(0);

            //        BotMedia.UsingSkillStatus = TypeOfUsingSkill.RequestUsingSkill;
            //        //sent packet to buff skill
            //        ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            //        //Views.BindingFrom.WriteLine("[BotOutput::BuffMamager][" + skill.ID + "][" + skill.Name + "] sent packet buff ");
            //        return true;
            //    }
            //}
            ////else
            ////Views.BindingFrom.WriteLine("[BotOutput::BuffMamager] not found ");

            return false;
        }

        /// <summary>
        /// imbue: nhiểm => sử dụng skill nhiểm hiệu ứng
        /// </summary> 
        private static void ImbueManager()
        {
            ////Views.BindingFrom.Write("[BotOutput::ImbueManager]");

            ////if skill pass cooldown time, using skill
            //// if not enough MP don't cast skill
            //if (Globals.Character.CheckImbueReadyUsing())
            //{
            //    //create packet buff skill
            //    Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            //    packet.WriteUInt8(1);
            //    packet.WriteUInt8(4);
            //    packet.WriteUInt32(Globals.Character.ImbueSkill.ID);
            //    packet.WriteUInt8(0);

            //    //sent packet buff skill
            //    ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            //    //Views.BindingFrom.WriteLine("[BotOutput::ImbueManager] Imbue = " + Globals.Character.ImbueSkill.ID);
            //}
            ////else
            ////Views.BindingFrom.WriteLine("[BotOutput::ImbueManager] CheckImbueReadyUsing() = false ");
        }

        private static void AttackManager()
        {
            ////if status is waiting cast skill out
            //if (BotMedia.CurrentCastSkill.IsWaitingTaskDelay) return;

            ////Views.BindingFrom.Write("[BotOutput::AttackManager]");

            //Skilltrain attackSkill = Globals.Character.GetAttackSkillReadyUsing();

            //if (attackSkill.ID > 0)
            //{
            //    ////check waiting cast time, if wating then pass
            //    //if (!BotMedia.CurrentCastSkill.IsPassCastTimeSkill()) return;

            //    //create packet attack mob
            //    Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            //    packet.WriteUInt8(1);
            //    packet.WriteUInt8(4);
            //    packet.WriteUInt32(attackSkill);
            //    packet.WriteUInt8(1);
            //    packet.WriteUInt32(BotMedia.MobAttacked.ID);

            //    //sent packet attack nomal
            //    ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            //    //Views.BindingFrom.WriteLine("[BotOutput::AttackManager] attack = " + skillID);
            //}
            //else //attack monal
            //{
            //    Packet packet = new Packet(0x7074);
            //    packet.WriteUInt8(1);
            //    packet.WriteUInt8(1);
            //    packet.WriteUInt8(1);
            //    packet.WriteUInt32(BotMedia.MobAttacked.ID);

            //    //sent packet attack nomal
            //    ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            //    //Views.BindingFrom.WriteLine("[BotOutput::AttackManager] attack nomal");
            //}

        }

        public static void RepeatBuffSkill(uint tempSkillId)
        {

            //////Views.BindingFrom.Write("[BotOutput::RepeatBuffSkill][id = " + id + "]");

            //if (Globals.Character.ImbueSkill.TemporaryID == tempSkillId)//imbue skill
            //{
            //    // if not enough MP don't cast skill
            //    //if (Globals.Character.MP < Globals.Character.ImbueSkill.MPRequest) return;

            //    // if not attack mob don't cast skill
            //    if (BotMedia.MobAttacked.Status == StatusOfMobAttacked.None ||
            //        BotMedia.MobAttacked.Status == StatusOfMobAttacked.Died ||
            //        BotMedia.MobAttacked.Status == StatusOfMobAttacked.WaitingSelect) return;

            //    Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            //    packet.WriteUInt8(1);
            //    packet.WriteUInt8(4);
            //    packet.WriteUInt32(Globals.Character.ImbueSkill.ID);
            //    packet.WriteUInt8(0);

            //    BotMedia.UsingSkillStatus = TypeOfUsingSkill.RequestUsingSkill;
            //    //sent packet buff skill
            //    ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            //    //Views.BindingFrom.WriteLine("[BotOutput::ImbueSkill] ImbueSkill = " + Globals.Character.ImbueSkill.ID);
            //    //}
            //}
            //else //buff skill
            //{
            //    Skilltrain buffSkill = Globals.Character.BuffSkills.FirstOrDefault(a => a.TemporaryID == tempSkillId);
            //    if (buffSkill != null && buffSkill.ID > 0)
            //    {
            //        if (buffSkill.CastTime > 0)
            //        {
            //            //if status is waiting cast skill out
            //            if (BotMedia.CurrentCastSkill.IsWaitingTaskDelay) return;

            //            //check waiting cast time, if wating then pass
            //            //if (!BotMedia.CurrentCastSkill.IsPassCastTimeSkill()) return;

            //            //create packet buff skill
            //            Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            //            packet.WriteUInt8(1);
            //            packet.WriteUInt8(4);
            //            packet.WriteUInt32(buffSkill.ID);
            //            packet.WriteUInt8(0);

            //            BotMedia.UsingSkillStatus = TypeOfUsingSkill.RequestUsingSkill;
            //            //sent packet to buff skill
            //            ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            //            //Views.BindingFrom.WriteLine("[BotOutput::BuffMamager] buff = " + skill.ID);
            //        }
            //        else
            //        {
            //            //create packet buff skill
            //            Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            //            packet.WriteUInt8(1);
            //            packet.WriteUInt8(4);
            //            packet.WriteUInt32(buffSkill.ID);
            //            packet.WriteUInt8(0);

            //            BotMedia.UsingSkillStatus = TypeOfUsingSkill.RequestUsingSkill;
            //            //sent packet to buff skill
            //            ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
            //            //Views.BindingFrom.WriteLine("[BotOutput::BuffMamager] buff = " + skill.ID);
            //        }
            //    }
            //    //else
            //    //Views.BindingFrom.WriteLine("[BotOutput::BuffMamager] not found ");
            //}

        }

        public static void StartIncreaseStr()
        {
            Packet packet = new Packet(0x7050);
            ThreadProxy.Proxy.SendPacketToAgentRemote(packet);
        }
    }
}
