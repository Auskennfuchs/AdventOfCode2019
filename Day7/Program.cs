using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        private static readonly int[] inputCode = new int[] { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 38, 55, 64, 89, 114, 195, 276, 357, 438, 99999, 3, 9, 101, 3, 9, 9, 102,
            3, 9, 9, 1001, 9, 5, 9, 4, 9, 99, 3, 9, 101, 2, 9, 9, 1002, 9, 3, 9, 101, 5, 9, 9, 4, 9, 99, 3, 9, 101, 3, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 4, 9, 101, 5, 9, 9,
            1002, 9, 5, 9, 101, 5, 9, 9, 102, 3, 9, 9, 4, 9, 99, 3, 9, 101, 3, 9, 9, 1002, 9, 4, 9, 101, 5, 9, 9, 102, 5, 9, 9, 1001, 9, 5, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9,
            3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101,
            1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9,
            3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 101,
            2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 101,
            1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 99, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9,
            3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 1002, 9,
            2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9,
            2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99 };

        private static readonly int[] inputCode2 = new int[] {3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,
27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5};
        static void Main(string[] args)
        {
            var intCode = new IntCode(inputCode);

            /*            intCode = new IntCode(new int[] { 3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0 });
                        intCode = new IntCode(new int[] { 3,23,3,24,1002,24,10,24,1002,23,-1,23,
            101,5,23,23,1,24,23,23,4,23,99,0,0});
                        intCode = new IntCode(new int[] {3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,
            1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0});*/

            var maxResult = 0;
            for (var i = 0; i <= 4; i++)
            {
                for (var j = 0; j <= 4; j++)
                {
                    for (var k = 0; k <= 4; k++)
                    {
                        for (var l = 0; l <= 4; l++)
                        {
                            for (var m = 0; m <= 4; m++)
                            {
                                if (i == j || i == k || i == l || i == m ||
                                    j == k || j == l || j == m ||
                                    k == l || k == m || l == m)
                                {
                                    continue;
                                }
                                var thruster = new int[] { i, j, k, l, m };
                                var result = 0;
                                foreach (var t in thruster)
                                {
                                    intCode.Init(new int[] { t, result });
                                    result = intCode.RunCode();
                                }
                                if (result > maxResult)
                                {
                                    maxResult = result;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(maxResult);
            Console.WriteLine("\nEnd!");
            Part2();
        }

        private static void Part2()
        {
            var maxResult = 0;
            for (var i = 0; i <= 4; i++)
            {
                for (var j = 0; j <= 4; j++)
                {
                    for (var k = 0; k <= 4; k++)
                    {
                        for (var l = 0; l <= 4; l++)
                        {
                            for (var m = 0; m <= 4; m++)
                            {
                                if (i == j || i == k || i == l || i == m ||
                                    j == k || j == l || j == m ||
                                    k == l || k == m || l == m)
                                {
                                    continue;
                                }
                                var thruster = new int[] { 5+i, 5+j, 5+k, 5+l, 5+m };

                                var thrusterOutput = runCombination(thruster);
                                if (thrusterOutput > maxResult)
                                {
                                    maxResult = thrusterOutput;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Max Thruster: {maxResult}");
        }

        private static int runCombination(int[] combination)
        {
            var amplifier = Enumerable.Range(0, 5).Select((i) =>
            {
                var a = new IntCode(inputCode);
                a.Init(new int[] { combination[i] });
                return a;
            }).ToList();
            amplifier[0].PushInput(0);

            var thrusterOutput = -1;
            var running = true;
            while (running)
            {
                amplifier.ForEach(a =>
                {
                    var index = amplifier.IndexOf(a);
                    var res = a.RunCode();
                    amplifier[(index + 1) % 5].PushInput(res);
                    if (index == amplifier.Count - 1)
                    {
                        thrusterOutput = res;
                    }
                    if (a.Finished)
                    {
                        running = false;
                    }
                });
            }
            return thrusterOutput;
        }
    }
}
