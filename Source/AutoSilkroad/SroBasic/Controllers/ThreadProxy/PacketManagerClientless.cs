using SilkroadSecurityApi;
using SroBasic.Component.Logic;
using SroBasic.Controllers.ParsePacket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ThreadProxy
{
    static class PacketManagerClientless
    {
        public static void Manager(Packet packet)
        {
            switch (packet.Opcode)
            {
                #region Login Related
                //Session
                case 0x2001: //Identify
                    _0x2001.DoWork(packet,true);
                    break;
                case 0xA100: //Check Version
                    _0xA100.DoWork(packet,true);
                    break;
                case 0xA106: //AcceptVersion
                    _0xA106.DoWork(packet,true);
                    break;
                //Login
                case 0xA101: //SERVER_LIST
                    _0xA101.Dowork(packet,true);
                    break;
                case 0x2322: //SERVER_SEND_PACKET_CAPTCHA
                    _0x2322.DoWork(packet,true);
                    break;
                case 0xA102: //Login Replay
                    _0xA102.DoWork(packet,true);
                    break;
                case 0xA103:// SERVER_AGENT_LOGIN_RESPONSE
                    _0xA103.DoWork(packet,true);
                    break;
                case 0xB007://CLIENT_AGENT_CHARACTER_SELECTION_REQUEST
                    _0xB007.DoWork(packet,true);
                    break;
                #endregion
                #region Character
                case 0x3013://SERVER_AGENT_CHARACTER_DATA
                    _0x3013.DoWork(packet);
                    break;
                case 0x3020://ConfirmCharacterSpawn
                    _0x3020.DoWork(packet,true);
                    break;
                case 0x303D://Character Info
                    _0x303D.DoWork(packet);
                    break;
                case 0x3054://Character Level Up
                    _0x3054.DoWork(packet);
                    break;
                //case 0x3056://Character Exp Sp Level Up
                //    _0x3056.DoWork(packet);
                //    break;
                case 0x3057://Character HP MP Update
                    _0x3057.DoWork(packet);
                    break;
                //case 0x30D0://Character Speed Update
                //    _0x30D0.DoWork(packet);
                //    break;
                case 0xB021://Character Move
                    _0xB021.DoWork(packet);
                    break;
                //case 0xB050://Character Increase str
                //    _0xB050.DoWork(packet);
                //    break;
                //case 0xB051://Character Increase Int
                //    _0xB051.DoWork(packet);
                //    break;
                //case 0xB0A1://Character Skill Update
                //    _0xB0A1.DoWork(packet);
                //    break;
                #endregion
                #region InGameRelated
                //AbilitiesAndFighting
                case 0xB070:// Skill Add
                    _0xB070.DoWork(packet);
                    break;
                case 0xB071:// Skill Casted
                    _0xB071.DoWork(packet);
                    break;
                case 0xB072:// Buff Dell
                    _0xB072.DoWork(packet);
                    break;
                case 0xB0BD:// Buff Info
                    _0xB0BD.DoWork(packet);
                    break;
                //Environment
                case 0x3015:// Single Spawn
                    _0x3015.DoWork(packet);
                    break;
                case 0x3016:// Single Spawn
                    _0x3016.DoWork(packet);
                    break;
                case 0x3017:// Single Spawn
                    _0x3017.DoWork(packet);
                    break;
                case 0x3018:// Single Spawn
                    //_0x3018.DoWork(packet);
                    break;
                case 0x3019:// Single Spawn
                    _0x3019.DoWork(packet);
                    break;
                case 0x30BF:// Single Spawn
                    _0x30BF.DoWork(packet);
                    break;
                //Related
                //case 0x304E:// StuffUpdate
                //    _0x304E.DoWork(packet);
                //    break;
                //case 0x30D2:// Bad Effect
                //    _0x30D2.DoWork(packet);
                //    break;
                //case 0xB023:// Stuct
                //    _0xB023.DoWork(packet);
                //    break;
                #endregion
                #region Interaction
                case 0xB045:// Object Action
                    _0xB045.DoWork(packet);
                    break;
                //case 0xB046:// NPC Select
                //    _0xB046.DoWork(packet);
                //    break;
                //case 0xB04B:// NPC Deselect
                //    _0xB04B.DoWork(packet);
                //    break;
                case 0xB074:// Object Action
                    _0xB074.DoWork(packet);
                    break;
                #endregion
                #region PetAndVehicle
                //case 0x30C8:// Pet Info
                //    _0x30C8.DoWork(packet);
                //    break;
                //case 0x30C9:// Pet Stats
                //    _0x30C9.DoWork(packet);
                //    break;
                //case 0xB0CB:// Horse Action
                //    _0xB0CB.DoWork(packet);
                //    break;
                #endregion
            }

            switch (packet.Opcode)
            {
                case 0x3013://SERVER_AGENT_CHARACTER_DATA
                    DebugPacket.SavePacketToFile(packet);
                    break;
                case 0x3020://ConfirmCharacterSpawn
                    DebugPacket.SavePacketToFile(packet);
                    break;
            }
        }
    }
}
