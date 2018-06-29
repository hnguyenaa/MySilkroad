using SilkroadSecurityApi;
using SroBasic.Component.Logic;
using SroBasic.Models.PacketData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0xA102] Login Reply
    /// </summary>
    public static class _0xA102
    {

        public static LoginReply Parse(Packet packet)
        {
            LoginReply loginReply = new LoginReply();

            byte result = packet.ReadUInt8();
            if (result == 0x01)
            {
                uint session = packet.ReadUInt32();
                string ip = packet.ReadAscii();
                ushort port = packet.ReadUInt16();

                loginReply = new LoginReply
                {
                    Result = result,
                    Session = session,
                    AgentRemoteEP = new IPEndPoint(IPAddress.Parse(ip), port)
                };
            }

            return loginReply;
        }

        private static void Share(LoginReply data, bool isClientless)
        {
            if(data != null)
            {
                if(data.Result == 1)
                {
                    Metadata.Globals.session = data.Session;
                    if (isClientless)
                    {
                        ThreadProxy.ProxyClientless.SetAgentRemoteEndPoint(data.AgentRemoteEP);
                        ThreadProxy.ProxyClientless.StartAgent();
                    }
                    else
                    {
                        ThreadProxy.Proxy.SetAgentRemoteEndPoint(data.AgentRemoteEP);

                        var agentLocalEP = ThreadProxy.Proxy.GetAgentLocalEndPoint();
                        var packet = GeneratePacket.LoginReply(data.Result, data.Session, agentLocalEP.Address.ToString(), (ushort)agentLocalEP.Port);
                        ThreadProxy.Proxy.SendPacketToGatewayLocal(packet);

                        ThreadProxy.Proxy.StartAgent();
                    }
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
