using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0x30D2] Bad Effect
    /// </summary>
    public static class _0x30D2
    {
        enum BadStatusType
        {
            None,
            Choang = 0x40,
            ElectricShock = 0x04
        }
        private static List<BadStatusType> Parse(Packet packet)
        {
            List<BadStatusType> BadStatusTypes = new List<BadStatusType>();

            byte badStatus = 0;
            for (int i = 0; i < 4; i++)
            {
                badStatus = packet.ReadUInt8();
                if (badStatus == 0x40)
                {
                    BadStatusTypes.Add(BadStatusType.Choang);
                }
            }

            return BadStatusTypes;
        }
        private static void Share(List<BadStatusType> data)
        {
            //if (badStatus != null && badStatus.Count > 0)
            //{
            //    //BotController.UpdateBadStatus(true);
            //    Bot.BotInput.UpdateBadStatus(true);
            //}
            //else
            //{
            //    //BotController.UpdateBadStatus(false);
            //    Bot.BotInput.UpdateBadStatus(false);
            //}
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
