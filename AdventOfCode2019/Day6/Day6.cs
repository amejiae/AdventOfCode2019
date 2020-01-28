using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    class Day6 : ProgramBase
    {
        private int orbitCounter = 0;
        private string[] _input;

        public override void Solve()
        {
            _input = File.ReadAllLines(".\\Inputs\\06.txt");

            int counter = _input.Length - 1;
            while (counter > 0)
            {
                string orbitalRelation = _input[counter];
                string orbitObject = orbitalRelation.Split(')')[1];

                FindOrbits(orbitObject);

                counter -= 1;
            }
        }

        private void FindOrbits(string orbit)
        {
            int counter = _input.Length - 1;
            string orbitalRelation = _input[counter];
            string[] orbitObject = orbitalRelation.Split(')');

            while (orbitObject[0] != "COM")
            {
                var currentOrbitalRelation = _input[counter].Split(')');
                counter -= 1;
                string[] nextOrbitalRelation = _input[counter].Split(')');

                if (currentOrbitalRelation[0] == nextOrbitalRelation[1])
                {

                    orbitCounter++;
                }
            }
        }
    }
}
