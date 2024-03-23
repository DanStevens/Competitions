using System;

namespace KnightlyPursuit
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var numTestCases = int.Parse(Console.ReadLine());

            for (int i = 0; i < numTestCases; i++)
            {
                var numRows = int.Parse(Console.ReadLine());
                var numColumns = int.Parse(Console.ReadLine());
                var pawnStart = new Position(
                    int.Parse(Console.ReadLine()),
                    int.Parse(Console.ReadLine()));
                var knightStart = new Position(
                    int.Parse(Console.ReadLine()),
                    int.Parse(Console.ReadLine()));

                var solver = new KnightlyPursuitSolver(numRows, numColumns);
                var result = solver.Solve(pawnStart, knightStart);
                Console.WriteLine(result);
            }
        }
    }

    public class KnightlyPursuitSolver
    {
        private readonly int _numRows;
        private readonly int _numColumns;
        private readonly int[,] _minMoves;
        private readonly Position[] _currentPositions;
        private readonly Position[] _newPositions;
        private int _numCurrentPositions;
        private int _numNewPositions;

        public KnightlyPursuitSolver(int numRows, int numColumns)
        {
            _numColumns = numColumns;
            _numRows = numRows;
            _minMoves = new int[_numRows + 1, _numColumns + 1];
            _currentPositions = new Position[_numRows * _numColumns];
            _newPositions = new Position[_numRows * _numColumns];
        }

        private void ResetMinMoves()
        {
            for (int i = 0; i <= _numRows; i++)
            for (int j = 0; j <= _numColumns; j++)
                _minMoves[i, j] = -1;
        }

        public Result Solve(Position pawnStart, Position knightStart)
        {
            int numMovesPawn = 0;
            var pawnCurrent = pawnStart;

            // Determine if knight can win
            while (pawnCurrent.Row < _numRows)
            {
                int numMovesKnight = FindDistance(knightStart, pawnCurrent);

                if (CompareMoves(numMovesKnight, numMovesPawn))
                    return new Result { Outcome = GameOutcome.Win, NumMoves = numMovesPawn };

                pawnCurrent = pawnCurrent.Offset(1, 0);
                numMovesPawn += 1;
            }

            pawnCurrent = pawnStart;
            numMovesPawn = 0;

            // Determine if Knight can stalemate
            while (pawnCurrent.Row < _numRows)
            {
                var numMovesKnight = FindDistance(knightStart, pawnCurrent.Offset(1, 0));

                if (CompareMoves(numMovesKnight, numMovesPawn))
                    return new Result { Outcome = GameOutcome.Stalemate, NumMoves = numMovesPawn };

                pawnCurrent = pawnCurrent.Offset(1, 0);
                numMovesPawn += 1;
            }

            return new Result { Outcome = GameOutcome.Loss, NumMoves = _numRows - pawnStart.Row - 1 };
        }

        private static bool CompareMoves(int numMovesKnight, int numMovesPawn)
        {
            return numMovesKnight >= 0 && numMovesPawn >= numMovesKnight &&
                   (numMovesPawn - numMovesKnight) % 2 == 0;
        }

        private int FindDistance(Position knightPos, Position to)
        {
            ResetMinMoves();

            _minMoves[knightPos.Row, knightPos.Column] = 0;

            _currentPositions[0] = knightPos;
            _numCurrentPositions = 1;

            while (_numCurrentPositions > 0)
            {
                _numNewPositions = 0;

                for (int i = 0; i < _numCurrentPositions; i++)
                {
                    var position = _currentPositions[i];
                    
                    if (position == to)
                        return _minMoves[to.Row, to.Column];

                    AddPosition(position, position.Offset(1, 2));
                    AddPosition(position, position.Offset(1, -2));
                    AddPosition(position, position.Offset(-1, 2));
                    AddPosition(position, position.Offset(-1, -2));
                    AddPosition(position, position.Offset(2, 1));
                    AddPosition(position, position.Offset(2, -1));
                    AddPosition(position, position.Offset(-2, 1));
                    AddPosition(position, position.Offset(-2, -1));
                }

                _numCurrentPositions = _numNewPositions;
                Array.Copy(_newPositions, _currentPositions, _numCurrentPositions);
            }

            return -1;
        }

        private void AddPosition(Position from, Position to)
        {
            var condition = to.Row >= 1 && to.Column >= 1 &&
                            to.Row <= _numRows && to.Column <= _numColumns &&
                            _minMoves[to.Row, to.Column] == -1;
            if (condition)
            {
                _minMoves[to.Row, to.Column] = 1 + _minMoves[from.Row, from.Column];
                _newPositions[_numNewPositions] = to;
                _numNewPositions += 1;
            }
        }
    }

    public enum GameOutcome
    {
        Undefined = -1,
        Loss = 0,
        Win = 1,
        Stalemate = 2,
    }

    public struct Result
    {
        public GameOutcome Outcome { get; set; }
        public int NumMoves { get; set; }

        public override string ToString()
        {
            return $"{Outcome} in {NumMoves} knight move(s).";
        }
    }

    public struct Position : IComparable<Position>, IEquatable<Position>
    {
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }

        public override string ToString() => $"{Row},{Column}";

        public int CompareTo(Position other)
        {
            var rowComparison = Row.CompareTo(other.Row);
            if (rowComparison != 0) return rowComparison;
            return Column.CompareTo(other.Column);
        }

        public static bool operator <(Position left, Position right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Position left, Position right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Position left, Position right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Position left, Position right)
        {
            return left.CompareTo(right) >= 0;
        }

        public bool Equals(Position other)
        {
            return CompareTo(other) == 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Position other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Column;
            }
        }

        public Position Offset(int rowOffset, int colOffset)
        {
            return new Position(Row + rowOffset, Column + colOffset);
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !left.Equals(right);
        }
    }
}
