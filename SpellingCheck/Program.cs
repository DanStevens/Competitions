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

    public static string[] GenerateOneCharDeletions(ReadOnlySpan<char> word)
    {
        var result = new string[word.Length];
        var buffer = (Span<char>)stackalloc char[word.Length - 1];

        for (var splitAt = 0; splitAt < word.Length; splitAt++)
        {
            // Split the word into two at the split point, excluding the first character
            // of the right part - this results in deleting 1 character
            var lPart = word.Slice(0, splitAt);
            var rPart = word.Slice(splitAt + 1);

            // Combine the parts as efficiently as possible into a new string
            lPart.CopyTo(buffer);
            rPart.CopyTo(buffer.Slice(splitAt));

            result[splitAt] = new string(buffer);
        }

        return result; 
    }

    public static ReadOnlySpan<char> GetLongestCommonPrefix(ReadOnlySpan<char> first, ReadOnlySpan<char> second)
    {
        int i = 0;
        while (i < second.Length && first[i] == second[i])
            i++;
        return first.Slice(0, i);
    }

    public static ReadOnlySpan<char> GetLongestCommonSuffix(ReadOnlySpan<char> first, ReadOnlySpan<char> second)
    {
        int i = 1;
        while (i <= second.Length && first[^i] == second[^i])
            i++;
        return first.Slice(first.Length - (i - 1));
    }
}