using Xunit;

namespace KnightlyPursuit.Tests
{
    public class PositionTests
    {
        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(0, 0, 0, 1, -1)]
        [InlineData(0, 0, 1, 0, -1)]
        [InlineData(0, 0, 1, 1, -1)]
        [InlineData(0, 1, 0, 0, 1)]
        [InlineData(0, 1, 0, 1, 0)]
        [InlineData(0, 1, 1, 0, -1)]
        [InlineData(0, 1, 1, 1, -1)]
        [InlineData(1, 0, 0, 0, 1)]
        [InlineData(1, 0, 0, 1, 1)]
        [InlineData(1, 0, 1, 0, 0)]
        [InlineData(1, 0, 1, 1, -1)]
        [InlineData(1, 1, 0, 0, 1)]
        [InlineData(1, 1, 0, 1, 1)]
        [InlineData(1, 1, 1, 0, 1)]
        [InlineData(1, 1, 1, 1, 0)]
        [InlineData(0, 0, 0, 2, -1)]
        [InlineData(0, 0, 2, 0, -1)]
        [InlineData(0, 0, 2, 2, -1)]
        [InlineData(0, 2, 0, 0, 1)]
        [InlineData(0, 2, 0, 2, 0)]
        [InlineData(0, 2, 2, 0, -1)]
        [InlineData(0, 2, 2, 2, -1)]
        [InlineData(2, 0, 0, 0, 1)]
        [InlineData(2, 0, 0, 2, 1)]
        [InlineData(2, 0, 2, 0, 0)]
        [InlineData(2, 0, 2, 2, -1)]
        [InlineData(2, 2, 0, 0, 1)]
        [InlineData(2, 2, 0, 2, 1)]
        [InlineData(2, 2, 2, 0, 1)]
        [InlineData(2, 2, 2, 2, 0)]
        public void CompareTo(int p1Row, int p1Col, int p2Row, int p2Col, int result)
        {
            var p1 = new Position(p1Row, p1Col);
            var p2 = new Position(p2Row, p2Col);
            Assert.Equal(result, p1.CompareTo(p2));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, true)]
        [InlineData(0, 0, 0, 1, false)]
        [InlineData(0, 0, 1, 0, false)]
        [InlineData(0, 0, 1, 1, false)]
        [InlineData(0, 1, 0, 0, false)]
        [InlineData(0, 1, 0, 1, true)]
        [InlineData(0, 1, 1, 0, false)]
        [InlineData(0, 1, 1, 1, false)]
        [InlineData(1, 0, 0, 0, false)]
        [InlineData(1, 0, 0, 1, false)]
        [InlineData(1, 0, 1, 0, true)]
        [InlineData(1, 0, 1, 1, false)]
        [InlineData(1, 1, 0, 0, false)]
        [InlineData(1, 1, 0, 1, false)]
        [InlineData(1, 1, 1, 0, false)]
        [InlineData(1, 1, 1, 1, true)]
        [InlineData(0, 0, 0, 2, false)]
        [InlineData(0, 0, 2, 0, false)]
        [InlineData(0, 0, 2, 2, false)]
        [InlineData(0, 2, 0, 0, false)]
        [InlineData(0, 2, 0, 2, true)]
        [InlineData(0, 2, 2, 0, false)]
        [InlineData(0, 2, 2, 2, false)]
        [InlineData(2, 0, 0, 0, false)]
        [InlineData(2, 0, 0, 2, false)]
        [InlineData(2, 0, 2, 0, true)]
        [InlineData(2, 0, 2, 2, false)]
        [InlineData(2, 2, 0, 0, false)]
        [InlineData(2, 2, 0, 2, false)]
        [InlineData(2, 2, 2, 0, false)]
        [InlineData(2, 2, 2, 2, true)]
        public void Equals(int p1Row, int p1Col, int p2Row, int p2Col, bool result)
        {
            var p1 = new Position(p1Row, p1Col);
            var p2 = new Position(p2Row, p2Col);
            Assert.Equal(result, p1.Equals(p2));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0, 0, 0, 1, 0, 1)]
        [InlineData(0, 0, 1, 0, 1, 0)]
        [InlineData(0, 0, 1, 2, 1, 2)]
        [InlineData(0, 0, 2, 1, 2, 1)]
        [InlineData(2, 1, -2, -1, 0, 0)]
        [InlineData(5, 2, 2, 4, 7, 6)]
        [InlineData(5, 2, -2, -4, 3, -2)]
        public void Offset(int pInRow, int pInCol, int rowOffset, int colOffset, int pOutRow, int pOutCol)
        {
            var pIn = new Position(pInRow, pInCol);
            var pOut = pIn.Offset(rowOffset, colOffset);
            Assert.Equal(new Position(pOutRow, pOutCol), pOut);
        }
    }
}
