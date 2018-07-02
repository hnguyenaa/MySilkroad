using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum TypeID1 : byte
    {
        None = 0x00,
        Bionic = 0x01,
        Item = 0x03,
        Portals = 0x04
    }

    public enum CharTypeID2 : byte
    {
        None = 0x00,
        CHAR = 0x01,
        NPC = 0x02,
        ITEM_EQUIP = 0x01,
        ITEM_ETC = 0x03,
    }
    public enum CharTypeID3 : byte
    {
        None = 0x00,
        NPC_MOB = 0x02,
        NPC_COS = 0x03,
        NPC_FORTRESS_COS = 0x04,
        NPC_FORTRESS_STRUCT = 0x05,
        ITEM_ETC_MONEY_GOLD = 0x05,
        ITEM_ETC_TRADE = 0x08,
        ITEM_ETC_QUEST = 0x09
    }
    public enum ItemTypeID2 : byte
    {
        None = 0x00,
        CHAR = 0x01,
        NPC = 0x02,
        ITEM_EQUIP = 0x01,
        ITEM_ETC = 0x03,
    }
    public enum ItemTypeID3 : byte
    {
        None = 0x00,
        NPC_MOB = 0x02,
        NPC_COS = 0x03,
        NPC_FORTRESS_COS = 0x04,
        NPC_FORTRESS_STRUCT = 0x05,
        ITEM_ETC_MONEY_GOLD = 0x05,
        ITEM_ETC_TRADE = 0x08,
        ITEM_ETC_QUEST = 0x09
    }

    public class RefObj<T2, T3> 
    {
        uint ID { get; set; }
        string CodeName { get; set; }
        byte Bionic { get; set; }
        TypeID1 TypeID1 { get; set; }
        T2 TypeID2 { get; set; }
        T3 TypeID3 { get; set; }
        byte TypeID4 { get; set; }
    }

    public class RefObjChar : RefObj<CharTypeID2, CharTypeID3>
    {
        public byte Level { get; set; }
        public uint MaxHP { get; set; }
        public uint MaxMP { get; set; }
    }

    public class RefObjItem : RefObj<ItemTypeID2, ItemTypeID3>
    {
        public uint MaxStack { get; set; }
        public byte ItemClass { get; set; }
    }
}
