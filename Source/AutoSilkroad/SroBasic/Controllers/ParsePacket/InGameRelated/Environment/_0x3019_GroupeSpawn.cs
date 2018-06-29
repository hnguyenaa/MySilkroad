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
    /// [_0x3019] Group Spawn
    /// </summary>
    public static class _0x3019
    {
        private static void Parse(Packet packet)
        {
             var groupSpawnInfo = _0x3017._GroupSpawnBegin;

             if (groupSpawnInfo.Type == GroupSpawnType.Spawn) //Spawn
            {
                #region Spawn Group
                for (int i = 0; i < groupSpawnInfo.MobCount; i++)
                {
                    _0x3015.DoWork(packet);
                }
                #endregion
            }
            else//despawn
            {
                #region Despawn Group
                for (int i = 0; i < groupSpawnInfo.MobCount; i++)
                {
                    _0x3016.DoWork(packet);
                }
                #endregion
            }
        }

        public static void DoWork(Packet packet)
        {
            Parse(packet);
        }
    }
}
