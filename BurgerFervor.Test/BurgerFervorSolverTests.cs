namespace BurgerFervor.Test;

public class BurgerFervorSolverTests
{
    [Theory]
    [InlineData(3, 5, 54, 18, 0)]
    [InlineData(3, 5, 55, 17, 0)]
    [InlineData(4, 9, 0, 0, 0)]
    [InlineData(4, 9, 15, 2, 2)]
    [InlineData(4, 9, 22, 3, 0)]
    [InlineData(4, 9, 54, 11, 0)]
    public void TestSolution(int m, int n, int t, int numBurgersExpected, int minsRemaining)
    {
        var expectedResult = new Result(numBurgersExpected, minsRemaining);
        var result = BurgerFervorSolver.Solve(m, n, t);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(4, 9, 0, 0)]
    [InlineData(4, 9, 15, -1)]
    [InlineData(4, 9, 20, 5)]
    [InlineData(4, 9, 22, 3)]
    [InlineData(4, 9, 36, 9)]
    public void SolveForT(int m, int n, int t, int expectedResult)
    {
        var result = BurgerFervorSolver.SolveForT(m, n, t);
        Assert.Equal(expectedResult, result);
    }
}