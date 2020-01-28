using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2019
{
    public class Day2 : ProgramBase
    {
        private static int[] _inputAsIntArray;

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

            int noun = 0;

            while (noun <= 99)
            {
                var verb = 0;
                while (verb <= 99)
                {
                    //reset the memory and try a new combination.
                    _inputAsIntArray = inputAll.ToArray();
                    _inputAsIntArray[1] = noun;
                    _inputAsIntArray[2] = verb;

                    FindValue(_inputAsIntArray);

                    verb++;
                }

                noun++;
            }

            Console.ReadLine();
        }

        private static void FindValue(int[] inputArray)
        {
            int instructionPointer = 0;

            while (true)
            {
                var input = inputArray[instructionPointer];
                var operand1 = inputArray[instructionPointer + 1];
                var operand2 = inputArray[instructionPointer + 2];
                var outputIndex = inputArray[instructionPointer + 3];

                switch (input)
                {
                    case 1:
                        //handle addition
                        inputArray[outputIndex] = inputArray[operand1] + inputArray[operand2];
                        break;
                    case 2:
                        //handle multiplication
                        inputArray[outputIndex] = inputArray[operand1] * inputArray[operand2];
                        break;
                    case 99:
                    {
                        if (_inputAsIntArray[0] == 19690720)
                        {
                        }

                        //handle exit
                        return;
                    }
                }

                instructionPointer += 4;
            }
        }
    }
}
