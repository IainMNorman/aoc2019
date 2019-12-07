using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5Shite
{
    class Program
    {
        private static List<int> outputs;

        private static List<int> inputs;

        static void Main(string[] args)
        {
            var program = "3,225,1,225,6,6,1100,1,238,225,104,0,1101,82,10,225,101,94,44,224,101,-165,224,224,4,224,1002,223,8,223,101,3,224,224,1,224,223,223,1102,35,77,225,1102,28,71,225,1102,16,36,225,102,51,196,224,101,-3468,224,224,4,224,102,8,223,223,1001,224,7,224,1,223,224,223,1001,48,21,224,101,-57,224,224,4,224,1002,223,8,223,101,6,224,224,1,223,224,223,2,188,40,224,1001,224,-5390,224,4,224,1002,223,8,223,101,2,224,224,1,224,223,223,1101,9,32,224,101,-41,224,224,4,224,1002,223,8,223,1001,224,2,224,1,223,224,223,1102,66,70,225,1002,191,28,224,101,-868,224,224,4,224,102,8,223,223,101,5,224,224,1,224,223,223,1,14,140,224,101,-80,224,224,4,224,1002,223,8,223,101,2,224,224,1,224,223,223,1102,79,70,225,1101,31,65,225,1101,11,68,225,1102,20,32,224,101,-640,224,224,4,224,1002,223,8,223,1001,224,5,224,1,224,223,223,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,8,226,226,224,1002,223,2,223,1006,224,329,101,1,223,223,1008,677,677,224,102,2,223,223,1006,224,344,101,1,223,223,1107,226,677,224,102,2,223,223,1005,224,359,101,1,223,223,1008,226,226,224,1002,223,2,223,1006,224,374,1001,223,1,223,1108,677,226,224,1002,223,2,223,1006,224,389,1001,223,1,223,7,677,226,224,1002,223,2,223,1006,224,404,101,1,223,223,7,226,226,224,1002,223,2,223,1005,224,419,101,1,223,223,8,226,677,224,1002,223,2,223,1006,224,434,1001,223,1,223,7,226,677,224,1002,223,2,223,1006,224,449,1001,223,1,223,107,226,677,224,1002,223,2,223,1005,224,464,1001,223,1,223,1007,677,677,224,102,2,223,223,1005,224,479,101,1,223,223,1007,226,226,224,102,2,223,223,1005,224,494,1001,223,1,223,1108,226,677,224,102,2,223,223,1005,224,509,101,1,223,223,1008,677,226,224,102,2,223,223,1005,224,524,1001,223,1,223,1007,677,226,224,102,2,223,223,1005,224,539,101,1,223,223,1108,226,226,224,1002,223,2,223,1005,224,554,101,1,223,223,108,226,226,224,102,2,223,223,1005,224,569,101,1,223,223,108,677,677,224,102,2,223,223,1005,224,584,101,1,223,223,1107,226,226,224,1002,223,2,223,1006,224,599,101,1,223,223,8,677,226,224,1002,223,2,223,1006,224,614,1001,223,1,223,108,677,226,224,102,2,223,223,1006,224,629,1001,223,1,223,1107,677,226,224,1002,223,2,223,1006,224,644,1001,223,1,223,107,677,677,224,102,2,223,223,1005,224,659,101,1,223,223,107,226,226,224,102,2,223,223,1006,224,674,1001,223,1,223,4,223,99,226";

            var disk = program
                    .Split(',')
                    .Select(x => int.Parse(x))
                    .ToArray();

            var memory = disk.ToArray();

            outputs = new List<int>();
            inputs = new List<int>();

            inputs.Add(1);

            var result1 = Run(memory, 0);

            memory = disk.ToArray();

            inputs.Add(5);

            var result2 = Run(memory, 0);
        }

        internal static int Run(int[] memory, int position)
        {
            var ins = ParseInstruction(memory, position);

            position += 1 + ins.ReadParams.Length + ins.WriteAddresses.Length;

            switch (ins.Opcode)
            {
                case 99:
                    // quit
                    return outputs.Last();
                case 1:
                    // add
                    memory[ins.WriteAddresses[0]] = ins.ReadParams[0] + ins.ReadParams[1];
                    break;
                case 2:
                    // multiply
                    memory[ins.WriteAddresses[0]] = ins.ReadParams[0] * ins.ReadParams[1];
                    break;
                case 3:
                    // input
                    memory[ins.WriteAddresses[0]] = inputs.Last();
                    break;
                case 4:
                    // output
                    outputs.Add(ins.ReadParams[0]);
                    break;
                case 5:
                    // jump if true
                    if (ins.ReadParams[0] != 0)
                    {
                        position = ins.ReadParams[1];
                    }
                    break;
                case 6:
                    // jump if false
                    if (ins.ReadParams[0] == 0)
                    {
                        position = ins.ReadParams[1];
                    }
                    break;
                case 7:
                    // less than
                    memory[ins.WriteAddresses[0]] = ins.ReadParams[0] < ins.ReadParams[1] ? 1 : 0;
                    break;
                case 8:
                    // equals
                    memory[ins.WriteAddresses[0]] = ins.ReadParams[0] == ins.ReadParams[1] ? 1 : 0;
                    break;
                default:
                    break;
            }            

            var result = Run(memory, position);

            return result != 0 ? result : 0;
        }

        internal static Instruction ParseInstruction(int[] memory, int position)
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
    }
}
