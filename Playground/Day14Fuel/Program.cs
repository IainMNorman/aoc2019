using System;
using System.Collections.Generic;
using System.IO;

namespace Day14Fuel
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1("mine.txt");
            Part2("mine.txt");
        }

        private static void Part2(string input)
        {
            var refinery = new Refinery(input);

            var oreAmount = 1000000000000;
            var minFuel = oreAmount / refinery.Refine("FUEL", 1);
            var maxFuel = 2 * minFuel;
            while (maxFuel > minFuel + 1)
            {
                var prodFuel = minFuel + (maxFuel - minFuel) / 2;
                if (refinery.Refine("FUEL", prodFuel) > oreAmount)
                {
                    maxFuel = prodFuel;
                }
                else
                {
                    minFuel = prodFuel;
                }
            }

            Console.WriteLine($"With {oreAmount} ORE we can make {minFuel} FUEL");
        }

        static void Part1(string input)
        {
            var refinery = new Refinery(input);
            Console.WriteLine($"{refinery.Refine("FUEL", 1)} ORE required");
        }
    }
}
