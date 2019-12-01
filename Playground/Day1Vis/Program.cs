using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Day1Vis
{
    public class Program
    {
        static void Main(string[] args)
        {
            var index = 0;

            using MD5 md5Hash = MD5.Create();
            var modules = File.ReadAllLines("input.txt").Select(x => new Module()
            {
                Weight = int.Parse(x),
                Index = index++,
                Hash = GetMd5Hash(md5Hash, index.ToString() + x),
                Fuel = 0
            });

            foreach (var module in modules)
            {
                Console.WriteLine(HexToInt(module.Hash.Substring(0, 1)));
                Console.WriteLine();
            }
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private static int HexToInt(string hex)
        {
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
