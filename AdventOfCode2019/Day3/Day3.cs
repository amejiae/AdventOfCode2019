using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace AdventOfCode2019
{
    public class Day3 : ProgramBase
    {
        private const int CenterX = 15000;
        private const int CenterY = 15000;

        private static char[,] _grid;
        private static int _currentPositionX;
        private static int _currentPositionY;

        private static int _allmovesLine = 0;

        private static readonly List<int> AllManhattanDistances = new List<int>();
        private static readonly List<Tuple<int, int>> Crossings = new List<Tuple<int, int>>();
        private static List<CrossingWithSteps> crossingWithStepses = new List<CrossingWithSteps>();
            
        public override void Solve()
        {
            _grid = new char[30000, 30000];

            _currentPositionX = CenterX;
            _currentPositionY = CenterY;

            var inputLines = File.ReadLines("Input.txt");
            string[] line1 = inputLines.First().Split(',');
            string[] line2 = inputLines.Last().Split(',');

            //Do all the moves for each line.
            DoLine1(line1, '1');

            //Reset the start position to the center.
            _currentPositionY = CenterX;
            _currentPositionX = CenterY;

            DoLine2(line2, '2');

            foreach (var crossing in Crossings)
            {
                AllManhattanDistances.Add(ManhattanDistance(CenterX, crossing.Item1, CenterY, crossing.Item2));
            }

            var shortest = AllManhattanDistances.Min();

            //Start again
            _currentPositionX = CenterX;
            _currentPositionY = CenterY;
            _allmovesLine = 0;

            DoLine1V2(line1, true);

            //Start again
            _currentPositionX = CenterX;
            _currentPositionY = CenterY;
            _allmovesLine = 0;

            DoLine1V2(line2, false);

            var lowestPath = crossingWithStepses.Select(c => c.CountLine2 + c.CountLine1).Min();

            Console.Write(shortest);
            Console.ReadLine();
        }

        private static void DoLine1V2(string[] moves, bool line1)
        {
            foreach (string move in moves)
            {
                DoMove1V2(move, line1);
            }
        }

        private static void DoMove1V2(string move, bool line1)
        {
            int moves = Convert.ToInt32(move.Substring(1));
            if (move.Contains("D"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionY -= 1;
                    _allmovesLine++;
                    CheckForCrossingV2(line1);
                    moveCounter++;
                }
            }
            else if (move.Contains("L"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionX -= 1;
                    _allmovesLine++;
                    CheckForCrossingV2(line1);
                    moveCounter++;
                }
            }
            else if (move.Contains("R"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionX += 1;
                    _allmovesLine++;
                    CheckForCrossingV2(line1);
                    moveCounter++;
                }
            }
            else if (move.Contains("U"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionY += 1;
                    _allmovesLine++;
                    CheckForCrossingV2(line1);
                    moveCounter++;
                }
            }
        }

        private static void CheckForCrossingV2(bool lineOne)
        {
            var currentCrossing = new Tuple<int, int>(_currentPositionX, _currentPositionY);
            if (Crossings.Contains(currentCrossing))
            {
                var exisitingCrossing = crossingWithStepses.FirstOrDefault(c => c.CrossingId.Item1 == currentCrossing.Item1 && c.CrossingId.Item2 == currentCrossing.Item2);
                if (lineOne)
                {
                    if (exisitingCrossing == null)
                    {
                        crossingWithStepses.Add(new CrossingWithSteps() { CountLine1 = _allmovesLine, CrossingId = new Tuple<int, int>(_currentPositionX, _currentPositionY) });
                    }
                    else
                    {
                        exisitingCrossing.CountLine1 = _allmovesLine;
                    }

                }
                else
                {
                    if (exisitingCrossing == null)
                    {
                        crossingWithStepses.Add(new CrossingWithSteps() { CountLine2 = _allmovesLine, CrossingId = new Tuple<int, int>(_currentPositionX, _currentPositionY) });
                    }
                    else
                    {
                        exisitingCrossing.CountLine2 = _allmovesLine;
                    }
                }
            }
        }

        private static void DoLine1(string[] moves, char lineMarker)
        {
            foreach (string move in moves)
            {
                DoMove1(move, lineMarker);
            }
        }

        private static void DoLine2(string[] moves, char lineMarker)
        {
            foreach (string move in moves)
            {
                DoMove2(move, lineMarker);
            }
        }

        private static void DoMove1(string move, char lineMarker)
        {
            int moves = Convert.ToInt32(move.Substring(1));
            if (move.Contains("D"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionY -= 1;
                    _grid[_currentPositionX, _currentPositionY] = lineMarker;
                    moveCounter++;
                }
            }
            else if (move.Contains("L"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionX -= 1;
                    _grid[_currentPositionX, _currentPositionY] = lineMarker;
                    moveCounter++;
                }
            }
            else if (move.Contains("R"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionX += 1;
                    _grid[_currentPositionX, _currentPositionY] = lineMarker;
                    moveCounter++;
                }
            }
            else if (move.Contains("U"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionY += 1;
                    _grid[_currentPositionX, _currentPositionY] = lineMarker;
                    moveCounter++;
                }
            }
        }

        private static void DoMove2(string move, char lineMarker)
        {
            int moves = Convert.ToInt32(move.Substring(1));
            if (move.Contains("D"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionY -= 1;
                    CheckForCrossing(_currentPositionX, _currentPositionY);
                    _grid[_currentPositionX, _currentPositionY] = lineMarker;
                    moveCounter++;
                }
            }
            else if (move.Contains("L"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionX -= 1;
                    CheckForCrossing(_currentPositionX, _currentPositionY);
                    _grid[_currentPositionX, _currentPositionY] = lineMarker;
                    moveCounter++;
                }
            }
            else if (move.Contains("R"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionX += 1;
                    CheckForCrossing(_currentPositionX, _currentPositionY);
                    _grid[_currentPositionX, _currentPositionY] = lineMarker;
                    moveCounter++;
                }
            }
            else if (move.Contains("U"))
            {
                int moveCounter = 0;
                while (moveCounter < moves)
                {
                    _currentPositionY += 1;
                    CheckForCrossing(_currentPositionX, _currentPositionY);
                    _grid[_currentPositionX, _currentPositionY] = lineMarker;
                    moveCounter++;
                }
            }
        }

        private static void CheckForCrossing(int currentPositionX, int currentPositionY)
        {
            if (_grid[currentPositionX, currentPositionY] == '1')
            {
                Crossings.Add(new Tuple<int, int>(currentPositionX, currentPositionY));
                _grid[currentPositionX, currentPositionY] = 'x';
            }
        }

        public static int ManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public class CrossingWithSteps
        {
            public Tuple<int, int> CrossingId { get; set; }
            public int CountLine1 { get; set; }
            public int CountLine2 { get; set; }
        }
    }
}
