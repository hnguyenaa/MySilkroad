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
    /// [0xA100] Check Version
    /// </summary>
    public static class _0xA100
    {
        public static byte Parse(Packet packet)
        {
            return packet.ReadUInt8();
        }

        private static void Share(byte data, bool isClientless)
        {
            if(data == 1)
            {
                if (isClientless)
                {
                    Packet packet = new Packet(0x6106, true); //Reply Version Success
                    ProxyClientless.SendPacketToGatewayRemote(packet);
                }
            }
            else if(data == 2)
            {
                Views.BindingFrom.WriteLine("Version not correct, need update.");
            }
        }

        public static void DoWork(Packet packet, bool isClientless = false)
        {
            var data = Parse(packet);
            Share(data, isClientless);
        }
    }
}
