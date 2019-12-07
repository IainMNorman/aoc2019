using System;
using System.Linq;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var disk = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,9,1,19,1,19,5,23,1,23,6,27,2,9,27,31,1,5,31,35,1,35,10,39,1,39,10,43,2,43,9,47,1,6,47,51,2,51,6,55,1,5,55,59,2,59,10,63,1,9,63,67,1,9,67,71,2,71,6,75,1,5,75,79,1,5,79,83,1,9,83,87,2,87,10,91,2,10,91,95,1,95,9,99,2,99,9,103,2,10,103,107,2,9,107,111,1,111,5,115,1,115,2,119,1,119,6,0,99,2,0,14,0"
                .Split(',')
                .Select(x => int.Parse(x))
                .ToArray();

            var memory = disk.ToArray();

            memory[1] = 12;
            memory[2] = 2;

            Console.WriteLine(Run(memory, 0));

            for (int n = 0; n < 100; n++)
            {
                for (int v = 0; v < 100; v++)
                {
                    memory = disk.ToArray();
                    memory[1] = n;
                    memory[2] = v;
                    if (Run(memory, 0) == 19690720)
                    {
                        Console.WriteLine($"{n * 100 + v}");
                        Environment.Exit(0);
                    }
                }
            }
        }

        static int Run(int[] memory, int position)
        {
            var opcode = memory[position];

            if (opcode == 99)
            {
                return memory[0];
            }

            var address1 = memory[position + 1];
            var address2 = memory[position + 2];
            var pointer = memory[position + 3];

            switch (opcode)
            {
                case 1:
                    memory[pointer] = memory[address1] + memory[address2];
                    break;
                case 2:
                    memory[pointer] = memory[address1] * memory[address2];
                    break;
                default:
                    break;
            }

            position += 4;

            var result = Run(memory, position);

            return result != 0 ? result : 0;
        }
    }
}
