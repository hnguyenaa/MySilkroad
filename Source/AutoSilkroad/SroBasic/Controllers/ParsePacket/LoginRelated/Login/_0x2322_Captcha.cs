using SilkroadSecurityApi;
using SroBasic.Controllers.ThreadProxy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0x2322] Captcha
    /// </summary>
    public static class _0x2322
    {
        private static void Share(Image data, bool isClientless)
        {
            if (isClientless)
            {
                var p = GeneratePacket.SubmitCaptcha("1");
                ProxyClientless.SendPacketToGatewayRemote(p);
            }
        }
        public static void DoWork(Packet packet, bool isClientless = false)
        {
            Share(null, isClientless);
        }
    }
}
