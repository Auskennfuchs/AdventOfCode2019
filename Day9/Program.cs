using System;
using System.Linq;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Console.WriteLine("Hello World!");
        }

        private static void Part1()
        {
            var inputCode = new int[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
            var intCode = new IntCode(inputCode);
            intCode.Init(new int[] { });
            do
            {
                int result = intCode.RunCode();
                Console.Write(result);
            } while (!intCode.Finished);
        }
    }
}
