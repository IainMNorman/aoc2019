using System;
using System.Collections.Generic;
using System.Text;

namespace Day7Shite2
{
    public class Instruction
    {
        public long Opcode { get; set; }

        public long[] ReadParams { get; set; }

        public long[] WriteAddresses { get; set; }
    }
}
