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
        private readonly KnightMovesFinder _knightMovesFinder;

        public KnightlyPursuitSolver(int boardRows, int boardColumns)
        {
            _knightMovesFinder =
                new KnightMovesFinder(boardRows, boardColumns);
        }

        public Result Solve(Position pawnStart, Position knightStart)
        {
            var boardRows = _knightMovesFinder.BoardRows;
            int numMovesPawn = 0;
            var pawnCurrent = pawnStart;

            var minMovesForKnight = _knightMovesFinder.Search(knightStart);

            // Determine if knight can win
            while (pawnCurrent.Row < boardRows)
            {
                int numMovesKnight = minMovesForKnight[pawnCurrent.Row, pawnCurrent.Column];

                if (CompareMoves(numMovesKnight, numMovesPawn))
                    return new Result { Outcome = GameOutcome.Win, NumMoves = numMovesPawn };

                pawnCurrent = pawnCurrent.Offset(1, 0);
                numMovesPawn += 1;
            }

            pawnCurrent = pawnStart;
            numMovesPawn = 0;

            // Determine if Knight can stalemate
            while (pawnCurrent.Row < boardRows)
            {
                int numMovesKnight = minMovesForKnight[pawnCurrent.Row + 1, pawnCurrent.Column];

                if (CompareMoves(numMovesKnight, numMovesPawn))
                    return new Result { Outcome = GameOutcome.Stalemate, NumMoves = numMovesPawn };

                pawnCurrent = pawnCurrent.Offset(1, 0);
                numMovesPawn += 1;
            }

            return new Result { Outcome = GameOutcome.Loss, NumMoves = boardRows - pawnStart.Row - 1 };
        }

        private static bool CompareMoves(int numMovesKnight, int numMovesPawn)
        {
            return numMovesKnight >= 0 && numMovesPawn >= numMovesKnight &&
                   (numMovesPawn - numMovesKnight) % 2 == 0;
        }
    }

    public class KnightMovesFinder
    {

        private int[,] _minMoves;
        private Position[] _currentPositions;
        private Position[] _newPositions;
        private int _numCurrentPositions;
        private int _numNewPositions;

        public KnightMovesFinder(int boardRows, int boardColumns)
        {
            BoardRows = boardRows;
            BoardColumns = boardColumns;
        }

        public int BoardRows { get; }
        public int BoardColumns { get; }

        private void Initialize()
        {
            _minMoves = new int[BoardRows + 1, BoardColumns + 1];
            _currentPositions = new Position[BoardRows * BoardColumns];
            _newPositions = new Position[BoardRows * BoardColumns];
            _numCurrentPositions = 0;
            _numNewPositions = 0;

            for (int i = 0; i <= BoardRows; i++)
            for (int j = 0; j <= BoardColumns; j++)
                _minMoves[i, j] = -1;
        }

        public int[,] Search(Position knightPos)
        {
            Initialize();

            _minMoves[knightPos.Row, knightPos.Column] = 0;
            _currentPositions[0] = knightPos;
            _numCurrentPositions = 1;

            while (_numCurrentPositions > 0)
            {
                _numNewPositions = 0;

                for (int i = 0; i < _numCurrentPositions; i++)
                {
                    var position = _currentPositions[i];
                    
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

            return _minMoves;
        }

        private void AddPosition(Position from, Position to)
        {
            var condition = to.Row >= 1 && to.Column >= 1 &&
                            to.Row <= BoardRows && to.Column <= BoardColumns &&
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
