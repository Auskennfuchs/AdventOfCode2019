using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day2
{
    class IntCode
    {
        private int[] code;

        public IntCode(int[] code)
        {
            this.code = code;
        }

        public int[] RunCode()
        {
            bool run = true;
            int cursorPos = 0;
            while (run || cursorPos >= code.Length)
            {
                int opCode = code[cursorPos++];
                switch (opCode)
                {
                    case 1:
                        RunAdd(code[cursorPos++], code[cursorPos++], code[cursorPos++]);
                        break;
                    case 2:
                        RunMultiply(code[cursorPos++], code[cursorPos++], code[cursorPos++]);
                        break;
                    case 99:
                        run = false;
                        break;
                    default:
                        Console.WriteLine($"wrong opcode input {opCode}");
                        break;
                }
            }
            return code;
        }

        private void RunAdd(int posA, int posB, int posRes)
        {
            int a = code[posA];
            int b = code[posB];
            code[posRes] = a + b;
        }

        private void RunMultiply(int posA, int posB, int posRes)
        {
            int a = code[posA];
            int b = code[posB];
            code[posRes] = a * b;
        }
    }
}
