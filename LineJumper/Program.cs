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
        private int[] _movementGraph;
        private int[] _currentPositions;
        private int[] _newPositions;
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
            if (_movementGraph == null)
                BuildMovementGraph(_minDistance);

            int bestResult = -1;

            for (int i = _minDistance; i < _minDistance * 2; i++)
                if (_movementGraph[i] != -1 && (bestResult == -1 || _movementGraph[i] < bestResult))
                    bestResult = _movementGraph[i];

            return bestResult;
        }

        private void Initialize()
        {
            _numCurrentPositions = 0;
            _numNewPositions = 0;
            _movementGraph = new int[_minDistance * 2];
            _currentPositions = new int[_minDistance * 2];
            _newPositions = new int[_minDistance * 2];

            for (int i = 0; i < _movementGraph.Length; i++)
                _movementGraph[i] = -1;
        }

        private void BuildMovementGraph(int targetDistance)
        {
            Initialize();

            _movementGraph[0] = 0;
            _currentPositions[0] = 0;
            _numCurrentPositions = 1;

            while (_numCurrentPositions > 0)
            {
                _numNewPositions = 0;

                for (int i = 0; i < _numCurrentPositions; i++)
                {
                    var fromDistance = _currentPositions[i];

                    AddPosition(fromDistance, fromDistance + _jumpDistance, targetDistance * 2 - 1);

                    for (int j = 0; j < fromDistance; j++)
                        AddPosition(fromDistance, j, targetDistance * 2 - 1);
                }

                _numCurrentPositions = _numNewPositions;
                Array.Copy(_newPositions, _currentPositions, _numCurrentPositions);
            }
        }

        private void AddPosition(int fromDistance, int toDistance, int maxHeight)
        {
            bool condition = toDistance <= maxHeight &&
                             !ExcludedSegments[toDistance] &&
                             _movementGraph[toDistance] == -1;
            if (condition)
            {
                _movementGraph[toDistance] = 1 + _movementGraph[fromDistance];
                _newPositions[_numNewPositions] = toDistance;
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
}
