using SroBasic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Metadata
{
    public static class Globals
    {

        public static bool IsDebug = false;

        public static uint session;
        public static Character Character = new Character();

        public static Dictionary<uint, MobSpawn> MobSpawns = new Dictionary<uint, MobSpawn>();

        //public static List<MobSpawn> MobSpawns = new List<MobSpawn>();

        public static void SetMobDie(uint id)
        {
            if (MobSpawns.ContainsKey(id))
                MobSpawns[id].IsDie = true;
        }
        public static void SetMobCoordinate(uint id, Coordinate coordinate)
        {
            if (MobSpawns.ContainsKey(id))
                MobSpawns[id].Coordinate = coordinate;
        }
        //public static void SetMobSelected(uint id)
        //{
        //    if (MobSpawns.ContainsKey(id))
        //        MobSpawns[id].IsSelected = true;
        //}
        public static void AddMob(MobSpawn mob)
        {
            if (mob != null && mob.UniqueID > 0)
                MobSpawns.Add(mob.UniqueID, mob);
        }
        public static bool RemoveMob(uint id)
        {
            if (MobSpawns.ContainsKey(id))
                return MobSpawns.Remove(id);

            return false;
        }

        public static MobSpawn GetMobMinDistance()
        {
            MobSpawn mob = new MobSpawn();
            if(MobSpawns.Count > 0)
                mob = MobSpawns
                    .Where(a=> !a.Value.IsDie)
                    .OrderBy(a => a.Value.Distance)
                    .FirstOrDefault().Value;

            return mob;
        }
    }
}
