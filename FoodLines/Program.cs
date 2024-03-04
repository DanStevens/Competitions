using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var firstLineArgs = Console.ReadLine().Split(' ');
        var numLines = int.Parse(firstLineArgs[0]);
        var numArrivals = int.Parse(firstLineArgs[1]);
        var lineLengths = Console.ReadLine().Split(' ')
                                            .Select(s => int.Parse(s)).ToArray();

        var solver = new FoodLinesSolver(numLines, numArrivals, lineLengths);
        var result = solver.Solve();

        foreach (int item in result)
        {
            Console.WriteLine(item);
        }
    }
}

public class FoodLinesSolver
{
    private class Line
    {
        public Line(int initialLength)
        {
            this.Length = initialLength;
        }

        public int Length { get; private set; }

        public void Increment() => Length += 1;
    }

    private readonly int numArrivals;
    private readonly IList<Line> lines;

    public FoodLinesSolver(int numLines, int numArrivals, int[] lineLengths)
    {
        this.numArrivals = numArrivals;
        this.lines = lineLengths.Take(numLines)
                                .Select(l => new Line(l)).ToList();
    }

    public IList<int> Solve()
    {
        var result = new List<int>();
        
        for (int i = 0; i < numArrivals; i++)
        {
            var shortestLine = GetShortestLine();
            result.Add(shortestLine.Length);
            shortestLine.Increment();
        }

        return result;
    }

    private Line GetShortestLine()
    {
        var line = lines.MinBy(l => l.Length);
        return line;
    }
}

public static class EnumerableExtensions
{
    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        // Find the minimum key value using the provided key selector
        var minKeyValue = source.Min(keySelector);

        // Return the first element with the minimum key value
        return source.FirstOrDefault(item => keySelector(item).Equals(minKeyValue));
    }
}