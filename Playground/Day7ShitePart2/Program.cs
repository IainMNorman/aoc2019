using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7Shite2
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = "3,8,1001,8,10,8,105,1,0,0,21,42,63,76,101,114,195,276,357,438,99999,3,9,101,2,9,9,102,5,9,9,1001,9,3,9,1002,9,5,9,4,9,99,3,9,101,4,9,9,102,5,9,9,1001,9,5,9,102,2,9,9,4,9,99,3,9,1001,9,3,9,1002,9,5,9,4,9,99,3,9,1002,9,2,9,101,5,9,9,102,3,9,9,101,2,9,9,1002,9,3,9,4,9,99,3,9,101,3,9,9,102,2,9,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,99";
            program = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            //program = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";
            var disk = program
                    .Split(',')
                    .Select(x => int.Parse(x))
                    .ToArray();

            var phases = new List<int>() { 5, 6, 7, 8, 9 };
            var settingPerms = Helpers.GetPermutations<int>(phases, 5);

            var allPerms = new List<Permutation>();

            foreach (var phaseSettings in settingPerms)
            {
                var ampA = new Computer(disk, "A");
                var ampB = new Computer(disk, "B");
                var ampC = new Computer(disk, "C");
                var ampD = new Computer(disk, "D");
                var ampE = new Computer(disk, "E");

                ampA.Outputs = ampB.Inputs;
                ampB.Outputs = ampC.Inputs;
                ampC.Outputs = ampD.Inputs;
                ampD.Outputs = ampE.Inputs;
                ampE.Outputs = ampA.Inputs;

                ampA.Inputs.Push(0);
                ampA.Inputs.Push(phaseSettings.ToArray()[0]);
                ampB.Inputs.Push(phaseSettings.ToArray()[1]);
                ampC.Inputs.Push(phaseSettings.ToArray()[2]);
                ampD.Inputs.Push(phaseSettings.ToArray()[3]);
                ampE.Inputs.Push(phaseSettings.ToArray()[4]);

                while (ampE.Running)
                {
                    ampA.RunStep();
                    ampB.RunStep();
                    ampC.RunStep();
                    ampD.RunStep();
                    ampE.RunStep();
                }

                var thrusterInput = ampE.Outputs.Peek();
                allPerms.Add(new Permutation() { PhaseSettings = phaseSettings.ToArray(), ThrusterInput = thrusterInput });
            }

            var answer = allPerms.Max(x => x.ThrusterInput);

            Console.WriteLine(answer);
        }
        
    }
}
