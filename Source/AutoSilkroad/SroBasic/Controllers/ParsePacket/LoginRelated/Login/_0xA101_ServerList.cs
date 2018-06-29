using SilkroadSecurityApi;
using SroBasic.Component.Logic;
using SroBasic.Controllers.ThreadProxy;
using SroBasic.Models.PacketData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0xA101] Server List
    /// </summary>
    public static class _0xA101
    {
        public static List<Server> Parse(Packet packet)
        {
            List<Server> servers = new List<Server>();

            byte entry = packet.ReadUInt8();
            while (entry == 1)
            {
                byte id = packet.ReadUInt8();
                string name = packet.ReadAscii();
                entry = packet.ReadUInt8();
            }

            entry = packet.ReadUInt8();
            while (entry == 1)
            {
                ushort id = 0;
                float ratio = 0;
                string name = null;
                char country = '?';
                byte state = 0;
                ushort cur = 0;
                ushort max = 0;

                int locale = Globals.clientInfo.Locale;

                id = packet.ReadUInt16();

                if (locale == 18 || locale == 22)
                {
                    name = packet.ReadAscii();
                }
                else if (locale == 40)
                {
                    name = packet.ReadAscii(1251);
                }
                else if (locale == 2)
                {
                    name = packet.ReadAscii(949);
                }
                else if (locale == 4)
                {
                    name = packet.ReadAscii(936);
                }
                else if (locale == 23)
                {
                    name = packet.ReadAscii(936);
                }
                else if (locale == 38)
                {
                    name = packet.ReadAscii();
                }

                if (locale == 18)
                {
                    country = name[0];
                    name = name.Remove(0, 1);
                    ratio = packet.ReadSingle();
                }
                else
                {
                    cur = packet.ReadUInt16();
                    max = packet.ReadUInt16();
                }

                state = packet.ReadUInt8();

                entry = packet.ReadUInt8();

                servers.Add(new Server
                {
                    ID = id,
                    Name = name,
                    CurrentUsers = cur,
                    MaxUsers = max,
                    State = state,
                    Country = country,
                    Ratio = ratio
                });
            }

            return servers;
        }

        private static void Share(List<Server> data, bool isClientless)
        {
            if(data != null & data.Count > 0)
            {
                Views.BindingFrom.BindingServerCombobox(data);
                if (isClientless)
                {
                    var p = GeneratePacket.LoginServer(Globals.clientInfo.Locale, Globals.loginUser, Globals.loginPass, data[0].ID);
                    ProxyClientless.SendPacketToGatewayRemote(p);
                }
            }
        }

        public static void Dowork(Packet packet, bool isClientless = false)
        {
            var data = Parse(packet);
            Share(data, isClientless);
        }
    }
}
