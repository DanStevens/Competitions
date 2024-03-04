using System;
using System.Collections.Generic;
using System.IO;

namespace CompoundWords
{

    public class Program
    {
        static void Main(string[] args)
        {
            var solver = new CompoundWordsSolver(EnumerateLines(Console.In));
            foreach (var word in solver.Solve())
                Console.WriteLine(word);
        }

        private static IEnumerable<string> EnumerateLines(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        public class CompoundWordsSolver
        {
            private readonly ISet<string> _wordSet;
            private readonly List<string> _wordList;
            private readonly ISet<string> _compoundWords;

            public CompoundWordsSolver(IEnumerable<string> words)
            {
                _wordList = new List<string>();
                _wordSet = new HashSet<string>();
                _compoundWords = new HashSet<string>();
                
                foreach (var word in words)
                {
                    _wordList.Add(word);
                    _wordSet.Add(word);
                }
            }

            public IEnumerable<string> Solve()
            {
                foreach (string word in _wordList)
                foreach (var tuple in GenerateDecompositions(word))
                    if (IsUniqueCompoundWord(tuple, word))
                    {
                        _compoundWords.Add(word);
                        yield return word;
                    }
            }

            private bool IsUniqueCompoundWord(Tuple<string, string> tuple, string word)
            {
                return _wordSet.Contains(tuple.Item1) && _wordSet.Contains(tuple.Item2) &&
                       !_compoundWords.Contains(word);
            }

            public static IEnumerable<Tuple<string, string>> GenerateDecompositions(string word)
            {
                for (var splitAt = 1; splitAt < word.Length; splitAt++)
                {
                    yield return new Tuple<string, string>(
                        word.Substring(0, splitAt),
                        word.Substring(splitAt));
                }
            }
        }
    }
}