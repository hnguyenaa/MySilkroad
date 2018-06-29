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
    /// [0xA106] Accept Version
    /// <para>Reply server packet: REQUEST_SERVER_LIST</para>
    /// </summary>
    public static class _0xA106
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
                    Packet packet = new Packet(0x6101, true); // REQUEST_SERVER_LIST
                    ProxyClientless.SendPacketToGatewayRemote(packet);
                }
            }
            else
            {
                Views.BindingFrom.WriteLine("Version not accept,please check version.");
            }
        }

        public static void DoWork(Packet packet, bool isClientless = false)
        {
            var data = Parse(packet);
            Share(data, isClientless);
        }
    }
}
