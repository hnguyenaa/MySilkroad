using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class Item
    {
        public uint ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public ushort Stack { get; set; }
        public ushort Durability { get; set; }

        public Item() { }
        public Item(uint id, string type, string name, byte level, ushort stack, ushort durability)
        {
            ID = id;
            Type = type;
            Name = name;
            Level = level;
            Stack = stack;
            Durability = durability;
        }

        public override string ToString()
        {
            //return base.ToString();
            string result = "(id: {0}, name: '{1}', type: '{2}', level: {3}, stack: {4}, durability: {5})";
            result = string.Format(result, ID, Name, Type, Level, Stack, Durability);
            return result;
        }
    }
}
