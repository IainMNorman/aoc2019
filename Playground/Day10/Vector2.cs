using System;
using System.Collections.Generic;
using System.Text;

namespace Day10
{
    public class Vector2
    {
        public Vector2(int x1, int y1, int x2, int y2)
        {
            this.X = x2 - x1;
            this.Y = y2 - y1;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public double Magnitude
        {
            get
            {
                return Math.Sqrt((Math.Pow(this.X, 2)) + (Math.Pow(this.Y, 2)));
            }
        }

        public double Heading
        {
            get
            {
                var heading = Math.Atan2(this.X, this.Y);
                heading = heading * (180.0 / Math.PI);
                heading -= 180;

                if (heading < 0)
                {
                    heading = heading + 360;
                }
                return (360 - heading) % 360;
            }
        }
    }
}
