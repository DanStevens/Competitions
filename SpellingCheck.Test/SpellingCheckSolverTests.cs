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

        [Theory]
        [InlineData("", "", "")]
        [InlineData("a", "", "")]
        [InlineData("a", "a", "a")]
        [InlineData("ab", "a", "a")]
        [InlineData("abcde", "abcd", "abcd")]
        [InlineData("abcde", "abce","abc")]
        [InlineData("abcde", "abde", "ab")]
        [InlineData("abcde", "acde", "a")]
        [InlineData("abcde", "bcde", "")]
        [InlineData("abdrakadabra", "abrakadabra", "ab")]
        [InlineData("competition", "codeforces", "co")]
        [InlineData("foo", "bar", "")]
        public void GetLongestCommonPrefix(string first, string second, string expectedResult)
        {
            var result = SpellingCheckSolver.GetLongestCommonPrefix(first, second);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("a", "", "")]
        [InlineData("a", "a", "a")]
        [InlineData("ab", "b", "b")]
        [InlineData("abcde", "abcd", "")]
        [InlineData("abcde", "abce", "e")]
        [InlineData("abcde", "abde", "de")]
        [InlineData("abcde", "acde", "cde")]
        [InlineData("abcde", "bcde", "bcde")]
        [InlineData("abdrakadabra", "abrakadabra", "rakadabra")]
        [InlineData("competition", "codeforces", "")]
        [InlineData("foo", "bar", "")]
        public static void GetLongestCommonSuffix(string first, string second, string expectedResult)
        {
            var result = SpellingCheckSolver.GetLongestCommonSuffix(first, second);
            Assert.Equal(expectedResult, result);
        }
    }
}