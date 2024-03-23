using Xunit;

namespace KnightlyPursuit.Tests
{
    public class KnightlyPursuitSolverTests
    {
        [Theory]
        [InlineData(3, 3, 1, 1, 2, 3, GameOutcome.Stalemate, 1)]
        [InlineData(5, 3, 1, 1, 3, 1, GameOutcome.Win, 2)]
        [InlineData(7, 7, 1, 1, 3, 3, GameOutcome.Win, 1)]
        [InlineData(7, 7, 1, 1, 4, 6, GameOutcome.Win, 3)]
        [InlineData(99, 99, 33, 33, 33, 35, GameOutcome.Win, 1)]
        [InlineData(99, 99, 96, 23, 99, 1, GameOutcome.Loss, 2)]
        public void TestSolution(
            int numRows, int numColumns, int pawnRow, int pawnColumn, int knightRow, int knightColumn,
            GameOutcome expectedOutcome, int numMoves)
        {
            var expectedResult = new Result { Outcome = expectedOutcome, NumMoves = numMoves };
            var solver = new KnightlyPursuitSolver(numRows, numColumns);
            var result = solver.Solve(new Position(pawnRow, pawnColumn), new Position(knightRow, knightColumn));
            Assert.Equal(expectedResult, result);
        }
    }
}
