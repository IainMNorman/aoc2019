using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day18Keys
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1("./inputs/ex1.txt");
            Part1("./inputs/ex2.txt");
            Part1("./inputs/ex3.txt");
            Part1("./inputs/mine.txt");
        }

        static void Part1(string path)
        {
            var map = new List<MapTile>();
            var rows = File.ReadAllLines(path);
            for (int y = 0; y < rows.Length; y++)
            {
                var cols = rows[y].ToCharArray();
                for (int x = 0; x < cols.Length; x++)
                {
                    map.Add(new MapTile(new Point(x, y), cols[x]));
                }
            }
            var keys = map.Where(x => x.Value > 96).Select(x => x.Value);

            var finalKey = keys.OrderByDescending(o => (int)o).First();

            var q = new Queue<Option>();
            var paths = new List<Option>();

            var option1 = new Option();
            option1.Keys.Add('@');
            q.Enqueue(option1);
            var keyCount = 0;

            var prevDists = new Dictionary<(Point, Point, List<char>), int>();

            while (q.Count > 0)
            {                
                var opt = q.Dequeue();
                if (opt.Keys.Count() > keyCount)
                {
                    Console.WriteLine(opt.Keys.Count());
                    keyCount = opt.Keys.Count();
                }

                // check we've got all keys
                if (opt.Keys.Count() == keys.Count() + 1)
                {
                    paths.Add(opt);
                }
                else
                {

                    var reachable = new List<(char, int)>();
                    var startPoint = map.First(m => m.Value == opt.Keys.Last()).Point;

                    foreach (var key in keys)
                    {
                        if (!opt.Keys.Contains(key))
                        {
                            var dist = -1;
                            if (prevDists.ContainsKey((startPoint, map.First(m => m.Value == key).Point, opt.Keys))) {
                                dist = prevDists[(startPoint, map.First(m => m.Value == key).Point, opt.Keys)];
                            } else
                            {
                                dist = GetShortestPath(map, startPoint, map.First(m => m.Value == key).Point, opt.Keys);
                            }
                            if (dist > -1)
                            {
                                reachable.Add((key, dist));
                            }
                        }
                    }

                    foreach (var item in reachable.OrderBy(o => o.Item2))
                    {
                        var newOpt = new Option()
                        {
                            DistanceMoved = opt.DistanceMoved + item.Item2,
                            Keys = opt.Keys.Select(x => x).ToList()
                        };
                        newOpt.Keys.Add(item.Item1);
                        q.Enqueue(newOpt);
                    }
                }
            }

            Console.WriteLine(paths.OrderBy(p => p.DistanceMoved).First().DistanceMoved);
        }

        private static int GetShortestPath(IEnumerable<MapTile> map, Point startPoint, Point destination, List<char> keys)
        {
            var visited = new List<Point>();

            var q = new Queue<QNode>();

            q.Enqueue(new QNode(startPoint, 0));
            visited.Add(startPoint);

            while (q.Count > 0)
            {
                var curr = q.Peek();
                var pt = curr.Point;

                if (pt == destination)
                {
                    return curr.Distance;
                }

                q.Dequeue();

                // find adj
                var adjacents = new List<Point>()
                {
                    new Point(pt.X, pt.Y - 1),
                    new Point(pt.X + 1, pt.Y),
                    new Point(pt.X, pt.Y + 1),
                    new Point(pt.X - 1, pt.Y)
                };

                // queue all valid adjacents
                adjacents.ForEach(a =>
                {
                    if (map.Any(m =>
                        m.Point == a &&
                        m.Value != '#' &&
                        (m.Value > 96 || keys.Contains((char)(m.Value + 32)) || m.Value == '.' || m.Value == '@')
                        && !visited.Any(v => v == a)))
                    {
                        visited.Add(a);
                        q.Enqueue(new QNode(a, curr.Distance + 1));
                    }
                });
            }

            return -1;
        }
    }
}
