using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models.PacketData
{
    /// <summary>
    /// [0x3017] 
    /// </summary>
    public class GroupSpawnBegin
    {
        public GroupSpawnType Type { get; set; }
        public ushort MobCount { get; set; }

        public GroupSpawnBegin()
        {
            Type = GroupSpawnType.None;
            MobCount = 0;
        }
    }

    public enum GroupSpawnType
    {
        None,
        Spawn,
        Despawn
    }
}
