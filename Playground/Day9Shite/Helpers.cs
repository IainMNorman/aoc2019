using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day7Shite2
{
    public static class Helpers
    {
        public static Instruction ParseInstruction(long[] memory, long position, long relativeBase)
        {
            var instruction = new Instruction();
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
                case 9:
                    noofReadParams = 1;
                    noofWriteAddresses = 0;
                    break;
                case 99:
                    noofReadParams = 0;
                    noofWriteAddresses = 0;
                    break;
                default:
                    break;
            }

            instruction.ReadParams = new long[noofReadParams];
            instruction.WriteAddresses = new long[noofWriteAddresses];

            for (int i = 0; i < noofReadParams; i++)
            {
                var parameterMode = (opcodeValue / (int)Math.Pow(10, 2 + i)) % 10;
                var parameter = memory[position + i + 1];
                switch (parameterMode)
                {
                    case 0:
                        //position
                        instruction.ReadParams[i] = memory[parameter];
                        break;
                    case 1:
                        // immediate
                        instruction.ReadParams[i] = parameter;
                        break;
                    case 2:
                        // relative
                        instruction.ReadParams[i] = memory[parameter + relativeBase];
                        break;
                    default:
                        break;
                }
            }

            for (int i = 0; i < noofWriteAddresses; i++)
            {
                var parameterMode = (opcodeValue / (int)Math.Pow(10, 2 + i + noofReadParams)) % 10;
                var parameter = memory[position + noofReadParams + i + 1];
                switch (parameterMode)
                {
                    case 0:
                        //position
                        instruction.WriteAddresses[i] = parameter;
                        break;
                    case 2:
                        // relative
                        instruction.WriteAddresses[i] = parameter + relativeBase;
                        break;
                    default:
                        break;
                }
            }
            // Console.WriteLine($"OP-{instruction.Opcode} : R {string.Join(',', instruction.ReadParams)} : W {string.Join(',', instruction.WriteAddresses)}");
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
