using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers
{
    public static class DebugPacket
    {
        private const string FolderDebug = "DebugPacket";
        private static readonly string _pathDebugPacket = Environment.CurrentDirectory + "\\" + FolderDebug + "\\";
        public static void SavePacketToFile(Packet packet)
        {
        //    Packet packet = new Packet(packet);
            if (!Directory.Exists(FolderDebug))
            {
                Directory.CreateDirectory(FolderDebug);
            }
            string filePath = _pathDebugPacket + string.Format("[{0:X4}].txt", packet.Opcode);//{1:yyyy-MM-dd-HH-mm}].

            using (TextWriter writer = File.CreateText(filePath))
            {
                writer.WriteLine(string.Format("{0:X4}", packet.Opcode));
                writer.WriteLine(packet.Encrypted == true ? 1 : 0);
                writer.WriteLine(packet.Massive == true ? 1 : 0);
                byte[] bytes = packet.GetBytes();
                writer.WriteLine(bytes.Length);
                for (int i = 0; i < bytes.Length; i++)
                {
                    writer.WriteLine(string.Format("{0:X2}", bytes[i]));
                }
            }
        }

        public static Packet LoadPacketFormFile(ushort packetOpcode)
        {
            Packet packet = null;
            string path = _pathDebugPacket + string.Format("[{0:X4}].txt", packetOpcode);
            using (TextReader reader = File.OpenText(path))
            {
                ushort opcode = Convert.ToUInt16(reader.ReadLine(), 16);
                bool encrypted = reader.ReadLine() == "1" ? true : false;
                bool massive = reader.ReadLine() == "1" ? true : false;

                int lenght = Convert.ToInt32(reader.ReadLine());
                byte[] bytes = new byte[lenght];
                for (int i = 0; i < lenght; i++)
                {
                    bytes[i] = Convert.ToByte(reader.ReadLine(), 16);
                }

                packet = new Packet(opcode, encrypted, massive, bytes);
            }
            packet.Lock();
            return packet;
        }
    }
}
