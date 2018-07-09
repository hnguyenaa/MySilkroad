using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RefObjChar
    {
        public uint ID { get; set; }
        public string CodeName128 { get; set; }
        public byte Bionic { get; set; }
        public byte TypeID1 { get; set; }
        public byte TypeID2 { get; set; }
        public byte TypeID3 { get; set; }
        public byte TypeID4 { get; set; }
        public uint DecayTime { get; set; }
        public byte Country { get; set; }
        public byte Rarity { get; set; }
        public byte CanTrade { get; set; }
        public byte CanSell { get; set; }
        public byte CanBuy { get; set; }
        public byte CanBorrow { get; set; }
        public byte CanDrop { get; set; }
        public byte CanPick { get; set; }
        public byte CanRepair { get; set; }
        public byte CanRevive { get; set; }
        public byte CanUse { get; set; }
        public byte CanThrow { get; set; }
        public byte ReqLevel1 { get; set; }
        public byte Lvl { get; set; }
        public byte CharGender { get; set; }
    }
}
