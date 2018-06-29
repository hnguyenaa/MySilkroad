using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.Bot
{
    enum TypeOfBotWhenRun
    {
        None = 0,
        Train,
        Trade
    }

    enum StatusOfMobAttacked
    {
        None = 0,
        Selecting,
        Attacked,
        WaitingSelect,
        Died
    }
    class CurrentCastSkill
    {
        public uint ID { get; set; }
        public uint TempID { get; set; }
        public int CastTime { get; set; }
        public DateTime TimeUsing { get; set; }
        public bool IsWaitingTaskDelay { get; set; }

        public CurrentCastSkill()
        {
            ID = 0;
            TempID = 0;
            CastTime = 0;
            TimeUsing = new DateTime();
            IsWaitingTaskDelay = false;
        }

        public void SetCastTime(int castTime)
        {
            CastTime = castTime;
            TimeUsing = DateTime.Now;
        }

        public void SetIsWaitingTaskDelay(bool isWait)
        {
            IsWaitingTaskDelay = isWait;
        }

        public double GetTimePass()
        {
            double timePass = -(TimeUsing.Subtract(DateTime.Now).TotalMilliseconds);
            return timePass;
        }

        public bool IsPassCastTimeSkill()
        {
            double timePass = GetTimePass();
            if (timePass >= CastTime)
                return true;
            return false;
        }

        public void Resert()
        {
            CastTime = 0;
            TimeUsing = new DateTime();
            IsWaitingTaskDelay = false;
        }
    }
    class MobAttackedManagerByBot
    {
        public uint ID { get; private set; }
        public uint IDNeedSelect { get; private set; }
        public StatusOfMobAttacked Status { get; private set; }

        public void SetMobDie()
        {
            ID = 0;
            Status = StatusOfMobAttacked.Died;
        }

        public void SetSelectMobFall()
        {
            Resert();
        }

        public void SetMobNeedSelect(uint id)
        {
            IDNeedSelect = id;
            Status = StatusOfMobAttacked.WaitingSelect;
            //Views.BindingView.WriteLine("[MobAttackedManagerByBot::SetMobNeedSelect][" + IDNeedSelect + "][" + Status + "]");
        }

        //public bool IsWaitingSelect()
        //{
        //    if (IDWaitSelect > 0 && Status == StatusOfMobAttacked.WaitingSelect)
        //        return true;

        //    return false;
        //}

        public void SetSelectMobAttacked(uint mobId)
        {

            //if (IsWaitingSelect())
            //{
            ID = mobId;
            IDNeedSelect = 0;
            Status = StatusOfMobAttacked.Selecting;
            //}
            //Views.BindingView.WriteLine("[MobAttackedManagerByBot::SetSelectMobAttacked][" + ID + "][" + Status + "]");
        }

        public void SetStatusAttacked()
        {
            Status = StatusOfMobAttacked.Attacked;
        }

        public void Resert()
        {
            //Views.BindingView.WriteLine("[MobAttackedManagerByBot::Resert]");
            ID = 0;
            IDNeedSelect = 0;
            Status = StatusOfMobAttacked.None;
        }

        public uint GetIDNeedSelect()
        {
            return IDNeedSelect;
        }
    }

    enum TypeOfUsingSkill
    {
        None = 0,
        /// <summary>
        /// waiting 0xb074
        /// </summary>
        RequestUsingSkill,
        /// <summary>
        /// accept using skill
        /// </summary>
        WaitingCastSkill,
        UsingSkillSuccess
    }

    public class Content0xB074
    {
        public uint SkillID { get; set; }

    }

    public static class BotMedia
    {
        
        //public static TypeOfBotWhenRun BotType { get; private set; }
        //public static MobAttackedManagerByBot MobAttacked { get; set; }
        //public static CurrentCastSkill CurrentCastSkill { get; set; }
        /// <summary>
        /// Bad status made character stop attack
        /// </summary>
        public static bool BadStatus { get; private set; }

        public static bool AutoIncreaseSTR { get; set; }
        public static bool AutoIncreaseINT { get; set; }

        //public static TypeOfUsingSkill UsingSkillStatus { get; set; }

        static BotMedia()
        {
            //CurrentCastSkill = new CurrentCastSkill();
        }

        public static void Start()
        {
            //BotType = TypeOfBotWhenRun.None;
            //MobAttacked = new MobAttackedManagerByBot();
            //CurrentCastSkill = new CurrentCastSkill();
            BadStatus = false;

            //BotType = TypeOfBotWhenRun.Train;
            BotOutput.SelectNewMob();
            //UsingSkillStatus = TypeOfUsingSkill.None;
        }
        public static void Stop()
        {
            //BotType = TypeOfBotWhenRun.None;
            //CurrentCastSkill.Resert();
            //MobAttacked.Resert();
        }


        public static void SetBadStatus(bool badStatus)
        {
            BadStatus = badStatus;
        }
        public static void SetAutoIncreaseSTR(bool isAuto)
        {
            AutoIncreaseSTR = isAuto;
        }
    }
}
