using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class ClientInfo
    {
        public string SroType { get; set; }
        public byte Locale { get; set; }
        public uint Version { get; set; }
        public int Port { get; set; }
        public IPAddress IP { get; set; }

        public IPEndPoint RedirectGatewayServer { get; set; }
        public IPEndPoint RedirectAgentSetver { get; set; }
    }
}
