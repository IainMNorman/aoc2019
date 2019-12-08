using System;
using System.Collections.Generic;
using System.Text;

namespace Day7Shite
{
    public class Instruction
    {
        public int Opcode { get; set; }

        public int[] ReadParams { get; set; }

        public int[] WriteAddresses { get; set; }
    }
}
