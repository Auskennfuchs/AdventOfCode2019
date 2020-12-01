using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day5
{
    class IntCode
    {
        private readonly int[] code;
        private int[] memory;
        private int cursorPos;
        private bool run = false;
        private string output = "";

        private int param1Mode, param2Mode, param3Mode;

        public IntCode(int[] code)
        {
            this.code = code;
        }

        public string RunCode(int[] input)
        {
            run = true;
            cursorPos = 0;
            memory = (int[])code.Clone();
            output = "";
            //            input.CopyTo(memory, 0);
            while (run && cursorPos < memory.Length)
            {
                var opCode = memory[cursorPos++];
                var instructionCode = opCode % 100;
                var paramModes = opCode / 100;
                param1Mode = paramModes % 10;
                param2Mode = (paramModes / 10) % 10;
                param3Mode = (paramModes / 100) % 10;
                switch (instructionCode)
                {
                    case 1:
                        RunAdd();
                        break;
                    case 2:
                        RunMultiply();
                        break;
                    case 3:
                        RunStore(input);
                        break;
                    case 4:
                        RunShow();
                        break;
                    case 5:
                        RunJumpIfTrue();
                        break;
                    case 6:
                        RunJumpIfFalse();
                        break;
                    case 7:
                        RunLessThan();
                        break;
                    case 8:
                        RunEquals();
                        break;
                    case 99:
                        run = false;
                        break;
                    default:
                        //                        run = false;
                        Console.WriteLine($"wrong opcode input {opCode}");
                        break;
                }
            }
            return output;
        }

        private void RunLessThan()
        {
            var (a, b) = GetDoubleParameter();
            var posRes = GetValue();
            memory[posRes] = a < b ? 1 : 0;
        }

        private void RunEquals()
        {
            var (a, b) = GetDoubleParameter();
            var posRes = GetValue();
            memory[posRes] = a == b ? 1 : 0;
        }

        private void RunAdd()
        {
            var (a, b) = GetDoubleParameter();
            var posRes = GetValue();
            memory[posRes] = a + b;
        }

        private void RunMultiply()
        {
            var (a, b) = GetDoubleParameter();
            var posRes = GetValue();
            memory[posRes] = a * b;
        }

        private void RunStore(int[] input)
        {
            var pos = GetValue();
            memory[pos] = input[0];
        }

        private void RunShow()
        {
            var val = GetSingleParameter();
            output += $"{val}";
        }

        private void RunJumpIfTrue()
        {
            var (check, newCursor) = GetDoubleParameter();
            if (check != 0)
            {
                cursorPos = newCursor;
            }
        }

        private void RunJumpIfFalse()
        {
            var (check, newCursor) = GetDoubleParameter();
            if (check == 0)
            {
                cursorPos = newCursor;
            }
        }

        private int GetSingleParameter()
        {
            return GetCodeValue(GetValue(), param1Mode);
        }
        private (int, int) GetDoubleParameter()
        {
            return (GetCodeValue(GetValue(), param1Mode),
                GetCodeValue(GetValue(), param2Mode));
        }

        private (int, int, int) GetTripleParameter()
        {
            return (GetCodeValue(GetValue(), param1Mode),
                GetCodeValue(GetValue(), param2Mode),
                GetCodeValue(GetValue(), param3Mode));
        }

        private int GetCodeValue(int pos, int paramMode)
        {
            return paramMode > 0 ? pos : memory[pos];
        }

        private int GetValue()
        {
            return memory[cursorPos++];
        }
    }
}
