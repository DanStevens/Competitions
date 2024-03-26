using System;
using System.Collections.Generic;
using LineJumper;
using Xunit;

namespace LineJumperTests
{
    public class LineJumpSolverTests
    {
        public static IEnumerable<object[]> SolveTestCases => new[]
        {
            new object[] { -1,  5, 2, new[] { new Range(1, 1), new Range(4, 4) } },
            new object[] {  1, 1, 1, Array.Empty<Range>() },
            new object[] {  2, 2, 1, Array.Empty<Range>() },
            new object[] {  5, 12, 5, new[] { new Range(2, 4), new Range(10, 10) } },
            new object[] {  4, 10, 4, new[] { new Range(8, 9) } },
        };

        [Theory]
        [MemberData(nameof(SolveTestCases))]
        public void Solve(int expectedResult, int minDistance, int jumpDistance, Range[] exclusionZones)
        {
            var solver = new LineJumpSolver(minDistance, jumpDistance, exclusionZones);
            Assert.Equal(expectedResult, solver.Solve());
        }

        public static IEnumerable<object[]> DecodeExclusionZonesTestCases => new[]
        {
            new object[] { 1, Array.Empty<Range>(), new[] { false, false } },
            new object[] { 1, new[] { new Range(0, 0) }, new[] { true, false } },
            new object[] { 1, new[] { new Range(1, 1) }, new[] { false, true } },
            new object[] { 2, Array.Empty<Range>(), new[] { false, false, false, false } },
        };

        [Theory]
        [MemberData(nameof(DecodeExclusionZonesTestCases))]
        public void DecodeExclusionZones(int minDistance, Range[] exclusionZones, bool[] exclusionSegments)
        {
            var solver = new LineJumpSolver(minDistance, 1, exclusionZones);
            Assert.Equal(exclusionSegments, solver.ExcludedSegments);
        }
    }
}
