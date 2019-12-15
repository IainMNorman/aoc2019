using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace IntCode
{
    public class GameTile
    {
        public Point Coords { get; set; }

        public TileType TileType { get; set; }
    }

    public enum TileType
    {
        Empty = 0,
        Wall = 1,
        Block = 2,
        Paddle = 3,
        Ball = 4
    }
}
