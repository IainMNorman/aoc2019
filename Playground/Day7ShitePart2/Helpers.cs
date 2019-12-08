using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day7Shite2
{
    public static class Helpers
    {
        public static Instruction ParseInstruction(int[] memory, int position)
        {
            var instruction = new Instruction();
            // determine opcode
            var opcodeValue = memory[position];
            instruction.Opcode = opcodeValue % 100;

            var noofReadParams = 0;
            var noofWriteAddresses = 0;

            switch (instruction.Opcode)
            {
                case 1:
                    noofReadParams = 2;
                    noofWriteAddresses = 1;
                    break;
                case 2:
                    noofReadParams = 2;
                    noofWriteAddresses = 1;
                    break;
                case 3:
                    noofReadParams = 0;
                    noofWriteAddresses = 1;
                    break;
                case 4:
                    noofReadParams = 1;
                    noofWriteAddresses = 0;
                    break;
                case 5:
                    noofReadParams = 2;
                    noofWriteAddresses = 0;
                    break;
                case 6:
                    noofReadParams = 2;
                    noofWriteAddresses = 0;
                    break;
                case 7:
                    noofReadParams = 2;
                    noofWriteAddresses = 1;
                    break;
                case 8:
                    noofReadParams = 2;
                    noofWriteAddresses = 1;
                    break;
                case 99:
                    noofReadParams = 0;
                    noofWriteAddresses = 0;
                    break;
                default:
                    break;
            }

            instruction.ReadParams = new int[noofReadParams];
            instruction.WriteAddresses = new int[noofWriteAddresses];

            for (int i = 0; i < noofReadParams; i++)
            {
                // work out parameter mode.
                var mode = (opcodeValue / (int)Math.Pow(10, 2 + i)) % 10;

                if (mode == 0)
                {
                    instruction.ReadParams[i] = memory[memory[position + i + 1]];
                }
                else
                {
                    instruction.ReadParams[i] = memory[position + i + 1];
                }
            }

            for (int i = 0; i < noofWriteAddresses; i++)
            {
                instruction.WriteAddresses[i] = memory[position + noofReadParams + i + 1];
            }

            return instruction;
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
