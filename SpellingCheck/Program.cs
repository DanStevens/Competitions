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
        var prefixLength = GetLongestCommonPrefixLength(_first, _second);
        var suffixLength = GetLongestCommonSuffix(_first, _second);
        var total = Math.Clamp(
            prefixLength + 2 - (_first.Length - suffixLength),
            0,
            int.MaxValue);

        var result = new int[total];

        for (int i = 0; i < total; i++)
            result[i] = i + _first.Length - suffixLength;

        return result;
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

    public static int GetLongestCommonPrefixLength(ReadOnlySpan<char> first, ReadOnlySpan<char> second)
    {
        int i = 0;
        while (i < second.Length && first[i] == second[i])
            i++;
        return i;
    }

    public static int GetLongestCommonSuffix(ReadOnlySpan<char> first, ReadOnlySpan<char> second)
    {
        int i = 1;
        while (i <= second.Length && first[^i] == second[^i])
            i++;
        return i - 1;
    }
}
//