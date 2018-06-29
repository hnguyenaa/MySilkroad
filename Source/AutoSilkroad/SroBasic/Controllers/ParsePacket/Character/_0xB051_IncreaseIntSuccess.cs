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
    /// [_0xB051] Character Increase Int
    /// </summary>
    public static class _0xB051
    {
        class Data
        {
            public bool IsIncreaseSuccess { get; set; }
        }
        private static Data Parse(Packet packet)
        {
            Data data = new Data();

            int flag = packet.ReadUInt8();
            if (flag == 0x01)
            { 
                data.IsIncreaseSuccess = true; 
            }

            return data;
            //int flast = packet.ReadUInt8();
            //if (flast == 1)
            //{
            //    if (CharData.StatPoints > 0 && BotData.autoUpStatPoints)
            //    {
            //        if (BotData.upPoint_STR)
            //        {
            //            //CharData.StatPoints -= 1;
            //            packet = new Packet(0x7050);
            //            Globals.SentPacketP_S(packet);
            //        }
            //        else if (BotData.upPoint_INT)
            //        {
            //            //CharData.StatPoints -= 1;
            //            packet = new Packet(0x7051);
            //            Globals.SentPacketP_S(packet);
            //        }
            //    }
            //}
        }
        private static void Share(Data data)
        {
            if (data.IsIncreaseSuccess)
            {
                //int statpoint = Globals.Character.StatPoint;
                //statpoint = statpoint - 1;
                //Globals.Character.StatPoint = (ushort)statpoint;

                //Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.StatPoint);
                //if (statpoint > 0)
                //{
                //    Bot.BotInput.CheckAutoIncreaseStrInt();
                //}

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
