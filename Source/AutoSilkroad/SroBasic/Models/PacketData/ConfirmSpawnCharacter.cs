using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models.PacketData
{
    /// <summary>
    /// For packet [0x3020] Confirm Spawn Character
    /// </summary>
    public class ConfirmSpawnCharacter
    {
        public uint UniqueID { get; set; }
        public List<byte> SkipCharacterID { get; set; }
    }
}
