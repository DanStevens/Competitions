namespace SpellingCheck.Test
{
    public class SpellingCheckSolverTests
    {
        [Theory]
        [InlineData("01", "abdrakadabra", "abrakadabra", 3)]
        [InlineData("02", "aa", "a", 1, 2)]
        [InlineData("03", "competition", "codeforces")]
        public void TestSolution(string testNum, string first, string second, params int[] expectedPositions)
        {
            var subject = new SpellingCheckSolver(first, second);
            Assert.Equal(expectedPositions, subject.Solve());
            Assert.NotNull(subject);
        }

        [Theory]
        [InlineData("z", "")]
        [InlineData("aa", "a", "a")]
        [InlineData("bc", "c", "b")]
        [InlineData("ten", "en", "tn", "te")]
        [InlineData("five", "ive", "fve", "fie", "fiv")]
        [InlineData("seven", "even", "sven", "seen", "sevn", "seve")]
        [InlineData("abdrakadabra", "bdrakadabra", "adrakadabra", "abrakadabra", "abdakadabra", "abdrkadabra", "abdraadabra", "abdrakdabra", "abdrakaabra", "abdrakadbra", "abdrakadara", "abdrakadaba", "abdrakadabr")]
        public void GenerateOneCharDeletions(string word, params string[] expectedItems)
        {
            var result = SpellingCheckSolver.GenerateOneCharDeletions(word);
            Assert.Equal(expectedItems, result);
        }
    }
}