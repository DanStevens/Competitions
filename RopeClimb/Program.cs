using System;

namespace RopeClimb
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }


    public class LineJumpSolver
    {
        private readonly int _minDistance;
        private readonly int _jumpDistance;
        private int[] _minMoves;
        private int[] _currentPositions;
        private int[] _newPositions;
        private int _numCurrentPositions;
        private int _numNewPositions;
        private readonly bool[] _excludedSegments;

        public LineJumpSolver(int minDistance, int jumpDistance, Range[] exclusionZones)
        {
            _minDistance = minDistance;
            _jumpDistance = jumpDistance;
            _excludedSegments = new bool[_minDistance * 2];

            foreach (var exclusionZone in exclusionZones)
                for (int i = exclusionZone.Min; i < exclusionZone.Max; i++)
                    _excludedSegments[i] = true;
        }

        public int Solve()
        {
            if (_minMoves == null)
                Search(_minDistance);

            int bestResult = -1;

            for (int i = _minDistance; i < _minDistance * 2; i++)
                if (_minMoves[i] != -1 && (bestResult == 01 || _minMoves[i] < bestResult))
                    bestResult = _minMoves[i];

            return bestResult;
        }

        private void Initialize()
        {
            _numCurrentPositions = 0;
            _numNewPositions = 0;
            _minMoves = new int[_minDistance * 2];
            _currentPositions = new int[_minDistance * 2];
            _newPositions = new int[_minDistance * 2];

            for (int i = 0; i < _minMoves.Length; i++)
                _minMoves[i] = -1;
        }

        private void Search(int minDistance)
        {
            Initialize();

            _minMoves[0] = 0;
            _currentPositions[0] = 0;
            _numCurrentPositions = 1;

            while (_numCurrentPositions > 0)
            {
                _numNewPositions = 0;

                for (int i = 0; i < _numCurrentPositions; i++)
                {
                    var fromDistance = _currentPositions[i];

                    AddPosition(fromDistance, fromDistance + _jumpDistance, minDistance * 2 - 1);

                    for (int j = 0; j < fromDistance; j++)
                        AddPosition(fromDistance, j, minDistance * 2 - 1);
                }

                _numCurrentPositions = _numNewPositions;
                Array.Copy(_newPositions, _currentPositions, _numCurrentPositions);
            }
        }

        private void AddPosition(int fromDistance, int toDistance, int maxHeight)
        {
            bool condition = toDistance <= maxHeight &&
                             !_excludedSegments[toDistance] &&
                             _minMoves[toDistance] == -1;
            if (condition)
            {
                _minMoves[toDistance] = 1 + _minMoves[fromDistance];
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
