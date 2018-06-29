using SilkroadSecurityApi;
using SroBasic.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0x3057] Character HP MP Update
    /// </summary>
    public static class _0x3057
    {
        enum ObjectType
        {
            None,
            Character,
            AttackPet,
            Horse,
            Mob
        }
        //enum ValueType
        //{
        //    None,
        //    HP,
        //    MP,
        //    HP_MP
        //}
        struct Data
        {
            public uint ObjectID { get; set; }
            public ObjectType Type { get; set; }
            //public ValueType ValueType { get; set; }
            public uint HP { get; set; }
            public uint MP { get; set; }
        }
        private static Data Parse(Packet packet)
        {
            Data data = new Data();

            //try
            //{
            uint objectID = packet.ReadUInt32();
            data.ObjectID = objectID;

            #region id = character.ID then up char(hp or mp)
            if (objectID == Globals.Character.UniqueID)
            {
                packet.ReadUInt8();
                packet.ReadUInt8(); // 0x00
                byte type2 = packet.ReadUInt8();

                data.Type = ObjectType.Character;
                switch (type2)
                {
                    case 0x01://HP
                        uint currentHP = packet.ReadUInt32();
                        data.HP = currentHP;
                        break;
                    case 0x02://MP
                        uint currentMP = packet.ReadUInt32();
                        data.MP = currentMP;
                        break;
                    case 0x03://HP + MP
                        currentHP = packet.ReadUInt32();
                        currentMP = packet.ReadUInt32();

                        data.HP = currentHP;
                        data.MP = currentMP;
                        break;
                    case 0x04: //Status
                        //if (packet.ReadUInt32() == 0)
                        //{
                        //    //set result
                        //    result.Add(new Tuple<object, CharacterUpdateType>(false, CharacterUpdateType.BadStatus));
                        //}
                        //else
                        //{
                        //    //set result
                        //    result.Add(new Tuple<object, CharacterUpdateType>(true, CharacterUpdateType.BadStatus));
                        //}
                        break;
                }
            }
            #endregion
            #region id = attack_pet then up attack_pet(hp) => buffHp.pet
            //else if (objectID == Globals.Character.AttackPetID)
            //{
            //    packet.ReadUInt8();
            //    packet.ReadUInt8();

            //    byte type = packet.ReadUInt8();
            //    switch (type)
            //    {
            //        case 0x04: //status
            //            //if (packet.ReadUInt32() == 0)
            //            //{ bool petBadStatus = false; }
            //            //else
            //            //{ bool petBadStatus = true; }
            //            break;
            //        case 0x05: //HP
            //            uint petHP = packet.ReadUInt32();
            //            break;

            //    }
            //}
            #endregion
            #region id = horse then up pet(hp) => buffhp.horse
            //else if (objectID == Globals.Character.HorseID)
            //{
            //    packet.ReadUInt8();
            //    packet.ReadUInt8();

            //    byte type = packet.ReadUInt8();
            //    switch (type)
            //    {
            //        case 0x04: //status
            //            //if (packet.ReadUInt32() == 0)
            //            //{ bool horseBadStatus = false; }
            //            //else
            //            //{ bool horseBadStatus = true; }
            //            break;
            //        case 0x05: //HP
            //            uint horseHP = packet.ReadUInt32();
            //            break;

            //    }
            //}
            #endregion
            #region up mob.hp = > remove mob.hp = 0 ==> monster died
            //else if (Globals.MobSpawns.Exists(a=>a.UniqueID == objectID))
            //{
            //    data.Type = ObjectType.Mob;

            //    packet.ReadUInt8();
            //    packet.ReadUInt8();
            //    byte type = packet.ReadUInt8();
            //    switch (type)
            //    {
            //        case 0x05:
            //            uint hp = packet.ReadUInt32();
            //            data.HP = hp;
            //            break;
            //    }

            //}
            #endregion
            //}
            //catch (Exception ex)
            //{
            //    Views.BindingView.Write("[ERROR] [ParsePacketDebug::CharacterHpMpUpdate]");
            //    Views.BindingView.WriteLine(ex.Message);
            //}

            return data;
        }
        private static void Share(Data data)
        {
            if (data.Type == ObjectType.Character)
            {
                if (data.HP > 0)
                    Globals.Character.HP = data.HP;
                if (data.MP > 0)
                    Globals.Character.MP = data.MP;

                Views.BindingFrom.BindingCharacter(Views.BindingCharacterType.HP_MP);
            }
            //else if (data.Type == ObjectType.Mob)
            //{
            //    if (data.HP == 0)
            //    {
            //        Views.BindingFrom.WriteLine("[0x3057][HP MP Update] mob die :" + data.ObjectID);
            //    }
            //}
        }
        public static void DoWork(Packet packet)
        {
            //var data = Parse(packet);
            //Share(data);
        }
    }
}
