using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IntCode
{
    public static class Extensions
    {
        public static bool AddIfNotContains(this List<MapTile> @this, MapTile value)
        {
            if (!@this.Any(m => m.Point.X == value.Point.X && m.Point.Y == value.Point.Y))
            {
                @this.Add(value);
                return true;
            }

            return false;
        }

        public static bool PointExists(this List<MapTile> @this, Point value)
        {
            return @this.Any(m => m.Point.X == value.X && m.Point.Y == value.Y);
        }
    }
}
