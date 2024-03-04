namespace Snowflakes.Test;

/// <summary>
/// Two snowflakes are alike if they are the same, if we can make them the same by moving rightward
/// through one of the snowflakes, or if we can make them the same by moving leftward through one
/// of the snowflakes.
/// </summary>
public class SnowflakeTest
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(new[] { 2, 3, 4, 5, 6, 7 })]
    [InlineData(new[] { 12, 13, 14, 15, 16, 17 })]
    public void TwoSnowflakesAreEqualIfTheArmsOfBothAreTheSameInTheSamePosition(int[] snowflake1And2Arms)
    {
        var snowflake1 = new Snowflake(snowflake1And2Arms);
        var snowflake2 = new Snowflake(snowflake1And2Arms);
        Assert.Equal(snowflake1, snowflake2);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 2, 3, 4, 5, 6, 7 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 12, 13, 14, 15, 16, 17 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 5, 6, 1, 2, 3 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 4, 5, 6, 1, 2 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 2, 1, 6, 5, 4 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 3, 2, 1, 6, 5 })]
    [InlineData(new[] { 3, 1, 2, 999, 82, 100 }, new[] { 82, 100, 3, 1, 2, 999 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 9, 15, 2, 1, 10 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 8, 4, 8, 9, 2, 8 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 82, 100, 3, 1, 2, 999 })]
    public void TwoSnowflakesAreNotEqualIfTheArmsOfBothAreTheDifferentInTheSamePositionWithoutRotation(int[] snowflake1Arms, int[] snowflake2Arms)
    {
        var snowflake1 = new Snowflake(snowflake1Arms);
        var snowflake2 = new Snowflake(snowflake2Arms);
        Assert.NotEqual(snowflake1, snowflake2);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(new[] { 2, 3, 4, 5, 6, 7 })]
    [InlineData(new[] { 12, 13, 14, 15, 16, 17 })]
    public void TwoSnowflakesAreAlikeIfTheyAreEqual(int[] snowflake1And2Arms)
    {
        var snowflake1 = new Snowflake(snowflake1And2Arms);
        var snowflake2 = new Snowflake(snowflake1And2Arms);
        Assert.True(snowflake1.IsLike(snowflake2));
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 5, 6, 1, 2, 3 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 4, 5, 6, 1, 2 })]
    public void TwoSnowflakesAreAlikeIfWeCanMakeThemEqualByMovingThe2ndSnowflakeRightwardThroughThe1st(
        int[] snowflake1Arms, int[] snowflake2Arms)
    {
        var snowflake1 = new Snowflake(snowflake1Arms);
        var snowflake2 = new Snowflake(snowflake2Arms);
        Assert.True(snowflake1.IsLike(snowflake2));
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 2, 1, 6, 5, 4 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 3, 2, 1, 6, 5 })]
    [InlineData(new[] { 3, 1, 2, 999, 82, 100 }, new[] { 82, 100, 3, 1, 2, 999 })]
    public void TwoSnowflakesAreAlikeIfWeCanMakeThemEqualByMovingThe2ndSnowflakeLeftwardThroughThe1st(
        int[] snowflake1Arms, int[] snowflake2Arms)
    {
        var snowflake1 = new Snowflake(snowflake1Arms);
        var snowflake2 = new Snowflake(snowflake2Arms);
        Assert.True(snowflake1.IsLike(snowflake2));
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 9, 15, 2, 1, 10 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 8, 4, 8, 9, 2, 8 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 82, 100, 3, 1, 2, 999 })]
    public void TwoSnowflakesAreNotAlikeIfWeCannotMakeThemEqualByMovingThe2ndSnowflakeLeftwardAndRightwardThroughThe1st(
        int[] snowflake1Arms, int[] snowflake2Arms)
    {
        var snowflake1 = new Snowflake(snowflake1Arms);
        var snowflake2 = new Snowflake(snowflake2Arms);
        Assert.False(snowflake1.IsLike(snowflake2));
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(new[] { 2, 3, 4, 5, 6, 7 })]
    [InlineData(new[] { 12, 13, 14, 15, 16, 17 })]
    public void GetHashCode_ShouldBeEqualsForTwoSnowflakesIfTheyAreTheSame(
        int[] snowflake1And2Arms)
    {
        var snowflakeHashCode1 = new Snowflake(snowflake1And2Arms).GetHashCode();
        var snowflakeHashCode2 = new Snowflake(snowflake1And2Arms).GetHashCode();
        Assert.Equal(snowflakeHashCode1, snowflakeHashCode2);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 5, 6, 1, 2, 3 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 4, 5, 6, 1, 2 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 9, 15, 2, 1, 10 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 8, 4, 8, 9, 2, 8 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 2, 1, 6, 5, 4 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 3, 2, 1, 6, 5 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 82, 100, 3, 1, 2, 999 })]
    public void GetHashCode_ShouldNotBeEqualsForTwoSnowflakesIfTheyAreTheDifferent(
        int[] snowflake1Arms, int[] snowflake2Arms)
    {
        var snowflakeHashCode1 = new Snowflake(snowflake1Arms).GetHashCode();
        var snowflakeHashCode2 = new Snowflake(snowflake2Arms).GetHashCode();
        Assert.NotEqual(snowflakeHashCode1, snowflakeHashCode2);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(new[] { 2, 3, 4, 5, 6, 7 })]
    [InlineData(new[] { 12, 13, 14, 15, 16, 17 })]
    public void Size_ShouldBeEqualsForTwoSnowflakesIfTheyAreTheSame(
        int[] snowflake1And2Arms)
    {
        var snowflake1Size = new Snowflake(snowflake1And2Arms).Size;
        var snowflake2Size = new Snowflake(snowflake1And2Arms).Size;
        Assert.Equal(snowflake1Size, snowflake2Size);
    }

    [Theory]
    //[InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 5, 6, 1, 2, 3 })]
    //[InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 4, 5, 6, 1, 2 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 9, 15, 2, 1, 10 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 8, 4, 8, 9, 2, 8 })]
    //[InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 2, 1, 6, 5, 4 })]
    //[InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 3, 2, 1, 6, 5 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 3, 9, 15, 2, 1, 10 })]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 82, 100, 3, 1, 2, 999 })]
    public void Size_ShouldNotBeEqualsForTwoSnowflakesIfTheyAreTheDifferent(
        int[] snowflake1Arms, int[] snowflake2Arms)
    {
        var snowflake1Size = new Snowflake(snowflake1Arms).Size;
        var snowflake2Size = new Snowflake(snowflake2Arms).Size;
        Assert.NotEqual(snowflake1Size, snowflake2Size);
    }
}