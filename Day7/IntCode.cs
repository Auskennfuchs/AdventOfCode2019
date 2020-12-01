using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Day7
{
    class IntCode
    {

        private const int POSITION_MODE = 0;
        private const int IMMEDIATE_MODE = 1;
        private const int RELATIVE_MODE = 2;

        private readonly int[] code;
        private int[] memory;
        private int cursorPos = 0;
        private Queue<int> inputValues = new Queue<int>();
        private int baseShift = 0;

        private int param1Mode, param2Mode, param3Mode;

        public bool Finished { get; private set; }
        public bool Running { get; private set; }

        public int Result { get; private set; }

        public IntCode(int[] code)
        {
            this.code = code;
            Running = false;
        }

        public void Init(int[] input)
        {
            SetInput(input);
            Running = false;
            Finished = false;
            cursorPos = 0;
            Running = true;
            Finished = false;
            cursorPos = 0;
            baseShift = 0;
            memory = new int[code.Length * 20];
            code.CopyTo(memory, 0);
        }

        public int RunCode()
        {
            Running = true;
            while (Running && cursorPos < memory.Length)
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
                        RunStore();
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
                    case 9:
                        RunShiftBase();
                        break;
                    case 99:
                        Running = false;
                        Finished = true;
                        break;
                    default:
                        Console.WriteLine($"wrong opcode input {opCode}");
                        break;
                }
            }
            return Result;
        }

        public void PushInput(int val) => inputValues.Enqueue(val);

        public void SetInput(int[] input)
        {
            inputValues.Clear();
            Array.ForEach(input, inputValues.Enqueue);
        }

        private void RunLessThan()
        {
            var (a, b) = GetDoubleParameter();
            var posRes = GetWriteCodeValue(cursorPos++, param3Mode);
            memory[posRes] = a < b ? 1 : 0;
        }

        private void RunEquals()
        {
            var (a, b) = GetDoubleParameter();
            var posRes = GetWriteCodeValue(cursorPos++, param3Mode);
            memory[posRes] = a == b ? 1 : 0;
        }

        private void RunAdd()
        {
            var (a, b) = GetDoubleParameter();
            var posRes = GetWriteCodeValue(cursorPos++, param3Mode);
            memory[posRes] = a + b;
        }

        private void RunMultiply()
        {
            var (a, b) = GetDoubleParameter();
            var posRes = GetWriteCodeValue(cursorPos++, param3Mode);
            memory[posRes] = a * b;
        }

        private void RunStore()
        {
            var pos = GetWriteCodeValue(cursorPos++, param1Mode);
            memory[pos] = inputValues.Dequeue();
        }

        private void RunShow()
        {
            var val = GetSingleParameter();
            Result = val;
            Running = false;
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

        private void RunShiftBase()
        {
            baseShift = GetSingleParameter();
        }

        private int GetSingleParameter()
        {
            return GetReadCodeValue(cursorPos++, param1Mode);
        }
        private (int, int) GetDoubleParameter()
        {
            return (GetReadCodeValue(cursorPos++, param1Mode),
                GetReadCodeValue(cursorPos++, param2Mode));
        }

        private int GetReadCodeValue(int pos, int paramMode)
        {
            switch (paramMode)
            {
                case POSITION_MODE:
                    return memory[memory[pos]];
                case IMMEDIATE_MODE:
                    return memory[pos];
                case RELATIVE_MODE:
                    return memory[memory[pos] + baseShift];
            }
            throw new ArgumentException($"wrong paramMode {paramMode}");
        }

        private int GetWriteCodeValue(int pos, int paramMode)
        {
            switch (paramMode)
            {
                case POSITION_MODE:
                    return memory[pos];
                case IMMEDIATE_MODE:
                    throw new ArgumentException($"wrong paramMode {paramMode}");
                case RELATIVE_MODE:
                    return memory[pos] + baseShift;
            }
            throw new ArgumentException($"wrong paramMode {paramMode}");
        }
    }
}
