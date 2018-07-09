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
    static class Proxy
    {
        static TcpListener gw_local_server;
        static TcpClient gw_local_client;
        static Security gw_local_security;
        static NetworkStream gw_local_stream;
        static TransferBuffer gw_local_recv_buffer;
        static List<Packet> gw_local_recv_packets;
        static List<KeyValuePair<TransferBuffer, Packet>> gw_local_send_buffers;
        static TcpClient gw_remote_client;
        static Security gw_remote_security;
        static NetworkStream gw_remote_stream;
        static TransferBuffer gw_remote_recv_buffer;
        static List<Packet> gw_remote_recv_packets;
        static List<KeyValuePair<TransferBuffer, Packet>> gw_remote_send_buffers;

        static TcpListener ag_local_server;
        static TcpClient ag_local_client;
        static Security ag_local_security;
        static NetworkStream ag_local_stream;
        static TransferBuffer ag_local_recv_buffer;
        static List<Packet> ag_local_recv_packets;
        static List<KeyValuePair<TransferBuffer, Packet>> ag_local_send_buffers;
        static TcpClient ag_remote_client;
        static Security ag_remote_security;
        static NetworkStream ag_remote_stream;
        static TransferBuffer ag_remote_recv_buffer;
        static List<Packet> ag_remote_recv_packets;
        static List<KeyValuePair<TransferBuffer, Packet>> ag_remote_send_buffers;

        

        static object exit_lock = new object();
        public static bool should_exit = false;

        static IPEndPoint _gatewayLocalEP;
        static IPEndPoint _gatewayRemoteEP;

        static IPEndPoint _agentLocalEP;
        static IPEndPoint _agentRemoteEP;

        private static Thread _gatewayThread;
        public static Thread _agentThread;

        public static void SetGatewayLocalEndPoint(IPEndPoint gatewayLocalEP)
        {
            _gatewayLocalEP = gatewayLocalEP;
        }
        public static void SetGatewayRemoteEndPoint(IPEndPoint gatewayRemoteEP)
        {
            _gatewayRemoteEP = gatewayRemoteEP;
        }

        public static void SetAgentLocalEndPoint(IPEndPoint agentLocalEP)
        {
            _agentLocalEP = agentLocalEP;
        }
        public static void SetAgentRemoteEndPoint(IPEndPoint agentRemoteEP)
        {
            _agentRemoteEP = agentRemoteEP;
        }

        public static IPEndPoint GetAgentLocalEndPoint()
        {
            return _agentLocalEP;
        }

        public static void StartGateway()
        {
            if(_gatewayRemoteEP != null)
            {
                _gatewayThread = new Thread(GatewayThread);
                _gatewayThread.Start();
                //_gatewayThread.IsBackground = true;
            }
            
        }
        public static void StartAgent()
        {
            if (_agentRemoteEP != null)
            {
                _agentThread = new Thread(AgentThread);
                _agentThread.Start();
                //_agentThread.IsBackground = true;
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
        public static void SendPacketToGatewayLocal(Packet packet)
        {
            if (gw_local_security != null)
                gw_local_security.Send(packet);
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
            if (packet.Opcode == 0x2002) return;
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
                            //PrintDebugPacket(packet, TransferType.StoP, ProxyType.GW_RM);

                            // Do not pass through these packets.
                            if (packet.Opcode == 0x5000 || packet.Opcode == 0x9000)
                            {
                                continue;
                            }

                            PacketManager.Manager(packet);

                            if (packet.Opcode == 0xA102)
                            {
                                continue;
                            }

                            gw_local_security.Send(packet);
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
        static void GatewayLocalThread()
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

                    if (gw_local_stream.DataAvailable)
                    {
                        gw_local_recv_buffer.Offset = 0;
                        gw_local_recv_buffer.Size = gw_local_stream.Read(gw_local_recv_buffer.Buffer, 0, gw_local_recv_buffer.Buffer.Length);
                        gw_local_security.Recv(gw_local_recv_buffer);
                    }

                    gw_local_recv_packets = gw_local_security.TransferIncoming();
                    if (gw_local_recv_packets != null)
                    {
                        #region gw_local_recv_packets
                        gw_local_recv_packets.ForEach(delegate(Packet packet)
                        {
                            byte[] packet_bytes = packet.GetBytes();

                            // Do not pass through these packets.
                            if (packet.Opcode == 0x5000 || packet.Opcode == 0x9000 || packet.Opcode == 0x2001)
                            {
                                return;
                            }

                            //print log
                            PrintDebugPacket(packet, TransferType.CtoP, ProxyType.GW_LC);

                            gw_remote_security.Send(packet);

                        });
                        #endregion
                    }

                    #region gw_local_send_buffers
                    gw_local_send_buffers = gw_local_security.TransferOutgoing();
                    if (gw_local_send_buffers != null)
                    {
                        gw_local_send_buffers.ForEach(delegate(KeyValuePair<TransferBuffer, Packet> kvp)
                        {
                            Packet packet = kvp.Value;
                            TransferBuffer buffer = kvp.Key;

                            //byte[] packet_bytes = packet.GetBytes();

                            //Print log
                            //if (!IgnoreOpcodes.Contains(packet.Opcode))
                                //Views.BindingFrom.WriteLine(String.Format("[P->C][GW_LC][{0:X4}][{1} bytes]{2}{3}{4}{5}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes)));
                            //PrintDebugPacket(packet, TransferType.PtoC, ProxyType.GW_LC);
                            gw_local_stream.Write(buffer.Buffer, 0, buffer.Size);
                        });
                    }
                    #endregion

                    Thread.Sleep(1);
                }
            }
            catch (System.Exception ex)
            {
                Views.BindingFrom.Write("[ERROR] [GatewayProxy::GatewayLocalThread]");
                Views.BindingFrom.WriteLine(ex.Message);
            }
        }
        static void GatewayThread()
        {
            try
            {
                gw_local_security = new Security();
                gw_local_security.GenerateSecurity(true, true, true);
                gw_local_recv_buffer = new TransferBuffer(4096, 0, 0);

                gw_remote_security = new Security();
                gw_remote_recv_buffer = new TransferBuffer(4096, 0, 0);

                gw_local_server = new TcpListener(_gatewayLocalEP);
                gw_local_server.Start();
                Views.BindingFrom.WriteLine(String.Format("[GW_LC] Waiting for Client (gw1):  {0}  {1} ", _gatewayLocalEP, Environment.NewLine));
                gw_local_client = gw_local_server.AcceptTcpClient();
                Views.BindingFrom.WriteLine("[GW_LC] Client Connected" + Environment.NewLine);
                gw_local_server.Stop();

                gw_remote_client = new TcpClient();
                Views.BindingFrom.WriteLine(String.Format("[GW_RM] Connecting to Server...(gw) | {0} : {1} | {2}" + Environment.NewLine, _gatewayRemoteEP.Address, _gatewayRemoteEP.Port, _gatewayRemoteEP));
                gw_remote_client.Connect(_gatewayRemoteEP);

                Views.BindingFrom.WriteLine("[GW_RM] Connected to Server!(gw2)" + Environment.NewLine);

                gw_local_stream = gw_local_client.GetStream();
                gw_remote_stream = gw_remote_client.GetStream();

                Thread remote_thread = new Thread(GatewayRemoteThread);
                remote_thread.Start();

                Thread local_thread = new Thread(GatewayLocalThread);
                local_thread.Start();

                remote_thread.Join();
                local_thread.Join();
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
                            //PrintDebugPacket(packet, TransferType.StoP, ProxyType.GW_RM);

                            // Do not pass through these packets.
                            if (packet.Opcode == 0x5000 || packet.Opcode == 0x9000)
                            {
                                continue;
                            }

                            PacketManager.Manager(packet);

                            ag_local_security.Send(packet);
                        }
                    }

                    ag_remote_send_buffers = ag_remote_security.TransferOutgoing();
                    if (ag_remote_send_buffers != null)
                    {
                        foreach (var kvp in ag_remote_send_buffers)
                        {
                            //print debug packet
                            //PrintDebugPacket(kvp.Value, TransferType.PtoS, ProxyType.GW_RM);

                            TransferBuffer buffer = kvp.Key;
                            try
                            {
                                ag_remote_stream.Write(buffer.Buffer, 0, buffer.Size);
                            }
                            catch (Exception ex)
                            {
                                PrintDebugPacket(kvp.Value, TransferType.PtoS, ProxyType.GW_RM);
                                Views.BindingFrom.WriteLine(String.Format("[AgentRemoteThread][ag_remote_stream] Exception: {0}" + Environment.NewLine, ex));
                                
                            }
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
        static void AgentLocalThread()
        {
            try
            {
                while (true)
                {
                    lock (exit_lock)
                    {
                        if (should_exit)
                        { break; }
                    }

                    if (ag_local_stream.DataAvailable)
                    {
                        ag_local_recv_buffer.Offset = 0;
                        ag_local_recv_buffer.Size = ag_local_stream.Read(ag_local_recv_buffer.Buffer, 0, ag_local_recv_buffer.Buffer.Length);
                        ag_local_security.Recv(ag_local_recv_buffer);
                    }

                    ag_local_recv_packets = ag_local_security.TransferIncoming();
                    if (ag_local_recv_packets != null)
                    {
                        #region ag_local_recv_packets
                        ag_local_recv_packets.ForEach(delegate(Packet packet)
                        {

                            byte[] packet_bytes = packet.GetBytes();

                            if (packet.Opcode == 0x5000 || packet.Opcode == 0x9000 || packet.Opcode == 0x2001)
                            {
                                return;
                            }

                            //print log
                            //if (!IgnoreOpcodes.Contains(packet.Opcode))
                            //    Views.BindingView.Write(String.Format("[C->P][AG_LC][{0:X4}][{1} bytes]{2}{3}{4}{5}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes)));

                            switch (packet.Opcode)
                            {
                                case 0x7158://resert when after upskill
                                    //int flag = packet.ReadUInt8();
                                    //if (flag == 1)
                                    //{
                                    //    packet.ReadUInt8();//unk
                                    //    flag = packet.ReadUInt8();
                                    //    if (flag == 73)
                                    //    {
                                    //        uint iskillId = packet.ReadUInt32();
                                    //        var skillTemp = CharData.Skill.FirstOrDefault(a => a.id.Equals(iskillId));
                                    //        if (skillTemp != null && skillTemp.id != 0 && skillTemp.status == 1)
                                    //        {
                                    //            if (skillTemp.type.Contains("_PASSIVE_"))//skill passive
                                    //            { }
                                    //            else if (skillTemp.type.Contains("_GIGONGTA_")) //skill imbue
                                    //            { }
                                    //            else
                                    //            {
                                    //                if (skillTemp.type.StartsWith("SKILL_CH_BOW") || skillTemp.type.StartsWith("SKILL_CH_SPEAR")
                                    //                    || skillTemp.type.StartsWith("SKILL_CH_SWORD"))
                                    //                {
                                    //                    if (skillTemp.type.StartsWith("SKILL_CH_BOW_CALL") || skillTemp.type.StartsWith("SKILL_CH_BOW_NORMAL")
                                    //                            || skillTemp.type.StartsWith("SKILL_CH_SPEAR_SPIN") || skillTemp.type.StartsWith("SKILL_CH_SWORD_SHIELD"))
                                    //                    {
                                    //                        skillTemp.status = 0;
                                    //                    }

                                    //                }

                                    //                if (skillTemp.type.StartsWith("SKILL_CH_COLD") || skillTemp.type.StartsWith("SKILL_CH_LIGHTNING")
                                    //                        || skillTemp.type.StartsWith("SKILL_CH_FIRE"))
                                    //                {
                                    //                    if (skillTemp.type.Contains("_GIGONGSUL_") || skillTemp.type.StartsWith("SKILL_CH_LIGHTNING_STORM")
                                    //                            || skillTemp.type.StartsWith("SKILL_CH_LIGHTNING_CHUNDUNG") || skillTemp.type.StartsWith("SKILL_CH_COLD_BINGPAN"))
                                    //                    { }
                                    //                    else//skill buff
                                    //                    {
                                    //                        skillTemp.status = 0;
                                    //                    }
                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    break;
                            }

                            ag_remote_security.Send(packet);


                        });
                        #endregion
                    }

                    #region ag_local_send_buffers
                    ag_local_send_buffers = ag_local_security.TransferOutgoing();
                    if (ag_local_send_buffers != null)
                    {
                        ag_local_send_buffers.ForEach((kvp) =>
                        {
                            Packet packet = kvp.Value;
                            TransferBuffer buffer = kvp.Key;

                            byte[] packet_bytes = packet.GetBytes();

                            //print log
                            //if (!IgnoreOpcodes.Contains(packet.Opcode))
                            //    Log.Write(String.Format("[P->C][AG_LC][{0:X4}][{1} bytes]{2}{3}{4}{5}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes)));

                            ag_local_stream.Write(buffer.Buffer, 0, buffer.Size);
                        });
                    }
                    #endregion

                    Thread.Sleep(1);

                }
            }
            catch (System.Exception ex)
            {
                Views.BindingFrom.Write("[ERROR] [AgentProxy::AgentLocalThread]");
                Views.BindingFrom.WriteLine(ex.Message);
            }
        }
        static void AgentThread()
        {
            try
            {
                ag_local_security = new Security();
                ag_local_security.GenerateSecurity(true, true, true);
                ag_local_recv_buffer = new TransferBuffer(4096, 0, 0);

                ag_remote_security = new Security();
                ag_remote_recv_buffer = new TransferBuffer(4096, 0, 0);

                ag_local_server = new TcpListener(_agentLocalEP);
                ag_local_server.Start();
                Views.BindingFrom.WriteLine(String.Format("[AG_LC] Waiting for a connection...   {0} " + Environment.NewLine, _agentLocalEP));
                ag_local_client = ag_local_server.AcceptTcpClient();
                ag_local_server.Stop();
                Views.BindingFrom.WriteLine(String.Format("[AG_LC] A connection has been made!" + Environment.NewLine));

                //_gatewayThread.Abort();

                Views.BindingFrom.WriteLine(String.Format("[AG_RM] Connecting to {0}  (ag)" + Environment.NewLine, _agentRemoteEP));
                ag_remote_client = new TcpClient();
                ag_remote_client.Connect(_agentRemoteEP);
                Views.BindingFrom.WriteLine(String.Format("[AG_RM] The connection has been made!(ag2)" + Environment.NewLine));

                ag_local_stream = ag_local_client.GetStream();
                ag_remote_stream = ag_remote_client.GetStream();

                Thread remote_thread = new Thread(AgentRemoteThread);
                remote_thread.Start();

                Thread local_thread = new Thread(AgentLocalThread);
                local_thread.Start();

                remote_thread.Join();
                local_thread.Join();


            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine(String.Format("[AgentThread] Exception: {0}" + Environment.NewLine, ex));
            }
        }
    }
}
