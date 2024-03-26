using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RopeClimb;
using Xunit;

namespace RopeClimbTests
{
    public class LineJumpSolverTests
    {
        public static IEnumerable<object[]> TestCases => new[]
        {
            new object[] { -1, 0, 0, Array.Empty<Range>() },
            new object[] {  5, 12, 5, new[] { new Range(2, 4), new Range(10, 10) } },
            new object[] { -1,  5, 2, new[] { new Range(1, 1), new Range(4, 4) } },
            new object[] {  4, 10, 4, new[] { new Range(8, 9) } },
        };

        [Theory]
        [MemberData(nameof(TestCases))]
        public void CreateSolver(int expectedResult, int minDistance, int jumpDistance, Range[] exclusionZones)
        {
            var solver = new LineJumpSolver(minDistance, jumpDistance, exclusionZones);
            Assert.Equal(expectedResult, solver.Solve());
        }
    }
}
