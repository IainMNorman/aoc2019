using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace IntCode
{
    public class QNode
    {
        public Point Point { get; set; }

        public int Distance { get; set; }

        public QNode(Point point, int distance)
        {
            Point = point;
            Distance = distance;
        }
    }
}
