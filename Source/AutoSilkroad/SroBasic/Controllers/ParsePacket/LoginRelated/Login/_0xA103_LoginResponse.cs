using SilkroadSecurityApi;
using SroBasic.Controllers.ThreadProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0xA103] SERVER_AGENT_LOGIN_RESPONSE
    /// <para>Reply packet: [0x7007] CHARACTER_LISTING</para>
    /// </summary>
    public static class _0xA103
    {
        public static byte Parse(Packet packet)
        {
            return packet.ReadUInt8();
        }

        private static void Share(byte data, bool isClientless)
        {
            if (data == 1)
            {
                if (isClientless)
                {
                    Packet packet = new Packet(0x7007); ////CLIENT_CHARACTER_LISTING
                    packet.WriteUInt8(2);
                    ProxyClientless.SendPacketToAgentRemote(packet);
                }
            }
        }

        public static void DoWork(Packet packet, bool isClientless = false)
        {
            var data = Parse(packet);
            Share(data, isClientless);
        }
    }
}
