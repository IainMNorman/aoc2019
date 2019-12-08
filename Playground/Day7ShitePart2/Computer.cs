using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day7Shite2
{
    public class Computer
    {
        private int[] memory;
        private int position = 0;

        public Computer(int[] program, string name)
        {
            memory = program.ToArray();
            this.Name = name;
        }

        public Stack<int> Inputs { get; set; } = new Stack<int>();
        public Stack<int> Outputs { get; set; } = new Stack<int>();
        public string Name { get; set; }
        public bool Running { get; set; } = true;

        public void RunStep()
        {
            if (this.Running)
            {
                var ins = Helpers.ParseInstruction(memory, position);

                // Console.WriteLine($"Amp {this.Name} is running opcode : {ins.Opcode}");
                var curPos = position;
                position += 1 + ins.ReadParams.Length + ins.WriteAddresses.Length;

                switch (ins.Opcode)
                {
                    case 99:
                        // quit
                        this.Running = false;
                        Console.WriteLine($"Amp {this.Name} halting and catching fire.");
                        break;
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
                        if (this.Inputs.Count > 0)
                        {
                            memory[ins.WriteAddresses[0]] = this.Inputs.Pop();
                            Console.WriteLine($"Amp {this.Name} got an input. Program cursor is {curPos}");
                        }
                        else
                        {
                            position -= 1 + ins.ReadParams.Length + ins.WriteAddresses.Length;
                        }
                        break;
                    case 4:
                        // output
                        Console.WriteLine($"Amp {this.Name} output {ins.ReadParams[0]}");
                        this.Outputs.Push(ins.ReadParams[0]);
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
            }
        }

        internal void Load(int[] program)
        {
            memory = program.ToArray();
            position = 0;
        }
    }
}
