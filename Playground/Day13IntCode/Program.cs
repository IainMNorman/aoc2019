using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace IntCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part1("moneygrab.txt");
            Part2("input.txt");
        }

        private static void Part2(string path)
        {
            var program = File.ReadAllText(path);

            var disk = program
                    .Split(',')
                    .Select(x => long.Parse(x))
                    .ToArray();

            Array.Resize(ref disk, disk.Length * 1000);

            var comp = new Computer(disk.ToArray(), "A");

            var screenMap = new int[36, 21];
            int score = 0;
            int frame = 0;
            int ballX = 0;
            int paddleX = 0;

            comp.SetFirstInt(2);

            while (comp.Running)
            {
                comp.RunStep();

                if (comp.Outputs.Count == 3)
                {
                    var x = comp.Outputs.Dequeue();
                    var y = comp.Outputs.Dequeue();
                    var value = comp.Outputs.Dequeue();

                    if (x == -1 && y == 0)
                    {
                        score = (int)value;
                    }
                    else
                    {
                        screenMap[x, y] = (int)value;
                        if (value == 3) paddleX = (int)x;
                        if (value == 4) ballX = (int)x;
                    }

                    if (paddleX < ballX)
                    {
                        comp.Inputs.Clear();
                        comp.Inputs.Enqueue(1);
                    }

                    if (paddleX > ballX)
                    {
                        comp.Inputs.Clear();
                        comp.Inputs.Enqueue(-1);
                    }

                    if (paddleX == ballX)
                    {
                        comp.Inputs.Clear();
                        comp.Inputs.Enqueue(0);
                    }

                    // if (frame % 2 == 0) DrawScreen(screenMap, score, frame);
                    frame++;
                }
            }

            Console.WriteLine(score);
        }

        private static void DrawScreen(int[,] screenMap, int score, int frame)
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < screenMap.GetLength(1); y++)
            {
                for (int x = 0; x < screenMap.GetLength(0); x++)
                {
                    switch (screenMap[x, y])
                    {
                        case 0:
                            Console.Write(' ');
                            break;
                        case 1:
                            Console.Write('█');
                            break;
                        case 2:
                            Console.Write('▒');
                            break;
                        case 3:
                            Console.Write('_');
                            break;
                        case 4:
                            Console.Write('■');
                            break;
                        default:
                            break;
                    }
                }
                Console.Write("\n");
            }
            Console.Write("\nScore: " + score);
            Console.Write("\nFrame: " + frame);
        }

        private static void Part1(string path)
        {
            var program = File.ReadAllText(path);

            var disk = program
                    .Split(',')
                    .Select(x => long.Parse(x))
                    .ToArray();

            Array.Resize(ref disk, disk.Length * 1000);

            var comp = new Computer(disk.ToArray(), "A");
            var tiles = new List<GameTile>();

            comp.Run();

            Console.WriteLine(comp.Outputs.ToList().Where((x, i) => (i - 2) % 3 == 0).Count(x => x == 2));
        }
    }
}


//9131 too high