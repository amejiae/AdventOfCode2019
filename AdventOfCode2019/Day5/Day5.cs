using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Day5
{
    public class Day5 : ProgramBase
    {
        private static int[] _inputAsIntArray;
        private const int SystemId = 5;
        private static int _instructionPointer;

        public override void Solve()
        {
            var inputAll = new List<int>();
            var inputFile = File.ReadAllText(".\\Input.txt");
            var allInputs = inputFile.Split(',');

            foreach (var inputLine in allInputs)
            {
                inputAll.Add(Convert.ToInt32(inputLine));
            }

            _inputAsIntArray = inputAll.ToArray();

            RunProgram(SystemId);

            Console.ReadLine();
        }

        private static void RunProgram(int inputValue)
        {
            while (true)
            {
                int opCode = _inputAsIntArray[_instructionPointer];
                if (opCode >= 100)
                {
                    var longOpCode = IntToIntArray(opCode).ToList();
                    opCode = longOpCode[longOpCode.Count - 1];
                }
                else
                {
                    opCode = _inputAsIntArray[_instructionPointer];
                }

                switch (opCode)
                {
                    case 1:
                        HandleAddition();
                        break;
                    case 2:
                        HandleMultiplication();
                        break;
                    case 3:
                        HandleSave();
                        break;
                    case 4:
                        HandleOutput();
                        break;
                    case 5:
                        HandleJumpIfTrue();
                        break;
                    case 6:
                        HandleJumpIfFalse();
                        break;
                    case 7:
                        HandleLessThan();
                        break;
                    case 8:
                        HandleEquals();
                        break;
                    case 99:
                    {
                        //handle exit
                        return;
                    }
                }
            }
        }

        private static void HandleAddition()
        {
            int modeOfFirstParameter = 0;
            int modeOfSecondParameter = 0;

            var opCode = _inputAsIntArray[_instructionPointer];

            if (opCode >= 100)
            {
                var longOpCode = IntToIntArray(opCode).ToList();
                modeOfFirstParameter = longOpCode[longOpCode.Count - 3];
                modeOfSecondParameter = longOpCode[longOpCode.Count - 4];
            }

            int operand1 = _inputAsIntArray[_instructionPointer + 1];
            int operand2 = _inputAsIntArray[_instructionPointer + 2];
            int outputIndex = _inputAsIntArray[_instructionPointer + 3];

            _inputAsIntArray[outputIndex] = GetValue(modeOfFirstParameter, operand1) + GetValue(modeOfSecondParameter, operand2);
            _instructionPointer += 4;
        }

        private static void HandleMultiplication()
        {
            int modeOfFirstParameter = 0;
            int modeOfSecondParameter = 0;

            var opCode = _inputAsIntArray[_instructionPointer];

            if (opCode >= 100)
            {
                var longOpCode = IntToIntArray(opCode).ToList();
                modeOfFirstParameter = longOpCode[longOpCode.Count - 3];
                modeOfSecondParameter = longOpCode[longOpCode.Count - 4];
            }

            int operand1 = _inputAsIntArray[_instructionPointer + 1];
            int operand2 = _inputAsIntArray[_instructionPointer + 2];
            int outputIndex = _inputAsIntArray[_instructionPointer + 3];

            _inputAsIntArray[outputIndex] = GetValue(modeOfFirstParameter, operand1) * GetValue(modeOfSecondParameter, operand2);
            _instructionPointer += 4;
        }

        private static void HandleSave()
        {
            int operand1 = _inputAsIntArray[_instructionPointer + 1];

            _inputAsIntArray[operand1] = SystemId;
            _instructionPointer += 2;
        }

        private static void HandleOutput()
        {
            int modeOfFirstParameter = 0;

            var opCode = _inputAsIntArray[_instructionPointer];
            if (opCode >= 100)
            {
                var longOpCode = IntToIntArray(opCode).ToList();
                modeOfFirstParameter = longOpCode[longOpCode.Count - 3];
            }

            int operand1 = _inputAsIntArray[_instructionPointer + 1];

            Console.WriteLine(GetValue(modeOfFirstParameter, operand1));
            _instructionPointer += 2;
        }

        private static void HandleJumpIfTrue()
        {
            int modeOfFirstParameter = 0;
            int modeOfSecondParameter = 0;

            var opCode = _inputAsIntArray[_instructionPointer];
            if (opCode >= 100)
            {
                var longOpCode = IntToIntArray(opCode).ToList();
                modeOfFirstParameter = longOpCode[longOpCode.Count - 3];
                modeOfSecondParameter = longOpCode[longOpCode.Count - 4];
            }

            int operand1 = _inputAsIntArray[_instructionPointer + 1];
            int operand2 = _inputAsIntArray[_instructionPointer + 2];

            if (GetValue(modeOfFirstParameter, operand1) != 0)
            {
                _instructionPointer = GetValue(modeOfSecondParameter, operand2);
            }
            else
            {
                _instructionPointer += 3;
            }
        }

        private static void HandleJumpIfFalse()
        {
            int modeOfFirstParameter = 0;
            int modeOfSecondParameter = 0;

            var opCode = _inputAsIntArray[_instructionPointer];
            if (opCode >= 100)
            {
                var longOpCode = IntToIntArray(opCode).ToList();
                modeOfFirstParameter = longOpCode[longOpCode.Count - 3];
                modeOfSecondParameter = longOpCode[longOpCode.Count - 4];
            }

            int operand1 = _inputAsIntArray[_instructionPointer + 1];
            int operand2 = _inputAsIntArray[_instructionPointer + 2];

            if (GetValue(modeOfFirstParameter, operand1) == 0)
            {
                _instructionPointer = GetValue(modeOfSecondParameter, operand2);
            }
            else
            {
                _instructionPointer += 3;
            }
        }

        private static void HandleLessThan()
        {
            int modeOfFirstParameter = 0;
            int modeOfSecondParameter = 0;

            var opCode = _inputAsIntArray[_instructionPointer];
            if (opCode >= 100)
            {
                var longOpCode = IntToIntArray(opCode).ToList();
                modeOfFirstParameter = longOpCode[longOpCode.Count - 3];
                modeOfSecondParameter = longOpCode[longOpCode.Count - 4];
            }

            int operand1 = _inputAsIntArray[_instructionPointer + 1];
            int operand2 = _inputAsIntArray[_instructionPointer + 2];
            int operand3 = _inputAsIntArray[_instructionPointer + 3];

            if (GetValue(modeOfFirstParameter, operand1) < GetValue(modeOfSecondParameter, operand2))
            {
                _inputAsIntArray[operand3] = 1;
            }
            else
            {
                _inputAsIntArray[operand3] = 0;
            }

            _instructionPointer += 4;
        }

        private static void HandleEquals()
        {
            int modeOfFirstParameter = 0;
            int modeOfSecondParameter = 0;

            var opCode = _inputAsIntArray[_instructionPointer];
            if (opCode >= 100)
            {
                var longOpCode = IntToIntArray(opCode).ToList();
                modeOfFirstParameter = longOpCode[longOpCode.Count - 3];
                modeOfSecondParameter = longOpCode[longOpCode.Count - 4];
            }

            int operand1 = _inputAsIntArray[_instructionPointer + 1];
            int operand2 = _inputAsIntArray[_instructionPointer + 2];
            int operand3 = _inputAsIntArray[_instructionPointer + 3];

            if (GetValue(modeOfFirstParameter, operand1) == GetValue(modeOfSecondParameter, operand2))
            {
                _inputAsIntArray[operand3] = 1;
            }
            else
            {
                _inputAsIntArray[operand3] = 0;
            }

            _instructionPointer += 4;
        }

        private static int GetValue(int parameterMode, int value)
        {
            if (parameterMode == 0)
            {
                return _inputAsIntArray[value];
            }

            return value;
        }

        private static int[] IntToIntArray(int n)
        {
            var result = new[] { 0, 0, 0, 0 };
            for (int i = result.Length - 1; i >= 0; i--)
            {
                result[i] = n % 10;
                n /= 10;
            }
            return result;
        }
    }
}
