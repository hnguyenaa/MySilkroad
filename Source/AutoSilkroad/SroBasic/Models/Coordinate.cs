using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SroBasic.Models
{
    public class Coordinate
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public int Z { get; private set; }

        public bool InCave { get; private set; }

        public Coordinate(byte xsec, byte ysec, float xcoord, float ycoord, float zcoord)
        {
            float real_xcoord = xcoord;
            float real_ycoord = ycoord;
            if (xcoord > 32768) real_xcoord = (65536 - xcoord);
            if (ycoord > 32768) real_ycoord = (65536 - ycoord);



            X = (int)((xsec - 135) * 192 + real_xcoord / 10);
            Y = (int)((ysec - 92) * 192 + real_ycoord / 10);
            Z = (int)zcoord;
            if (ysec == 0x80)
                InCave = true;
        }

        public static double Distance(Coordinate a, Coordinate b)
        {
            double distance = 0;
            if (a == null || b == null)
                distance = 0;
            else
                distance = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

            return distance;
        }

        public double Distance(Coordinate b)
        {
            double distance = 0;
            if (b == null)
                distance = 0;
            else
                distance = Math.Sqrt(Math.Pow(X - b.X, 2) + Math.Pow(Y - b.Y, 2));

            return distance;
        }
    }
}
