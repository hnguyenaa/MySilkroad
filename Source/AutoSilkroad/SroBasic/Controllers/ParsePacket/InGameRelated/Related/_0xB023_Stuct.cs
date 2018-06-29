using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0xB023] Stuct
    /// </summary>
    public static class _0xB023
    {
        public static void Parse(Packet packet)
        {
            //uint id = packet.ReadUInt32();
            //if (id == CharData.UniqueID)
            //{
            //    //MediaData.log("UniqueID:" + CharData.UniqueID + " idP:" + id);
            //    //MediaData.log(String.Format("[S->P][AG_RM][{0:X4}][{1} bytes]{2}{3}{4}{5}", packetCopy.Opcode, packet_bytes.Length, packetCopy.Encrypted ? "[Encrypted]" : "", packetCopy.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes)));
            //    stuck_count++;
            //    byte xsec = packet.ReadUInt8();
            //    byte ysec = packet.ReadUInt8();
            //    float xcoord = packet.ReadSingle();
            //    packet.ReadSingle();//z
            //    float ycoord = packet.ReadSingle();
            //    CharData.X = ConvertCoord.CalculatePositionX(xsec, xcoord);
            //    CharData.Y = ConvertCoord.CalculatePositionY(ysec, ycoord);

            //    if (stuck_count > 5)
            //    {
            //        //MediaData.log("STUCK at " + CharData.X + ", " + CharData.Y + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            //        stuck_count = 0;
            //        if (BotData.bot)
            //        {
            //            MonsterControl.DeSelectAll();
            //            WalkControl.WalkRandom();
            //            //System.Threading.Thread n_t = new System.Threading.Thread(LogicControl.Manager);
            //            //n_t.Start();
            //        }
            //    }
            //}
        }
        private static void Share(object data)
        {

        }
        public static void DoWork(Packet packet)
        {

        }
    }
}
