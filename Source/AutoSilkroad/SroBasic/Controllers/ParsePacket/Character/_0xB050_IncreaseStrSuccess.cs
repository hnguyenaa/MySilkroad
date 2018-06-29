using SilkroadSecurityApi;
using SroBasic.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0xB050] In create strength
    /// </summary>
    public static class _0xB050
    {
        class Data
        {
            public bool IsIncreaseSuccess { get; set; }
        }
        private static Data Parse(Packet packet)
        {
            var data = new Data();

            int flag = packet.ReadUInt8();
            if (flag == 0x01)
            {
                data.IsIncreaseSuccess = true;
            }

            return data;
        }
        private static void Share(Data data)
        {
            if (data.IsIncreaseSuccess)
            {
                //int statpoint = Globals.Character.StatPoint;
                //statpoint = statpoint - 1;
                //Globals.Character.StatPoint = (ushort)statpoint;

                //Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.StatPoint);

                Bot.BotInput.AutoIncreaseStatPoint();
            }
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
