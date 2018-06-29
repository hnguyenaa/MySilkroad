using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [0x304E] Stuff Update
    /// </summary>
    public static class _0x304E
    {
        enum StuffType
        {
            None = 0,
            Gold = 0x01,
            SkillPoint = 0x02,
            Zerk = 0x04
        }
        class Data
        {
            public StuffType DataType { get; private set; }
            public ulong Glod { get; private set; }
            public uint SkillPoint { get; private set; }
            public byte Zerk { get; private set; }

            public Data()
            {
                DataType = StuffType.None;
                Glod = 0;
                SkillPoint = 0;
                Zerk = 0;
            }

            public void SetGlod(ulong gold)
            {
                DataType = StuffType.Gold;
                Glod = gold;
            }
            public void SetSkillPoint(uint skillPoint)
            {
                DataType = StuffType.SkillPoint;
                SkillPoint = skillPoint;
            }
            public void SetZerk(byte zerk)
            {
                DataType = StuffType.Zerk;
                Zerk = zerk;
            }
        }
        private static Data Parse(Packet packet)
        {
            Data Data = new Data();
            //try
            //{
                byte code = packet.ReadUInt8();
                switch (code)
                {
                    case 0x01:
                        #region Gold Update
                        ulong gold = packet.ReadUInt64();

                        Data.SetGlod(gold);
                        #endregion
                        break;
                    case 0x02:
                        #region SP Update
                        uint skillPoints = packet.ReadUInt32();

                        Data.SetSkillPoint(skillPoints);
                        #endregion
                        break;
                    case 0x04:
                        #region Zerk Update
                        byte zerk = packet.ReadUInt8();

                        Data.SetZerk(zerk);
                        #endregion
                        break;
                }

            //}
            //catch (Exception ex)
            //{
            //    Views.BindingView.Write("[ERROR] [ParsePacketDebug::CharacterStuffUpdate]");
            //    Views.BindingView.WriteLine(ex.Message);
            //}

            return Data;
        }
        private static void Share(Data data)
        {
            if (data.DataType == StuffType.Zerk) 
            {
                Metadata.Globals.Character.Zerk = data.Zerk;
                Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.Zerk);

                Bot.BotInput.AutoUsingZerk();
            }
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
