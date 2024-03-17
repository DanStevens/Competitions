using System;
using System.Linq;
using Xunit;

namespace GeeseVsHawks.Test
{
    public class GeeseVsHawksSolverTests
    {
        #region ParseResults method tests

        [Fact]
        public void ParseResults_GivenZeroGames_ReturnsEmptyArray()
        {
            Assert.Empty(GeeseVsHawksSolver.ParseGames("", ""));
        }

        [Fact]
        public void ParseResults_GivenOneGame_L_ReturnsOneResult()
        {
            Assert.Equal(
                new[] { new GameResult(Outcome.Lose, 0) },
                GeeseVsHawksSolver.ParseGames("L", "0"));
        }

        [Fact]
        public void ParseResults_GivenOneGame_W_ReturnsOneResult()
        {
            Assert.Equal(
                new[] { new GameResult(Outcome.Win, 1) },
                GeeseVsHawksSolver.ParseGames("W", "1"));
        }

        [Fact]
        public void ParseResults_GivenTwoGames_LL_ReturnsTwoResults()
        {
            Assert.Equal(
                new[] { new GameResult(Outcome.Lose, 0), new GameResult(Outcome.Lose, 1) },
                GeeseVsHawksSolver.ParseGames("LL", "0 1"));
        }

        [Fact]
        public void ParseResults_GivenTwoGames_WL_ReturnsTwoResults()
        {
            Assert.Equal(
                new[] { new GameResult(Outcome.Win, 5), new GameResult(Outcome.Lose, 1) },
                GeeseVsHawksSolver.ParseGames("WL", "5 1"));
        }

        [Fact]
        public void ParseResults_GivenFourGames_WLLW_ReturnsFourResults()
        {
            Assert.Equal(
                new[]
                {
                    new GameResult(Outcome.Win, 1),
                    new GameResult(Outcome.Lose, 2),
                    new GameResult(Outcome.Lose, 3),
                    new GameResult(Outcome.Win, 4),
                },
                GeeseVsHawksSolver.ParseGames("WLLW", "1 2 3 4"));
        }

        [Fact]
        public void ParseResults_GivenFourGames_LWWL_ReturnsFourResults()
        {
            Assert.Equal(
                new[]
                {
                    new GameResult(Outcome.Lose, 6),
                    new GameResult(Outcome.Win, 5),
                    new GameResult(Outcome.Win, 3),
                    new GameResult(Outcome.Lose, 2),
                },
                GeeseVsHawksSolver.ParseGames("LWWL", "6 5 3 2"));
        }

        #endregion

        [Theory]
        [InlineData("W", "2", "W", "3", 0)]
        [InlineData("WLLW", "1 2 3 4", "LWWL", "6 5 3 2", 14)]
        [InlineData("WWW", "2 5 1", "WWW", "7 8 5", 0)]
        [InlineData("WWW", "2 5 1", "LLL", "7 8 5", 0)]
        [InlineData("WWW", "2 5 1", "LLL", "7 8 4", 9)]
        [InlineData("WW", "6 2", "LL", "8 1", 7)]
        [InlineData("WLWW", "3 4 1 8", "WLLL", "5 1 2 3", 20)]
        public void TestSolution(
            string geeseWinLose,
            string geeseGoals,
            string hawksWinLose,
            string hawksGoals,
            int expectedResult)
        {
            var subject = new GeeseVsHawksSolver(geeseWinLose, geeseGoals, hawksWinLose, hawksGoals);
            var result = subject.Solve();
            Assert.Equal(expectedResult, result);
        }
    }
}
