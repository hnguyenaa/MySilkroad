using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class MobSpawn
    { 
        #region field & propety
        public uint UniqueID { get; private set; }
        public Coordinate Coordinate { get; set; }
        public double Distance { get; set; }
        //public bool IsSelected { get; set; }
        public bool IsDie { get; set; }
        public int CountAttack { get; set; }
        public bool IsBehindObstacle { get; set; }

        #endregion

        public MobSpawn()
        {}

        public MobSpawn(uint id, Coordinate coordinate, double distance)
        {
            UniqueID = id;
            Coordinate = coordinate;
            Distance = distance;
            //IsSelected = false;
            IsDie = false;
            CountAttack = 0;
            IsBehindObstacle = false;
        }

        //public MobSpawn(uint id, Coordinate coordinate, Coordinate currentCoordinate)
        //{
        //    UniqueID = id;
        //    Coordinate = coordinate;

        //    double distance = Math.Sqrt((Coordinate.X - currentCoordinate.X) * (Coordinate.X - currentCoordinate.X) +
        //       (Coordinate.Y - currentCoordinate.Y) * (Coordinate.Y - currentCoordinate.Y));
        //    Distance = distance;
        //}

        public void UpdateDistance(Coordinate currentCoordinate)
        {
            double distance = Math.Sqrt((Coordinate.X - currentCoordinate.X) * (Coordinate.X - currentCoordinate.X) +
                (Coordinate.Y - currentCoordinate.Y) * (Coordinate.Y - currentCoordinate.Y));
            Distance = distance;
        }

        public void SetCoordinate(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public void SetCoordinateAndUpdateDistance(Coordinate coordinate, Coordinate currentCoordinate)
        {
            Coordinate = coordinate;
            double distance = Math.Sqrt((Coordinate.X - currentCoordinate.X) * (Coordinate.X - currentCoordinate.X) +
                (Coordinate.Y - currentCoordinate.Y) * (Coordinate.Y - currentCoordinate.Y));
            Distance = distance;
        }
    }
}
