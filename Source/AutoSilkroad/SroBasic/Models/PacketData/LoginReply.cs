using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models.PacketData
{
    /// <summary>
    /// For packet [0xA102] Login Reply
    /// </summary>
    public class LoginReply
    {
        public byte Result { get; set; }
        public uint Session { get; set; }
        public IPEndPoint AgentRemoteEP { get; set; }
    }
}
