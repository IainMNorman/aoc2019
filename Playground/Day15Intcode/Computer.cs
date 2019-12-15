using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntCode
{
    public class Computer
    {
        public long[] memory;
        private long position = 0;
        private long relativeBase = 0;

        public Computer(long[] program, string name)
        {
            memory = program.ToArray();
            this.Name = name;
        }

        public Queue<long> Inputs { get; set; } = new Queue<long>();
        public Queue<long> Outputs { get; set; } = new Queue<long>();
        public string Name { get; set; }
        public bool Running { get; set; } = true;

        public long Run()
        {
            this.Running = true;
            while (Running)
            {
                RunStep();
            }
            return this.Outputs.Peek();
        }

        public void RunStep()
        {
            if (this.Running)
            {
                var ins = Helpers.ParseInstruction(memory, position, relativeBase);

                // Console.WriteLine($"Amp {this.Name} is running opcode : {ins.Opcode}");
                var curPos = position;
                position += 1 + ins.ReadParams.Length + ins.WriteAddresses.Length;

                switch (ins.Opcode)
                {
                    case 99:
                        // quit
                        this.Running = false;
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
                            memory[ins.WriteAddresses[0]] = this.Inputs.Dequeue();
                        }
                        else
                        {
                            position -= 1 + ins.ReadParams.Length + ins.WriteAddresses.Length;
                        }
                        break;
                    case 4:
                        // output
                        this.Outputs.Enqueue(ins.ReadParams[0]);
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
                    case 9:
                        // releative base offset
                        relativeBase += ins.ReadParams[0];
                        break;
                    default:
                        break;
                }
            }
        }

        internal void Load(long[] program)
        {
            memory = program.ToArray();
            position = 0;
        }
    }
}
