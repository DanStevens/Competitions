using System;
using System.Linq;

namespace GeeseVsHawks
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            _ = Console.ReadLine();
            var geeseResults = GeeseVsHawksSolver.ParseGames(Console.ReadLine(), Console.ReadLine());
            var hawksResults = GeeseVsHawksSolver.ParseGames(Console.ReadLine(), Console.ReadLine());
            var solver = new GeeseVsHawksSolver(geeseResults, hawksResults);
            var result = solver.Solve();
            Console.WriteLine(result);
        }
    }

    public class GeeseVsHawksSolver
    {
        private const int Size = 1000;

        public static GameResult[] ParseGames(string outcomes, string goals)
        {
            var results  = new GameResult[outcomes.Length];
            var goalsParsed = goals.Length > 0
                ? goals.Split(' ').Select(int.Parse).ToArray() : Array.Empty<int>();

            for (int i = 0; i < outcomes.Length; i++)
            {
                results[i] = new GameResult(ParseOutcome(outcomes[i]), goalsParsed[i]); 
            }
            
            return results;
        }

        private static Outcome ParseOutcome(char c)
        {
            switch (c)
            {
                case 'L':
                case 'l':
                    return Outcome.Lose;
                case 'W': 
                case 'w':
                    return Outcome.Win;
                default:
                    throw new ArgumentOutOfRangeException(nameof(c),
                        "Argument can only be 'W' or 'L'");
            }
        }

        private readonly int _numTests;
        private readonly GameResult[] _geeseResults;
        private readonly GameResult[] _hawksResults;

        public GeeseVsHawksSolver(string geeseWinLose, string geeseGoals, string hawksWinLose, string hawksGoals)
            : this(ParseGames(geeseWinLose, geeseGoals), ParseGames(hawksWinLose, hawksGoals))
        {}

        public GeeseVsHawksSolver(GameResult[] geeseResults, GameResult[] hawksResults)
        {
            if (geeseResults?.Length != hawksResults?.Length)
                throw new ArgumentException("Arguments must have same number of elements");

            _numTests = geeseResults.Length;
            _geeseResults = geeseResults.Prepend(GameResult.Undefined).ToArray();
            _hawksResults = hawksResults.Prepend(GameResult.Undefined).ToArray();
        }

        public int Solve()
        {
            var previous = new int[_numTests + 1];
            var current = new int[_numTests + 1];

            for (int i = 1; i <= _numTests; i++)
            {
                for (int j = 1; j <= _numTests; j++)
                {
                    var geeseOutcome = _geeseResults[i].Outcome;
                    var hawksOutcome = _hawksResults[j].Outcome;
                    var geeseGoals = _geeseResults[i].Goals;
                    var hawksGoals = _hawksResults[j].Goals;

                    var condition1 = geeseOutcome == Outcome.Win && hawksOutcome == Outcome.Lose &&
                                     geeseGoals > hawksGoals;
                    var condition2 = geeseOutcome == Outcome.Lose && hawksOutcome == Outcome.Win &&
                                     geeseGoals < hawksGoals;

                    var options = new[]
                    {
                        condition1 || condition2
                            ? previous[j - 1] + _geeseResults[i].Goals + _hawksResults[j].Goals
                            : 0,
                        previous[j - 1],
                        previous[j],
                        current[j - 1],
                    };

                    current[j] = options.Max();
                }

                current.CopyTo(previous, 0);
            }

            return current[_numTests];
        }
    }

    public enum Outcome
    {
        Undefined = -1,
        Lose = 0,
        Win = 1,
    };

    public struct GameResult
    {
        public static GameResult Undefined = new GameResult();
        
        public GameResult(Outcome outcome, int goals)
        {
            Outcome = outcome;
            Goals = goals;
        }

        public Outcome Outcome { get; set; }
        public int Goals { get; set; }

        public override string ToString()
        {
            return $"{Outcome} {Goals}";
        }
    }
}
