using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Vector2(5, 5, 5, 4).Heading); // up
            Console.WriteLine(new Vector2(5, 5, 6, 5).Heading); // right
            Console.WriteLine(new Vector2(5, 5, 5, 6).Heading); // down
            Console.WriteLine(new Vector2(5, 5, 4, 5).Heading); // left


            //Console.WriteLine("3,4 - 8");
            //JustDoIt("test1.txt");

            //Console.WriteLine("5,8 - 33");
            //JustDoIt("test2.txt");

            //Console.WriteLine("1,2 - 35");
            //JustDoIt("test3.txt");

            //Console.WriteLine("6,3 - 41");
            //JustDoIt("test4.txt");

            Console.WriteLine("11,13 - 210");
            JustDoIt("test5.txt");

            Console.WriteLine("Mine!");
            JustDoIt("input.txt");
        }

        private static void JustDoIt(string file)
        {
            var input = File.ReadAllLines(file);

            var roids = new List<Asteroid>();

            for (int y = 0; y < input.Length; y++)
            {
                var cells = input[y].ToCharArray();
                for (int x = 0; x < cells.Length; x++)
                {
                    if (cells[x] == '#')
                    {
                        roids.Add(new Asteroid(x, y));
                    }
                }
            }

            roids.ForEach(x => x.PopulateVectors(roids));
            var best = roids.OrderByDescending(x => x.VisibleAsteroidCount).First();
            Console.WriteLine($"{best.X},{best.Y} - {best.VisibleAsteroidCount}");
            Console.WriteLine("");

            var laserHeading = -1.0;
            for (int i = 0; i < 199; i++)
            {
                var next = best.VectorsToOthers.Where(x=> x.Heading > laserHeading).OrderBy(x => x.Heading).ThenBy(x => x.Magnitude).First();
                Console.WriteLine($"Removing ({i+1}) : {best.X + next.X},{best.Y + next.Y} at angle {next.Heading} and distance {next.Magnitude}");
                best.VectorsToOthers.Remove(next);
                laserHeading = next.Heading;
            }

            var twohundred = best.VectorsToOthers.OrderBy(x => x.Heading > laserHeading).ThenBy(x => x.Magnitude).First();

            Console.WriteLine($"Part 2 : {best.X + twohundred.X},{best.Y + twohundred.Y}");
        }
    }
}
