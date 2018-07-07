using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0x2001] Identify
    /// </summary>
    public class _0x2001
    {
        public static string Parse(Packet packet)
        {
            return packet.ReadAscii();
        }

        private static void Share(string data, bool isClientless)
        {
            if (string.IsNullOrEmpty(data)) return;

            byte local = Metadata.Configs.ClientConfig.Locale;
            uint version = Metadata.Configs.ClientConfig.Version;

            if(data == "GatewayServer")
            {
                if (isClientless)
                {
                    var packet = GeneratePacket.AcceptConnect(local, version);
                    ThreadProxy.ProxyClientless.SendPacketToGatewayRemote(packet);
                }
            }
            else if (data == "AgentServer")
            {
                if (isClientless)
                {
                    var user = Metadata.Configs.LoginConfig.Username;
                    var pass = Metadata.Configs.LoginConfig.Password;
                    var packet = GeneratePacket.LoginRequest(local, user, pass, Metadata.Globals.session);
                    ThreadProxy.ProxyClientless.SendPacketToAgentRemote(packet); // difference: this seds the packts that came from client to the server(remote)
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
