using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.Bot
{
    class BotManager
    {
        public void Start()
        {
            Controllers.Bot.BotMedia.Start();
        }

        public void Stop()
        {
            Controllers.Bot.BotMedia.Stop();
        }

        public void SetAutoIncreaseSTR(bool isAuto)
        {
            Controllers.Bot.BotMedia.SetAutoIncreaseSTR(isAuto);
        }

        public void RunIncrease()
        {
            if (Bot.BotMedia.AutoIncreaseSTR)
            {
                Bot.BotOutput.StartIncreaseStr();
            }
        }

        public void RunBuffSkill()
        {
            Bot.BotOutput.CastSkill_Buff();
        }
    }
}
