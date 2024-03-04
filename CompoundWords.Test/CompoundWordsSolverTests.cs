using System.Runtime.ConstrainedExecution;
using System;
using Xunit.Abstractions;
using CompoundWordsSolver = CompoundWords.Program.CompoundWordsSolver;

namespace CompoundWords.Test
{
    public class CompoundWordsSolverTests
    {
        private readonly ITestOutputHelper _output;

        public CompoundWordsSolverTests(ITestOutputHelper outputHelper)
        {
            _output = outputHelper;
        }

        [Theory]
        [InlineData("sample-input", new[]
        {
            "alien",
            "newborn",
        })]
        [InlineData("UVa Online Judge", new string[]
        {
            "alien",
        })]
        [InlineData("nasher", new string[]
        {
            "gerardo",
        })]
        [InlineData("klrscpp", new string[]
        {
            "aborn",
            "alien",
            "gerardo",
            "lien",
            "newborn",
            "oo",
            "rdo",
            "zen",
            "zoo",
            "zz",
            "zzz",
        })]
        public void TestSolution(string inputFile, string[] expectedResult)
        {
            using var fileStream = File.OpenRead(Path.Combine("inputs", inputFile));
            using var streamReader = new StreamReader(fileStream);
            var lines = EnumerateLines(streamReader).ToArray();
            //Random.Shared.Shuffle(lines);
            var subject = new CompoundWordsSolver(lines);
            var result = subject.Solve().ToArray();

            foreach (var word in result)
            {
                //_output.WriteLine("\"{word}\",");
                _output.WriteLine(word);
            }

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GenerateCompoundWordsFrom_Create()
        {
            var word = "create";
            var expectedPairs = new[]
            {
                new Tuple<string, string>("c", "reate"),
                new Tuple<string, string>("cr", "eate"),
                new Tuple<string, string>("cre", "ate"),
                new Tuple<string, string>("crea", "te"),
                new Tuple<string, string>("creat", "e")
            };
            var result = CompoundWordsSolver.GenerateDecompositions(word);
            Assert.Equal(expectedPairs, result);
        }

        private IEnumerable<string> EnumerateLines(StreamReader reader)
        {
            while (reader.ReadLine()! is { } line)
            {
                yield return line;
            }
        }
    }
}