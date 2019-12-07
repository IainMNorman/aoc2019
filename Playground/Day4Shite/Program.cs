using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4Shite
{
    class Program
    {
        static void Main()
        {
            var count = 0;
            for (int i = 245182; i <= 790572; i++)
            {
                if (IsValidPassword(i))
                {
                    count++;
                }
            }
            Console.WriteLine(count);

            count = 0;
            for (int i = 245182; i <= 790572; i++)
            {
                if (IsValidPassword2(i))
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        internal static bool IsValidPassword(int password)
        {
            var digits = GetDigits(password).Reverse().ToArray();

            var hasRepeat = false;
            var onlyIncreases = true;
            var lastDigit = 0;

            foreach (var digit in digits)
            {
                if (digit < lastDigit)
                {
                    onlyIncreases = false;
                    break;
                }
                if (digit == lastDigit)
                {
                    hasRepeat = true;
                }
                lastDigit = digit;
            }

            return hasRepeat && onlyIncreases;
        }

        internal static bool IsValidPassword2(int password)
        {
            var digits = GetDigits(password).Reverse().ToArray();

            var hasRepeat = false;
            var onlyIncreases = true;
            var lastDigit = 0;

            foreach (var digit in digits)
            {
                if (digit < lastDigit)
                {
                    onlyIncreases = false;
                    break;
                }
                lastDigit = digit;
            }

            if (digits.GroupBy(i => i).Count(g => g.Count() == 2) > 0)
            {
                hasRepeat = true;
            }

            return hasRepeat && onlyIncreases;
        }

        internal static IEnumerable<int> GetDigits(int source)
        {
            while (source > 0)
            {
                var digit = source % 10;
                source /= 10;
                yield return digit;
            }
        }
    }
}
