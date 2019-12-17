using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace IntCode
{
    public class MapTile
    {
        public MapTile(Point point, char value)
        {
            Point = point;
            Value = value;
        }

        public char Value { get; set; }
        public Point Point { get; }
    }
}
