using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    class Day6 : ProgramBase
    {
        private int _orbitCounter = 0;
        private string[] _input;

        public override void Solve()
        {
            _input = File.ReadAllLines(".\\Inputs\\06.txt");

            int counter = _input.Length - 1;
            while (counter > 0)
            {
                _orbitCounter++;
                string orbitalRelation = _input[counter];
                string[] orbitObject = orbitalRelation.Split(')');

                FindOrbits(orbitObject);

                counter -= 1;
            }
        }

        private void FindOrbits(string[] orbitObject)
        {
            int counter = _input.Length - 1;
            string nextObject = orbitObject[0];
            
            while (nextObject != "COM")
            {
                counter -= 1;
                string[] nextOrbitalRelation = _input[counter].Split(')');

                if (nextObject == nextOrbitalRelation[1])
                {
                    nextObject = nextOrbitalRelation[0];
                    _orbitCounter++;
                }
            }
        }
    }
}
