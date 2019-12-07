using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6Shite
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var nodes = new List<Node>();

            foreach (var line in input)
            {
                var sides = line.Split(")", StringSplitOptions.RemoveEmptyEntries);

                var parent = sides[0];
                var child = sides[1];

                if (!nodes.Any(x => x.Name == parent))
                {
                    nodes.Add(new Node { Name = parent });
                }

                if (!nodes.Any(x => x.Name == child))
                {
                    nodes.Add(new Node { Name = child });
                }

                nodes.First(x => x.Name == child).Parent = nodes.First(x => x.Name == parent);
            }

            // count all depths

            foreach (var node in nodes)
            {
                Queue<Node> q = new Queue<Node>();
                var depth = 0;
                q.Enqueue(node);

                while (q.Count > 0)
                {
                    var n = q.Dequeue();
                    if (n.Name != node.Name)
                    {
                        node.Parents.Add(n);
                    }
                    if (n.Parent != null)
                    {
                        depth++;
                        q.Enqueue(n.Parent);
                    }
                }

                node.Depth = depth;
            }

            Console.WriteLine(nodes.Sum(x => x.Depth));

            var me = nodes.First(x => x.Name == "YOU");
            var santa = nodes.First(x => x.Name == "SAN");

            var firstCommonDepth = me.Parents.Intersect<Node>(santa.Parents).Max(x => x.Depth);

            Console.WriteLine((me.Depth - 1 - firstCommonDepth) + (santa.Depth - 1 - firstCommonDepth));
        }
    }
}
