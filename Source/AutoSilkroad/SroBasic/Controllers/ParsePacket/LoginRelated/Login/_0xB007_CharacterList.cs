using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0xB007] CLIENT_AGENT_CHARACTER_SELECTION_REQUEST
    /// </summary>
    public static class _0xB007
    {
        public static List<string> Parse(Packet packet)
        {
            List<string> characters = new List<string>();
            try
            {
                if (packet.ReadUInt8() == 0x02)
                {
                    if (packet.ReadUInt8() == 0x01)
                    {
                        byte char_count = packet.ReadUInt8();
                        for (int i = 0; i < char_count; i++)
                        {
                            #region Main
                            packet.ReadUInt32(); //Model
                            characters.Add(packet.ReadAscii()); // Name
                            packet.ReadUInt8(); //Volume/Height
                            packet.ReadUInt8(); //Level
                            packet.ReadUInt64(); //Exp
                            packet.ReadUInt16(); //STR
                            packet.ReadUInt16(); //INT
                            packet.ReadUInt16(); //Stats points
                            packet.ReadUInt32(); //Hp
                            packet.ReadUInt32(); //Mp
                            #endregion
                            #region Deletion
                            byte char_delete = packet.ReadUInt8();
                            if (char_delete == 1)
                            {
                                packet.ReadUInt32();
                            }
                            packet.ReadUInt16();
                            packet.ReadUInt8(); //Unknown
                            #endregion
                            #region Items
                            int itemscount = packet.ReadUInt8();
                            for (int a = 0; a < itemscount; a++)
                            {
                                packet.ReadUInt32(); //Item ID
                                packet.ReadUInt8(); //Plus Value
                            }
                            #endregion
                            #region Avatars
                            int avatarcount = packet.ReadUInt8(); //Avatar count
                            for (int a = 0; a < avatarcount; a++)
                            {
                                packet.ReadUInt32(); // Avatar ID
                                packet.ReadUInt8();
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Views.BindingFrom.WriteLine("[Error] _0xB007(CharacterList): " + ex.Message);
            }

            return characters;
        }

        private static void Share(List<string> data, bool isClientless)
        {
            if(data != null && data.Count > 0)
            {
                Views.BindingFrom.BindingCharacterList(data);
                Views.BindingFrom.Write("---- List Character (" + data.Count + ") ----");
                for(int i = 0; i < data.Count; i++)
                {
                    Views.BindingFrom.Write(data[i]);
                }
                Views.BindingFrom.Write("");

                if (isClientless)
                {
                    Views.BindingFrom.WriteLine("Auto select character: "+ data[1]);
                    Packet p = new Packet(0x7001);//CLIENT_SELECT_CHARACTER = 0x7001
                    p.WriteAscii(data[1]);
                    ThreadProxy.ProxyClientless.SendPacketToAgentRemote(p);
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
