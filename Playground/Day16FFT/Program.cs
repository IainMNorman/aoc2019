using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day16FFT
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "59708372326282850478374632294363143285591907230244898069506559289353324363446827480040836943068215774680673708005813752468017892971245448103168634442773462686566173338029941559688604621181240586891859988614902179556407022792661948523370366667688937217081165148397649462617248164167011250975576380324668693910824497627133242485090976104918375531998433324622853428842410855024093891994449937031688743195134239353469076295752542683739823044981442437538627404276327027998857400463920633633578266795454389967583600019852126383407785643022367809199144154166725123539386550399024919155708875622641704428963905767166129198009532884347151391845112189952083025";

            var test = "80871224585914546619083218645595";

            Part1(input);
            Part2(input);        
        }

        static void Part1(string input)
        {
            var inputArray = input.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
            var output = new int[inputArray.Length];
            output = inputArray.ToArray();

            for (int i = 0; i < 100; i++)
            {
                output = FFTStep(output);
            }

            Console.WriteLine($"Part1: {string.Join(string.Empty, output.Take(8))}");
        }

        static void Part2(string puzzle)
        {
            var input = string.Concat(Enumerable.Repeat(puzzle, 10000));

            var offset = int.Parse(puzzle.Substring(0, 7));

            var signal = input.Substring(offset).ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();

            for (int phase = 1; phase <= 100; phase++)
            {
                for (int i = signal.Length - 1; i >= 0; i--)
                {
                    signal[i] = Math.Abs((i + 1 >= signal.Length ? 0 : signal[i + 1]) + signal[i]) % 10;
                }
            }

            Console.WriteLine($"Part2: {string.Join(string.Empty, signal.Take(8))}");
        }

        static int[] FFTStep(int[] signal)
        {
            var basePattern = new int[] { 0, 1, 0, -1 };

            for (int i = 0; i < signal.Length; i++)
            {
                var total = 0;
                for (int j = 0; j < signal.Length; j++)
                {
                    total += signal[j] * basePattern[(j + 1) / (i + 1) % 4];
                }
                signal[i] = Math.Abs(total) % 10;
            }

            return signal;
        }
    }
}
