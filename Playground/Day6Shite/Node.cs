using System;
using System.Collections.Generic;
using System.Text;

namespace Day6Shite
{
    public class Node
    {
        public string Name { get; set; }

        public Node Parent { get; set; }

        public List<Node> Parents { get; set; } = new List<Node>();

        public int Depth { get; set; }
    }
}
