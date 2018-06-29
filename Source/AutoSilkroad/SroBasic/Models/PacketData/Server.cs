using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models.PacketData
{
    /// <summary>
    /// For packet [0xA101] Server List
    /// </summary>
    public class Server
    {
        public ushort ID { get; set; }
        public string Name { get; set; }
        public ushort CurrentUsers { get; set; }
        public ushort MaxUsers { get; set; }
        public byte State { get; set; }
        public char Country { get; set; }
        public float Ratio { get; set; }
    }
}
