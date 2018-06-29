using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models.PacketData
{
    public class CharacterData
    {
        //public uint UniqueID { get; set; }
        public uint ID { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public ushort StatPoint { get; set; }
        public byte Zerk { get; set; }
        public uint HP { get; set; }
        public uint MP { get; set; }
        

        public byte TotalInventorySlot { get; set; }
        public byte TotalItem { get; set; }
        public List<Inventory> Inventories { get; set; }
        public Coordinate Coordinate { get; set; }
        public float WalkSpeed { get; set; }
        public float RunSpeed { get; set; }
        public float ZerkSpeed { get; set; }

        //public uint HorseID { get; set; }
        //public uint AttackPetID { get; set; }

        //public CharacterBadStatus BadStatus { get; set; }

        public List<Skill> Skills { get; set; }
        //public List<Skilltrain> AttackSkills { get; set; }
        //public List<Skilltrain> BuffSkills { get; set; }
        //public Skilltrain ImbueSkill { get; set; }

        public CharacterData()
        {
            Inventories = new List<Inventory>();
            Skills = new List<Skill>();
        }
    }
}
