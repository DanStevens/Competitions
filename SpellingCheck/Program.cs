var first = Console.ReadLine()!;
var second = Console.ReadLine()!;
var solver = new SpellingCheckSolver(first, second);
var positions = solver.Solve();
Console.WriteLine(positions.Count);
if (positions.Count > 0)
    Console.WriteLine(string.Join(" ", positions));

public class SpellingCheckSolver
{
    private readonly string _first;
    private readonly string _second;

    public SpellingCheckSolver(string first, string second)
    {
        _first = first;
        _second = second;
    }

    public IList<int> Solve()
    {
        var positions = new List<int>();
        var variations = GenerateOneCharDeletions(_first);

        for (int i = 0; i < variations.Length; i++)
        {
            if (variations[i] == _second)
                positions.Add(i + 1);
        }

        return positions;
    }

    public static string[] GenerateOneCharDeletions(string word)
    {
        var result = new string[word.Length];

        for (var splitAt = 0; splitAt < word.Length; splitAt++)
        {
            var lPart = word.Substring(0, splitAt);
            var rPart = word.Substring(splitAt + 1);
            result[splitAt] = lPart + rPart;
        }

        return result;
    }
}