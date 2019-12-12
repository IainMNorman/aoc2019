using Combinatorics.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Day12Orbits
{
    class Program
    {
        static void Main(string[] args)
        {
            //DoIt("test1.txt", 10);
            //DoIt("test2.txt", 100);
            Part2("test2.txt");
        }

        static void Part1(string filename, int steps)
        {
            var lines = File.ReadAllLines(filename);

            var moons = new List<Moon>();

            var name = 0;

            foreach (var line in lines)
            {
                var coords = Regex.Matches(line, @"-?[0-9]\d*(\.\d+)?");
                moons.Add(new Moon()
                {
                    Name = name.ToString(),
                    Velocity = new Vector3(0, 0, 0),
                    Position = new Vector3(
                        float.Parse(coords[0].Value),
                        float.Parse(coords[1].Value),
                        float.Parse(coords[2].Value))
                });
                name++;
            }

            var pairs = new Combinations<Moon>(moons, 2);

            for (int i = 0; i < steps; i++)
            {
                //Console.WriteLine($"After {i} steps:");
                //moons.ForEach(x => Console.WriteLine($"pos={x.Position}, vel={x.Velocity}"));
                //Console.WriteLine("");

                // calculate gravity
                foreach (var pair in pairs)
                {
                    var moonA = pair[0];
                    var moonB = pair[1];

                    if (moonA.Position.X > moonB.Position.X)
                    {
                        moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(-1, 0, 0));
                        moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(1, 0, 0));
                    }

                    if (moonA.Position.X < moonB.Position.X)
                    {
                        moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(1, 0, 0));
                        moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(-1, 0, 0));
                    }

                    if (moonA.Position.Y > moonB.Position.Y)
                    {
                        moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(0, -1, 0));
                        moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(0, 1, 0));
                    }

                    if (moonA.Position.Y < moonB.Position.Y)
                    {
                        moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(0, 1, 0));
                        moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(0, -1, 0));
                    }

                    if (moonA.Position.Z > moonB.Position.Z)
                    {
                        moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(0, 0, -1));
                        moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(0, 0, 1));
                    }

                    if (moonA.Position.Z < moonB.Position.Z)
                    {
                        moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(0, 0, 1));
                        moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(0, 0, -1));
                    }
                }

                moons.ForEach(x => x.ApplyVelocity());
            }

            Console.WriteLine(moons.Sum(x => x.TotalEnergy));
        }

        static void Part2(string filename)
        {
            var lines = File.ReadAllLines(filename);

            var moons = new List<Moon>();

            var name = 0;

            foreach (var line in lines)
            {
                var coords = Regex.Matches(line, @"-?[0-9]\d*(\.\d+)?");
                moons.Add(new Moon()
                {
                    Name = name.ToString(),
                    Velocity = new Vector3(0, 0, 0),
                    Position = new Vector3(
                        float.Parse(coords[0].Value),
                        float.Parse(coords[1].Value),
                        float.Parse(coords[2].Value))
                });
                name++;
            }

            var pairs = new Combinations<Moon>(moons, 2);

            var states = new HashSet<(float, float, float, float, float, float, float, float)>();

            var step = 0;
            while (true)
            {
                step++;

                foreach (var pair in pairs)
                {
                    CalcVelocity(pair);
                }

                moons.ForEach(x => x.ApplyVelocity());

                var state = (moons[0].Position.X, moons[1].Position.X, moons[2].Position.X, moons[3].Position.X, moons[0].Velocity.X, moons[1].Velocity.X, moons[2].Velocity.X, moons[3].Velocity.X);

                if (states.Contains(state))
                {
                    break;
                }
                else
                {
                    states.Add(state);
                }
            }

            var xSteps = step;
            
        }

        private static void CalcVelocity(IList<Moon> pair)
        {
            var moonA = pair[0];
            var moonB = pair[1];

            if (moonA.Position.X > moonB.Position.X)
            {
                moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(-1, 0, 0));
                moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(1, 0, 0));
            }

            if (moonA.Position.X < moonB.Position.X)
            {
                moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(1, 0, 0));
                moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(-1, 0, 0));
            }

            if (moonA.Position.Y > moonB.Position.Y)
            {
                moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(0, -1, 0));
                moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(0, 1, 0));
            }

            if (moonA.Position.Y < moonB.Position.Y)
            {
                moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(0, 1, 0));
                moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(0, -1, 0));
            }

            if (moonA.Position.Z > moonB.Position.Z)
            {
                moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(0, 0, -1));
                moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(0, 0, 1));
            }

            if (moonA.Position.Z < moonB.Position.Z)
            {
                moonA.Velocity = Vector3.Add(moonA.Velocity, new Vector3(0, 0, 1));
                moonB.Velocity = Vector3.Add(moonB.Velocity, new Vector3(0, 0, -1));
            }
        }
    }
}
