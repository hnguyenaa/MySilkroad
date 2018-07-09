using SilkroadSecurityApi;
using SroBasic.Models;
using SroBasic.Models.PacketData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Views
{
    [Flags]
    public enum BindingCharacterType
    {
        None = 0,
        Level = 1 << 0,
        Coordinate = 1 << 1,
        HP_MP = 1 << 2,
        StatPoint = 1 << 3,
        Zerk = 1 << 4,
        Skill = 1 << 5,
        All = Level | Coordinate | HP_MP | StatPoint | Zerk | Skill
    }

    [Flags]
    public enum BindingBotDebug
    {
        None = 0,
        Skill = 1 << 0,
        Mob = 1 << 1,
        All = Skill | Mob
    }
    static class BindingFrom
    {
        static frmMain frmMain;

        public static void InitBinding(frmMain frm)
        {
            frmMain = frm;
        }

        public static void Write(string str)
        {
            DateTime now = DateTime.Now;
            str = String.Format("{0}:{1}:{2}:{3}", now.Hour, now.Minute, now.Second, now.Millisecond) + Environment.NewLine + str;
            frmMain.PrintLog(str);
        }

        public static void WriteLine(string str)
        {
            DateTime now = DateTime.Now;
            str = String.Format("{0}:{1}:{2}:{3}", now.Hour, now.Minute, now.Second, now.Millisecond) + Environment.NewLine + str + Environment.NewLine;
            frmMain.PrintLog(str);
        }

        public static void WriteDebug(string str)
        {
            DateTime now = DateTime.Now;
            str = str + Environment.NewLine;
            frmMain.PrintLog(str);
        }
        public static void WritePacket(Packet packet)
        {
            byte[] packet_bytes = packet.GetBytes();
            Views.BindingFrom.WriteLine(String.Format("[P->C][GW_LC][{0:X4}][{1} bytes]{2}{3}{4}{5}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes)));
        }

        #region Binding From
        public static void BindingServerCombobox(List<Server> servers)
        {
            frmMain.BindingServerCombobox(servers);
        }

        public static void BindingCharacterList(List<string> character)
        {
            frmMain.BindingCharacterList(character);
        }

        public static void BindingCharacter(BindingCharacterType type)
        {
            frmMain.BindingCharacterInfo(type, Metadata.Globals.Character);
        }
        public static void BindingBotDebug(BindingBotDebug type)
        {
            frmMain.BindingBotDebug(type);
        }
        #endregion
    }
}
