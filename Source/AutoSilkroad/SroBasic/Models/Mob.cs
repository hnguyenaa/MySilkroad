using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class Mob
    {
        public uint ID { get; private set; }
        public string Type { get; private set; }
        public string Name { get; private set; }
        public byte Level { get; private set; }
        public uint HP { get; private set; }

        public Mob() { }
        public Mob(uint id, string name, string type, byte level, uint hp)
        {
            ID = id;
            Name = name;
            Type = type;
            Level = level;
            HP = hp;
        }
    }
}
