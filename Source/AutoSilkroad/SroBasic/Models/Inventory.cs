using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class Inventory
    {
        public byte Slot { get; set; }
        public uint ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public ushort Count { get; set; }
        public uint Durability { get; set; }

        public override string ToString()
        {
            //return base.ToString();
            string result = "(id: {0}, slot: {1}, name: '{2}', type: '{3}', count: {4}, durability: {5})";
            result = string.Format(result, ID, Slot, Name, Type, Count, Durability);
            return result;
        }
    }
}
