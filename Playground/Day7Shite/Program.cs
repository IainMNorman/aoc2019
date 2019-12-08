using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7Shite
{
    class Program
    {
        private static List<int> outputs;

        private static Stack<int> inputs;

        static void Main(string[] args)
        {
            var program = "3,8,1001,8,10,8,105,1,0,0,21,42,63,76,101,114,195,276,357,438,99999,3,9,101,2,9,9,102,5,9,9,1001,9,3,9,1002,9,5,9,4,9,99,3,9,101,4,9,9,102,5,9,9,1001,9,5,9,102,2,9,9,4,9,99,3,9,1001,9,3,9,1002,9,5,9,4,9,99,3,9,1002,9,2,9,101,5,9,9,102,3,9,9,101,2,9,9,1002,9,3,9,4,9,99,3,9,101,3,9,9,102,2,9,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,99";
            program = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            program = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";
            var disk = program
                    .Split(',')
                    .Select(x => int.Parse(x))
                    .ToArray();

            outputs = new List<int>();
            inputs = new Stack<int>();

            var phases = new List<int>() { 0, 1, 2, 3, 4 };
            var settingPerms = GetPermutations<int>(phases, 5);

            var allPerms = new List<Permutation>();

            foreach (var phaseSettings in settingPerms)
            {
                var thrusterInput = RunThrusters(phaseSettings.ToArray(), disk);
                allPerms.Add(new Permutation() { PhaseSettings = phaseSettings.ToArray(), ThrusterInput = thrusterInput });
            }

            var answer = allPerms.Max(x => x.ThrusterInput);

            Console.WriteLine(answer);
        }

        internal static int RunThrusters(int[] phaseSettings, int[] disk)
        {
            outputs.Add(0);

            foreach (var phaseSetting in phaseSettings)
            {
                // load program
                var memory = disk.ToArray();

                // add last output to inputs
                var input = outputs.Last();
                inputs.Push(outputs.Last());

                // add phase setting to inputs
                inputs.Push(phaseSetting);

                Console.WriteLine($"Ran with phase setting {phaseSetting} and input of {input} and the output was {Run(memory, 0)}");
            }

            return outputs.Last();
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
                    memory[ins.WriteAddresses[0]] = inputs.Pop();
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

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
