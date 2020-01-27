using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019
{
    public class DayOne : ProgramBase
    {
        private static decimal _totalFuel;

        public override void Solve()
        {
            List<int> input = new List<int>();
            var inputFile = File.ReadLines(".\\Inputs\\01.txt");

            foreach (var inputLine in inputFile)
            {
                input.Add(Convert.ToInt32(inputLine));
            }

            foreach (int mass in input)
            {
                var fuel = GetFuel(mass);
                _totalFuel += fuel;
            }

            Console.WriteLine(_totalFuel);
            Console.ReadLine();
        }

        private static decimal GetFuel(decimal mass)
        {
            decimal fuelRequired = Math.Floor(mass / 3) - 2;

            if (Math.Floor((fuelRequired / 3) - 2) <= 0)
                return fuelRequired;

            return fuelRequired + GetFuel(fuelRequired);
        }
    }
}
