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

            var test = "69317163492948606335995924319873";

            //Part1(input);
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

            Console.WriteLine(string.Join(string.Empty, output.Take(8)));
        }

        static void Part2(string puzzle)
        {
            var input = string.Concat(Enumerable.Repeat(puzzle, 10000));

            var offset = int.Parse(puzzle.Substring(0, 7));

            var inputArray = input.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
            var output = new int[inputArray.Length];
            output = inputArray.ToArray();

            for (int i = 0; i < 100; i++)
            {
                output = FFTStep(output);
                Console.WriteLine($"Phase {i + 1}");
            }

            Console.WriteLine(string.Join(string.Empty, output.Skip(offset).Take(8)));
        }

        static int[] FFTStep(int[] input)
        {
            var output = new int[input.Length];
            var basePattern = new int[] { 0, 1, 0, -1 };

            for (int i = 0; i < input.Length; i++)
            {
                //if (i % 1000 == 0) Console.WriteLine($"Step {i}");

                var total = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    total += input[j] * basePattern[((j + 1) % (i + 1)) % 4];
                }

                output[i] = Math.Abs(total) % 10;

                Console.WriteLine(string.Join(',', output.Take(200)));
            }

            return output;
        }
    }
}
