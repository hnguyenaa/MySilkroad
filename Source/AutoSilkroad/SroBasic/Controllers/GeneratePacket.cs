using SilkroadSecurityApi;

namespace SroBasic.Controllers
{
    static class GeneratePacket
    {
        /// <summary>
        /// [0x6323]
        /// </summary>
        /// <param name="captcha"></param>
        /// <param name="sroType"></param>
        /// <returns></returns>
        public static Packet SubmitCaptcha(string captcha, string sroType = "vSro")
        {
            Packet packet = new Packet(0x6323);
            if (sroType == "iSro")
            {
                packet = new Packet(0x7625);
                packet.WriteUInt8(2);
            }
            packet.WriteAscii(captcha);

            return packet;
        }

        /// <summary>
        /// [0x6102]
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public static Packet LoginServer(byte locale, string user, string pass, uint serverID)
        {
            Packet packet = new Packet(0x6102, true);
            packet.WriteUInt8(locale);
            packet.WriteAscii(user);
            packet.WriteAscii(pass);
            if (locale == 18)
            {
                packet.WriteUInt16(0);//mobile verification...
                packet.WriteUInt8(255);//mobile verification...
            }
            packet.WriteUInt16(serverID);

            return packet;
        }

        /// <summary>
        /// [0x6103] CLIENT_AGENT_LOGIN_REQUEST
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public static Packet LoginRequest(byte locale, string user, string pass, uint sessionID)
        {
            Packet packet = new Packet(0x6103);//CLIENT_AGENT_LOGIN_REQUEST = 0x6103
            packet.WriteUInt32(sessionID);
            packet.WriteAscii(user);
            packet.WriteAscii(pass);
            packet.WriteUInt8(locale);
            packet.WriteUInt32(0);//MAC
            packet.WriteUInt16(0);//ADRESS

            return packet;
        }

        /// <summary>
        /// [0xA102]
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="sessionID"></param>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static Packet LoginReply(byte flag, uint sessionID, string ipAddress, ushort port)
        {
            Packet packet = new Packet(0xA102, true);//so client know not to connect to joymax, but to proxy first // wheres ip being used ?
            packet.WriteUInt8(flag);
            packet.WriteUInt32(sessionID);
            packet.WriteAscii(ipAddress);
            packet.WriteUInt16(port);//the fix was the agentthread, i know but u said the client doesnt respond back so i wanna see the [C->S][6102][26 bytes][Encrypted] etc

            return packet;
        }

        /// <summary>
        /// [0x6100]
        /// <para>Reply packet [0x2001] Identify (gateway connect)</para>
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="vesion"></param>
        /// <returns></returns>
        public static Packet AcceptConnect(byte locale, uint vesion)
        {
            Packet packet = new Packet(0x6100);//ACCEPT = 0x6100,
            packet.WriteUInt8(locale);
            packet.WriteAscii("SR_Client");
            packet.WriteUInt32(vesion);

            return packet;
        }

        public static Packet BuffSkill(uint skillId)
        {
            Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            packet.WriteUInt8(1);
            packet.WriteUInt8(4);
            packet.WriteUInt32(skillId);
            packet.WriteUInt8(0);

            return packet;
        }

        public static Packet AttackSkill(uint skillId, uint objectId)
        {
            Packet packet = new Packet(0x7074);//CLIENT_OBJECTACTION
            packet.WriteUInt8(1);
            packet.WriteUInt8(4);
            packet.WriteUInt32(skillId);
            packet.WriteUInt8(1);
            packet.WriteUInt32(objectId);

            return packet;
        }

        public static Packet AttackNormal(uint objectId)
        {
            Packet packet = new Packet(0x7074);
            packet.WriteUInt8(1);
            packet.WriteUInt8(1);
            packet.WriteUInt8(1);
            packet.WriteUInt32(objectId);

            return packet;
        }

        public static Packet IncreaseStrength()
        {
            Packet packet = new Packet(0x7050);

            return packet;
        }

        public static Packet IncreaseIntellect()
        {
            Packet packet = new Packet(0x7051);

            return packet;
        }

        public static Packet Berserk()
        {
            Packet packet = new Packet(0x70A7);
            packet.WriteUInt8(0x01);

            return packet;
        }

    }
}
