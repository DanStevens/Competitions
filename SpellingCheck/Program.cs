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
        var prefix = GetLongestCommonPrefix(_first, _second);
        var suffix = GetLongestCommonSuffix(_first, _second);
        var total = Math.Clamp(
            prefix.Length + 2 - (_first.Length - suffix.Length),
            0,
            int.MaxValue);

        var result = new int[total];

        for (int i = 0; i < total; i++)
            result[i] = i + _first.Length - suffix.Length;

        return result;
    }

    public IList<int> SolveOld()
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

    public static string GetLongestCommonPrefix(string first, string second)
    {
        return new string(Do().ToArray());

        IEnumerable<char> Do()
        {
            int i = 0;
            while (i < second.Length && first[i] == second[i])
                yield return first[i++];
        }
    }

    public static string GetLongestCommonSuffix(string first, string second)
    {
        return new string(Do().Reverse().ToArray());

        IEnumerable<char> Do()
        {
            int i = 1;
            while (i <= second.Length && first[^i] == second[^i])
                yield return first[^i++];
        }
    }
}