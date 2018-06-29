using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models.PacketData
{
    public class SingleSpawn
    {
        public SpawnedType Type { get; set; }
        public MobSpawn Mob { get; set; }

        public SingleSpawn()
        {
            Type = SpawnedType.None;
            Mob = new MobSpawn();
        }
    }


    public enum SpawnedType
    {
        None,
        Mob,
        Item
    }
}
