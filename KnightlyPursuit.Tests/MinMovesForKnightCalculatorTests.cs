using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace KnightlyPursuit.Tests
{
    public class MinMovesForKnightCalculatorTests
    {
        private readonly ITestOutputHelper _output;

        public MinMovesForKnightCalculatorTests(ITestOutputHelper output)
        {
            _output = output;
        }

        public static IEnumerable<object[]> TestData => new[]
        {
            new object[] { 1, 1, new Position(1, 1), new [,] {
                { -1, -1 },
                { -1,  0 }
            }},
            new object[] { 1, 2, new Position(1, 1), new [,] {
                { -1, -1, -1 },
                { -1,  0, -1 }
            }},
            new object[] { 2, 1, new Position(1, 1), new [,]
            {
                { -1, -1 },
                { -1,  0 },
                { -1, -1 }
            }},
            new object[] { 2, 2, new Position(1, 1), new [,]
            {
                { -1, -1, -1 },
                { -1,  0, -1 },
                { -1, -1, -1 }
            }},
            new object[] { 3, 3, new Position(1, 1), new [,]
            {
                { -1, -1, -1, -1 },
                { -1,  0,  3,  2 },
                { -1,  3, -1,  1 },
                { -1,  2,  1,  4 }
            }},
            new object[] { 3, 3, new Position(2, 3), new [,]
            {
                { -1, -1, -1, -1 },
                { -1,  1,  2,  3 },
                { -1,  4, -1,  0 },
                { -1,  1,  2,  3 }
            }},
            new object[] { 5, 3, new Position(3, 1), new [,]
            {
                { -1, -1, -1, -1 },
                { -1,  2,  1,  4 },
                { -1,  3,  2,  1 },
                { -1,  0,  3,  2 },
                { -1,  3,  2,  1 },
                { -1,  2,  1,  4 },
            }},
            new object[] { 7, 7, new Position(3, 3), new [,]
            {
                { -1, -1, -1, -1, -1, -1, -1, -1 },
                { -1,  4,  1,  2,  1,  4,  3,  2 },
                { -1,  1,  2,  3,  2,  1,  2,  3 },
                { -1,  2,  3,  0,  3,  2,  3,  2 },
                { -1,  1,  2,  3,  2,  1,  2,  3 },
                { -1,  4,  1,  2,  1,  4,  3,  2 },
                { -1,  3,  2,  3,  2,  3,  2,  3 },
                { -1,  2,  3,  2,  3,  2,  3,  4 },
            }},
            new object[] { 7, 7, new Position(4, 6), new [,]
            {
                { -1, -1, -1, -1, -1, -1, -1, -1 },
                { -1,  4,  3,  2,  3,  2,  3,  2 },
                { -1,  3,  2,  3,  4,  1,  2,  1 },
                { -1,  4,  3,  2,  1,  2,  3,  2 },
                { -1,  3,  2,  3,  2,  3,  0,  3 },
                { -1,  4,  3,  2,  1,  2,  3,  2 },
                { -1,  3,  2,  3,  4,  1,  2,  1 },
                { -1,  4,  3,  2,  3,  2,  3,  2 },
            }},
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Calculate(int numRows, int numColumns, Position knightStart, int[,] expectedResult)
        {
            _output.WriteLine("Expected:");
            _output.WriteLine(ToString(expectedResult));

            var subject = new KnightMovesFinder(numRows, numColumns);
            var result = subject.Search(knightStart);

            _output.WriteLine("Actual:");
            _output.WriteLine(ToString(result));
            
            Assert.Equal(expectedResult, result);
        }

        private string ToString(int[,] grid)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                    sb.Append(grid[i, j].ToString().PadLeft(2) + " ");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
