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
    /// [_0x3054] Character Level Up
    /// </summary>
    public static class _0x3054
    {
        class Data
        {
            public bool IsLevelUp { get; set; }
        }

        private static Data Parse(Packet packet)
        {
            var data = new Data();

            uint objectID = packet.ReadUInt32();
            if (objectID == Globals.Character.UniqueID)
            {
                data.IsLevelUp = true;
            }

            return data;
        }

        private static void Share(Data data)
        {
            if (data.IsLevelUp)
            {
                //ushort statPont = (ushort)((int)Globals.Character.StatPoint + 3);
                //Globals.Character.StatPoint = statPont;

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
