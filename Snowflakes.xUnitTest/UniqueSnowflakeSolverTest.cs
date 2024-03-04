namespace Snowflakes.Test
{
    public class UniqueSnowflakeSolverTest
    {
        [Fact]
        public void GivenTwoSnowflakesThatAreEqual_ShouldReturnTwinsFoundResult()
        {
            var snowflakes = new[]
            {
                new Snowflake(1, 2, 3, 4, 5, 6),
                new Snowflake(1, 2, 3, 4, 5, 6),
            };
            var subject = new UniqueSnowflakeFinder(snowflakes, snowflakes.Length);
            Assert.Equal(UniqueSnowflakeFinder.Result.TwinsFound, subject.Search());
        }

        [Fact]
        public void GivenTwoSnowflakesThatAreAlike_WhenCheckingMovingRightwards_ShouldReturnTwinsFoundResult()
        {
            var snowflakes = new[]
            {
                new Snowflake(1, 2, 3, 4, 5, 6),
                new Snowflake(4, 5, 6, 1, 2, 3),
            };
            var subject = new UniqueSnowflakeFinder(snowflakes, snowflakes.Length);
            Assert.Equal(UniqueSnowflakeFinder.Result.TwinsFound, subject.Search());
        }

        [Fact]
        public void GivenTwoSnowflakesThatAreAlike_WhenCheckingLeftwards_ShouldReturnTwinsFoundResult()
        {
            var snowflakes = new[]
            {
                new Snowflake(1, 2, 3, 4, 5, 6),
                new Snowflake(3, 2, 1, 6, 5, 4),
            };
            var subject = new UniqueSnowflakeFinder(snowflakes, snowflakes.Length);
            Assert.Equal(UniqueSnowflakeFinder.Result.TwinsFound, subject.Search());
        }

        [Fact]
        public void GivenTwoSnowflakesThatAreNotAlike_ShouldReturnNoTwinsFoundResult()
        {
            var snowflakes = new[]
            {
                new Snowflake(1, 2, 3, 4, 5, 6),
                new Snowflake(3, 9, 15, 2, 1, 10),
            };
            var subject = new UniqueSnowflakeFinder(snowflakes, snowflakes.Length);
            Assert.Equal(UniqueSnowflakeFinder.Result.NoTwinsFound, subject.Search());
        }

        [Fact]
        public void GivenAShowerOfSnowflakes_ContainingNoAlikeSnowflakes_ShouldReturnNoTwinsFoundResult()
        {
            var snowflakes = new[]
            {
                new Snowflake(12, 13, 14, 15, 16, 17),
                new Snowflake(1, 2, 3, 4, 5, 6),
                new Snowflake(3, 1, 2, 999, 82, 100),
                new Snowflake(3, 9, 15, 2, 1, 10),
                new Snowflake(8, 4, 8, 9, 2, 8),
            };
            var subject = new UniqueSnowflakeFinder(snowflakes, snowflakes.Length);
            Assert.Equal(UniqueSnowflakeFinder.Result.NoTwinsFound, subject.Search());
        }

        [Fact]
        public void GivenAShowerOfSnowflakes_ContainingTwoAlikeSnowflakes_ShouldReturnTwinsFoundResult()
        {
            var snowflakes = new[]
            {
                new Snowflake(12, 13, 14, 15, 16, 17),
                new Snowflake(1, 2, 3, 4, 5, 6),
                new Snowflake(3, 1, 2, 999, 82, 100),
                new Snowflake(3, 9, 15, 2, 1, 10),
                new Snowflake(4, 5, 6, 1, 2, 3),
                new Snowflake(8, 4, 8, 9, 2, 8),
            };
            var subject = new UniqueSnowflakeFinder(snowflakes, snowflakes.Length);
            Assert.Equal(UniqueSnowflakeFinder.Result.TwinsFound, subject.Search());
        }
    }
}
