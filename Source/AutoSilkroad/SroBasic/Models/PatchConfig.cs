using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class PatchConfig
    {
        public bool MultiClient { get; set; }
        public bool RedirectIP { get; set; }
        public bool NudePatch { get; set; }
        public bool ZoomHack { get; set; }
        public bool SwearFilter { get; set; }
        public bool ServerStatus { get; set; }
        public bool NoGameGuard { get; set; }
        public bool EnglishPatch { get; set; }
        public bool PatchSeed { get; set; }

        public IPEndPoint RedirectGatewayServer { get; set; }
        public IPEndPoint RedirectAgentServer { get; set; }
    }
}
