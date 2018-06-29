using SilkroadSecurityApi;
using SroBasic.Models.PacketData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Controllers.ParsePacket
{
    /// <summary>
    /// [_0x3017] Group Spawn Begin
    /// </summary>
    public static class _0x3017
    {
        public static GroupSpawnBegin _GroupSpawnBegin = new GroupSpawnBegin();
        private static GroupSpawnBegin Parse(Packet packet)
        {
            GroupSpawnBegin data = new GroupSpawnBegin();

            byte action = packet.ReadUInt8();
            ushort mobCount = packet.ReadUInt16();

            data.MobCount = mobCount;
            if (action == 0x01)
            {
                data.Type = GroupSpawnType.Spawn;
            }
            else if (action == 0x02)
            {
                data.Type = GroupSpawnType.Despawn;
            }

            return data;
        }
        private static void Share(GroupSpawnBegin data)
        {
            _GroupSpawnBegin = data;
        }
        public static void DoWork(Packet packet)
        {
            var data = Parse(packet);
            Share(data);
        }
    }
}
