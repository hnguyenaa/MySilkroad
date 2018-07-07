using SilkroadSecurityApi;
using SroBasic.Controllers.ParsePacket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ThreadProxy
{
    static class ProxyClientless
    {
        //public static bool manualLogin = true;
        //public static bool alreadyLoggedIn = false;

        static TcpClient gw_remote_client;
        public static Security gw_remote_security;
        static NetworkStream gw_remote_stream;
        static TransferBuffer gw_remote_recv_buffer;
        static List<Packet> gw_remote_recv_packets;
        static List<KeyValuePair<TransferBuffer, Packet>> gw_remote_send_buffers;


        static TcpClient ag_remote_client;
        public static Security ag_remote_security;
        static NetworkStream ag_remote_stream;
        static TransferBuffer ag_remote_recv_buffer;
        static List<Packet> ag_remote_recv_packets;
        static List<KeyValuePair<TransferBuffer, Packet>> ag_remote_send_buffers;

        

        static object exit_lock = new object();
        public static bool should_exit = false;

        //static uint id;
        //static string xfer_remote_ip;
        //static int xfer_remote_port;
        //static bool haveAllServers = false;

        //public static bool groupspawn = false;
        //public static bool groupspawnIsSpawn = false;
        //public static int spawnAmount;

        static IPEndPoint _gatewayRemoteEP;
        static IPEndPoint _agentRemoteEP;

        private static Thread _gatewayThread;
        public static Thread _agentThread;

        public static void SetGatewayRemoteEndPoint(IPEndPoint gatewayRemoteEP)
        {
            _gatewayRemoteEP = gatewayRemoteEP;
        }

        public static void SetAgentRemoteEndPoint(IPEndPoint agentRemoteEP)
        {
            _agentRemoteEP = agentRemoteEP;
        }

        public static void StartGateway()
        {
            if(_gatewayRemoteEP != null)
            {
                _gatewayThread = new Thread(GatewayThread);
                _gatewayThread.Start();
            }
            
        }
        public static void StartAgent()
        {
            if (_agentRemoteEP != null)
            {
                _agentThread = new Thread(AgentThread);
                _agentThread.Start();
                _agentThread.Join();
            }

        }

        public static void StopGateway()
        {
            if (!should_exit)
            {
                should_exit = true;
            }
        }
        public static void StopAgent()
        {
            if (!should_exit)
            {
                should_exit = true;
            }
        }

        public static void SendPacketToGatewayRemote(Packet packet)
        {
            if (gw_remote_security != null)
                gw_remote_security.Send(packet);
        }

        public static void SendPacketToAgentRemote(Packet packet)
        {
            if (ag_remote_security != null)
                ag_remote_security.Send(packet);
        }

        enum TransferType
        {
            StoP,
            PtoS,
            CtoP,
            PtoC
        }

        enum ProxyType
        {
            GW_RM,
            GW_LC,
            AG_RM,
            AG_LC
        }

        private static void PrintDebugPacket(Packet packet, TransferType transferType, ProxyType proxyType)
        {
            byte[] packet_bytes = packet.GetBytes();
            string debug = String.Format("[{0:X4}][{1} bytes]{2}{3}{4}{5}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes));

            string header = "";
            if (transferType == TransferType.CtoP) header = "[C->P]";
            else if (transferType == TransferType.PtoC) header = "[P->C]";
            else if (transferType == TransferType.StoP) header = "[S->P]";
            else if (transferType == TransferType.PtoS) header = "[P->S]";

            if (proxyType == ProxyType.GW_RM) header += "[GW_RM]";
            else if (proxyType == ProxyType.GW_LC) header += "[GW_LC]";
            else if (proxyType == ProxyType.AG_RM) header += "[AG_RM]";
            else if (proxyType == ProxyType.AG_LC) header += "[AG_LC]";

            debug = header + debug;
            Views.BindingFrom.WriteLine(debug);
        }

        static void GatewayRemoteThread()
        {
            try
            {
                while (true)
                {
                    lock (exit_lock)
                    {
                        if (should_exit)
                        {
                            break;
                        }
                    }

                    if (gw_remote_stream.DataAvailable)
                    {
                        gw_remote_recv_buffer.Offset = 0;
                        gw_remote_recv_buffer.Size = gw_remote_stream.Read(gw_remote_recv_buffer.Buffer, 0, gw_remote_recv_buffer.Buffer.Length);
                        gw_remote_security.Recv(gw_remote_recv_buffer);
                    }

                    gw_remote_recv_packets = gw_remote_security.TransferIncoming();
                    if (gw_remote_recv_packets != null)
                    {
                        foreach (Packet packet in gw_remote_recv_packets)
                        {
                            //print debug packet
                            PrintDebugPacket(packet, TransferType.StoP, ProxyType.GW_RM);

                            // Do not pass through these packets.
                            if (packet.Opcode == 0x5000 || packet.Opcode == 0x9000)
                            {
                                continue;
                            }

                            PacketManagerClientless.Manager(packet);
                        }
                    }

                    gw_remote_send_buffers = gw_remote_security.TransferOutgoing();
                    if (gw_remote_send_buffers != null)
                    {
                        foreach (var kvp in gw_remote_send_buffers)
                        {
                            //print debug packet
                            PrintDebugPacket(kvp.Value, TransferType.PtoS, ProxyType.GW_RM);

                            TransferBuffer buffer = kvp.Key;
                            gw_remote_stream.Write(buffer.Buffer, 0, buffer.Size);
                        }
                    }

                    Thread.Sleep(1);
                }
            }
            catch (System.Exception ex)
            {
                Views.BindingFrom.WriteLine(String.Format("[GatewayRemoteThread] Exception: {0}" + Environment.NewLine, ex));
            }
        }

        static void GatewayThread()
        {
            try
            {
                gw_remote_security = new Security();
                gw_remote_recv_buffer = new TransferBuffer(4096, 0, 0);

                gw_remote_client = new TcpClient();


                Views.BindingFrom.WriteLine(String.Format("[GW_RM] Connecting to Server...(gw) | {0} : {1} | {2}" + Environment.NewLine, _gatewayRemoteEP.Address, _gatewayRemoteEP.Port, _gatewayRemoteEP));
                gw_remote_client.Connect(_gatewayRemoteEP);

                Views.BindingFrom.WriteLine("[GW_RM] Connected to Server!(gw2)" + Environment.NewLine);

                gw_remote_stream = gw_remote_client.GetStream();

                Thread remote_thread = new Thread(GatewayRemoteThread);
                remote_thread.Start();
                remote_thread.Join();
            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine(String.Format("[GatewayThread] Exception: {0}", ex));
            }
        }


        static void AgentRemoteThread()
        {
            try
            {
                while (true)
                {
                    lock (exit_lock)
                    {
                        if (should_exit)
                        {
                            break;
                        }
                    }

                    if (ag_remote_stream.DataAvailable)
                    {
                        ag_remote_recv_buffer.Offset = 0;
                        ag_remote_recv_buffer.Size = ag_remote_stream.Read(ag_remote_recv_buffer.Buffer, 0, ag_remote_recv_buffer.Buffer.Length);
                        ag_remote_security.Recv(ag_remote_recv_buffer);
                    }

                    ag_remote_recv_packets = ag_remote_security.TransferIncoming();
                    if (ag_remote_recv_packets != null)
                    {
                        foreach (Packet packet in ag_remote_recv_packets)
                        {
                            //print debug packet
                            PrintDebugPacket(packet, TransferType.StoP, ProxyType.GW_RM);

                            // Do not pass through these packets.
                            if (packet.Opcode == 0x5000 || packet.Opcode == 0x9000)
                            {
                                continue;
                            }

                            PacketManagerClientless.Manager(packet);
                            #region
                            //Packet p;
                            //switch (packet.Opcode)
                            //{
                            //    #region Login //ok
                            //    case 0x2001://ok
                            //        #region AGENT_SERVER = 0x2001
                            //        string text = packet.ReadAscii();
                            //        if (text == "AgentServer")
                            //        {
                            //            var packetLoginRequest = GeneratePacket.LoginRequest(Globals.clientInfo.Locale, Globals.loginUser, Globals.loginPass, Globals.session);
                            //            ag_remote_security.Send(packetLoginRequest); // difference: this seds the packts that came from client to the server(remote)
                            //            //continue;
                            //        }
                            //        #endregion
                            //        break;
                            //    case 0xA103://ok
                            //        #region SERVER_AGENT_LOGIN_RESPONSE = 0xA103
                            //        if (packet.ReadUInt8() == 1)
                            //        {
                            //            Packet response = new Packet(0x7007);//CLIENT_CHARACTERLISTING = 0x7007
                            //            response.WriteUInt8(2);
                            //            ag_remote_security.Send(response);
                            //            //continue;                           //important so we can see chars #2
                            //        }
                            //        #endregion
                            //        break;
                            //    case 0xB007://ok
                            //        #region CLIENT_AGENT_CHARACTER_SELECTION_REQUEST = 0xB007

                            //        //List<string> characters = new List<string>();
                            //        //SroBasicAuto.Logic.Login.GetCharacterList listChar = new Logic.Login.GetCharacterList();
                            //        //characters = listChar.CharacterList(packet);
                            //        //Globals.frmMain.listBox_char_Box.Invoke(
                            //        //    (MethodInvoker)(() => { Globals.frmMain.listBox_char_Box.Items.Clear(); }));
                            //        //foreach (string sc in characters)
                            //        //{
                            //        //    Globals.frmMain.listBox_char_Box.Invoke(
                            //        //       (MethodInvoker)(() => { Globals.frmMain.listBox_char_Box.Items.Add(sc); }));
                            //        //}
                            //        //Globals.frmMain.btn_selectChar.SetPropertyValue(a => a.Enabled, true);
                            //        //continue;                       //this is the right important to see char
                            //        #endregion
                            //        break;
                            //    case 0x3013://ok
                            //        #region SERVER_AGENT_CHARACTER_DATA = 0x3013;
                            //        //f1.Radar_refr.Start();
                            //        //Globals.log("Sent  Character.char_Packet = packet;");
                            //        //CharData.char_Packet = packet;

                            //        #endregion
                            //        break;
                            //    case 0xB624:
                            //        if (packet.ReadUInt8() == 1)
                            //        {
                            //            //Globals.frmMain.btn_sendCaptcha.SetPropertyValue(a => a.Enabled, true);
                            //        }
                            //        break;
                            //    #endregion

                            //}
                            #endregion
                        }
                    }

                    ag_remote_send_buffers = ag_remote_security.TransferOutgoing();
                    if (ag_remote_send_buffers != null)
                    {
                        foreach (var kvp in ag_remote_send_buffers)
                        {
                            //print debug packet
                            PrintDebugPacket(kvp.Value, TransferType.PtoS, ProxyType.GW_RM);

                            TransferBuffer buffer = kvp.Key;
                            ag_remote_stream.Write(buffer.Buffer, 0, buffer.Size);
                        }
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine(String.Format("[AgentRemoteThread] Exception: {0}" + Environment.NewLine, ex));
            }
        }
        static void AgentThread()
        {
            try
            {
                ag_remote_security = new Security();
                ag_remote_recv_buffer = new TransferBuffer(4096, 0, 0);

                ag_remote_client = new TcpClient();


                //Views.BindingFrom.WriteLine(String.Format("[AG_RM] Connecting to {0}:{1}  (ag)" + Environment.NewLine, xfer_remote_ip, xfer_remote_port));
                //ag_remote_client.Connect(xfer_remote_ip, xfer_remote_port);
                Views.BindingFrom.WriteLine(String.Format("[AG_RM] Connecting to {0}:{1}  (ag)" + Environment.NewLine, _agentRemoteEP.Address, _agentRemoteEP.Port));
                ag_remote_client.Connect(_agentRemoteEP);
                Views.BindingFrom.WriteLine(String.Format("[AG_RM] The connection has been made!(ag2)" + Environment.NewLine));

                ag_remote_stream = ag_remote_client.GetStream();

                Thread remote_thread = new Thread(AgentRemoteThread);
                remote_thread.Start();

                remote_thread.Join();


            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine(String.Format("[AgentThread] Exception: {0}" + Environment.NewLine, ex));
            }
        }
    }
}
