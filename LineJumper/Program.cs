using System;
using System.Linq;

namespace LineJumper
{
    using System.Collections.Generic;

    internal class Program
    {
        static void Main()
        {
            var args = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var exclusionZonesReader = ExclusionZonesReader(args[2]);
            var solver = new LineJumpSolver(args[0], args[1], exclusionZonesReader);
            var result = solver.Solve();
            Console.WriteLine(result);
        }

        private static IEnumerable<Range> ExclusionZonesReader(int numToRead)
        {
            for (int i = 0; i < numToRead; i++)
            {
                var exclusionZoneMinMax = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                yield return new Range(exclusionZoneMinMax[0], exclusionZoneMinMax[1]);
            }
        }
    }

    public class LineJumpSolver
    {
        private readonly int _minDistance;
        private readonly int _jumpDistance;
        private int[] _progressLine;
        private int[] _regressLine;
        private Position[] _currentPositions;
        private Position[] _newPositions;
        private int _numCurrentPositions;
        private int _numNewPositions;
        public bool[] ExcludedSegments { get; }

        public LineJumpSolver(int minDistance, int jumpDistance, IEnumerable<Range> exclusionZones)
        {
            _minDistance = minDistance;
            _jumpDistance = jumpDistance;
            ExcludedSegments = new bool[_minDistance * 2];

            foreach (var exclusionZone in exclusionZones)
                for (int i = exclusionZone.Min; i <= exclusionZone.Max; i++)
                    ExcludedSegments[i] = true;
        }

        public int Solve()
        {
            if (_progressLine == null)
                BuildMovementGraph(_minDistance);

            int bestResult = -1;

            for (int i = _minDistance; i < _minDistance * 2; i++)
                if (_progressLine[i] != -1 && (bestResult == -1 || _progressLine[i] < bestResult))
                    bestResult = _progressLine[i];

            return bestResult;
        }

        private void Initialize()
        {
            _numCurrentPositions = 0;
            _numNewPositions = 0;
            _progressLine = new int[_minDistance * 2];
            _regressLine = new int[_minDistance * 2];
            _currentPositions = new Position[_minDistance * 2];
            _newPositions = new Position[_minDistance * 2];

            for (int i = 0; i < _progressLine.Length; i++)
            {
                _progressLine[i] = -1;
                _regressLine[i] = -1;
            }
        }

        private void BuildMovementGraph(int targetDistance)
        {
            Initialize();

            _progressLine[0] = 0;
            //_currentPositions[0] = 0;
            _numCurrentPositions = 1;

            while (_numCurrentPositions > 0)
            {
                _numNewPositions = 0;

                for (int i = 0; i < _numCurrentPositions; i++)
                {
                    var from = _currentPositions[i];

                    AddPositionForProgressing(from.Distance, from.Distance + _jumpDistance, targetDistance * 2 - 1);

                    for (int j = 0; j < from.Distance; j++)
                        AddPositionForProgressing(from.Distance, j, targetDistance * 2 - 1);
                }

                _numCurrentPositions = _numNewPositions;
                Array.Copy(_newPositions, _currentPositions, _numCurrentPositions);
            }
        }

        //void add_position_up(int from_height, int to_height, int max_height,
        //    positions pos, int* num_pos,
        //    int itching[], board min_moves)
        //{
        //    int distance = 1 + min_moves[from_height][0];
        //    if (to_height <= max_height && itching[to_height] == 0 &&
        //          (min_moves[to_height][0] == -1 ||
        //           min_moves[to_height][0] > distance)) {
        //        min_moves[to_height][0] = distance;
        //        pos[*num_pos] = (position){ to_height, 0};
        //        (*num_pos)++;
        //    }
        //}

        private void AddPositionForProgressing(int fromDistance, int toDistance, int maxHeight)
        {
            var line = _progressLine;
            var cost = 1 + line[fromDistance];
            bool condition = toDistance <= maxHeight && !ExcludedSegments[toDistance] &&
                             (line[toDistance] == -1 || line[toDistance] > cost);
            if (condition)
            {
                line[toDistance] = cost;
                _newPositions[_numNewPositions] = new Position(toDistance, line);
                _numNewPositions += 1;
            }
        }

        //void add_position_down(int from_height, int to_height,
        //    positions pos, int* num_pos,
        //    board min_moves)
        //{
        //    int distance = min_moves[from_height][1];
        //    if (to_height >= 0 && (min_moves[to_height][1] == -1 ||
        //                           min_moves[to_height][1] > distance))
        //    {
        //        min_moves[to_height][1] = distance;
        //        pos[*num_pos] = (position){ to_height, 1};
        //        (*num_pos)++;
        //    }
        //}

        private void AddPositionForRegressing(int fromDistance, int toDistance)
        {
            var line = _regressLine;
            var cost = 0 + line[fromDistance];
            bool condition = toDistance >= 0 &&
                             (line[toDistance] == -1 || line[toDistance] > cost);
            if (condition)
            {
                line[toDistance] = cost;
                _newPositions[_numNewPositions] = new Position(toDistance, line);
                _numNewPositions += 1;
            }
        }

        //void add_position_01(int from_height,
        //    positions pos, int* num_pos,
        //    board min_moves)
        //{
        //    int distance = 1 + min_moves[from_height][0];
        //    if (min_moves[from_height][1] == -1 ||
        //        min_moves[from_height][1] > distance)
        //    {
        //        min_moves[from_height][1] = distance;
        //        pos[*num_pos] = (position){ from_height, 1};
        //        (*num_pos)++;
        //    }
        //}

        private void AddPositionForTransitioningFromProgressToRegressLines(int fromDistance)
        {
            var cost = 1 + _progressLine[fromDistance];
            var condition = _regressLine[fromDistance] == -1 ||
                            _regressLine[fromDistance] > cost;
            if (condition)
            {
                _regressLine[fromDistance] = cost;
                _newPositions[_numNewPositions] = new Position(fromDistance, _regressLine);
                _numNewPositions += 1;
            }
        }

        private void AddPositionForTransitioningFromRegressToProgressLines(int fromDistance)
        {
            var cost = 0 + _regressLine[fromDistance];
            var condition = !ExcludedSegments[fromDistance] &&
                            _progressLine[fromDistance] == -1 ||
                            _progressLine[fromDistance] > cost;
            if (condition)
            {
                _regressLine[fromDistance] = cost;
                _newPositions[_numNewPositions] = new Position(fromDistance, _regressLine);
                _numNewPositions += 1;
            }
        }
    }

    public struct Range
    {
        public Range(int min, int max)
        {
            Min = min; Max = max;
        }

        public int Min { get; }
        public int Max { get; }

        public override string ToString() => $"{Min}..{Max}";
    }

    public struct Position
    {
        public Position(int distance, object line)
        {
            Distance = distance;
            Line = line;
        }

        public int Distance { get; }
        public object Line { get; }
    }
}
