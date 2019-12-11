using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day10
{
    public class Asteroid
    {
        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void PopulateVectors(List<Asteroid> others)
        {
            foreach (var asteroid in others)
            {
                if (this != asteroid)
                {
                    this.VectorsToOthers.Add(new Vector2(this.X, this.Y, asteroid.X, asteroid.Y));
                }
            }
        }

        public int VisibleAsteroidCount
        {
            get
            {
                return this.VectorsToOthers.GroupBy(x => x.Heading).Count();
            }
        }

        public int X { get; set; }

        public int Y { get; set; }

        public List<Vector2> VectorsToOthers { get; set; } = new List<Vector2>();

    }
}
