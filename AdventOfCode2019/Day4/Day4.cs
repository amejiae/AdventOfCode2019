using System;

namespace AdventOfCode2019.Day4
{
    public class Day4 : ProgramBase
    {
        public override void Solve()
        {
            int counter = 136818;
            int passwordCount = 0;
            while (counter <= 685979)
            {
                var intAsArray = IntToIntArray(counter);
                if (HasTwoAdjacentInts(intAsArray) && HasOnlyIncrement(intAsArray))
                {
                    passwordCount++;
                }

                counter++;
            }

            Console.WriteLine(passwordCount);
            Console.ReadLine();
        }

        private static int[] IntToIntArray(int n)
        {
            var result = new int[6];
            for (int i = result.Length - 1; i >= 0; i--)
            {
                result[i] = n % 10;
                n /= 10;
            }
            return result;
        }

        private static bool HasTwoAdjacentInts(int[] intAsArray)
        {
            for (int index = 0; index < intAsArray.Length - 1; index++)
            {
                if (index == 0)
                {
                    if (intAsArray[index] == intAsArray[index + 1] && (intAsArray[index] != intAsArray[index + 2]))
                        return true;
                }
                else if (index == 4)
                {
                    if (intAsArray[index] != intAsArray[index - 1] && (intAsArray[index] == intAsArray[index + 1]))
                        return true;
                }
                else
                {
                    if (intAsArray[index] != intAsArray[index - 1] && (intAsArray[index] == intAsArray[index + 1]) && (intAsArray[index] != intAsArray[index + 2]))
                        return true;
                }
            }

            return false;
        }

        private static bool HasOnlyIncrement(int[] intAsArray)
        {
            for (int index = 0; index < intAsArray.Length - 1; index++)
            {
                if (intAsArray[index] > intAsArray[index + 1])
                    return false;
            }

            return true;
        }
    }
}
