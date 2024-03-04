using System;
using System.Collections.Generic;
using System.Linq;

internal static class Program
{
    private static void Main(string[] args)
    {
        var numSnowflakesArg = int.Parse(Console.ReadLine());
        var snowflakeProvider = Enumerable.Range(0, numSnowflakesArg).Select(
            _ => new Snowflake(Console.ReadLine().Split(' ').Select(int.Parse).ToArray()));

        var solver = new UniqueSnowflakeFinder(snowflakeProvider, numSnowflakesArg);
        var result = solver.Search() == UniqueSnowflakeFinder.Result.TwinsFound
            ? "Twin snowflakes found."
            : "No two snowflakes are alike.";
        Console.WriteLine(result);
    }
}

public class UniqueSnowflakeFinder
{
    private readonly IEnumerable<Snowflake> _snowflakeProvider;
    private readonly int _capacity;

    public enum Result { NoTwinsFound, TwinsFound }

    public UniqueSnowflakeFinder(IEnumerable<Snowflake> snowflakeProvider, int capacity)
    {
        _snowflakeProvider = snowflakeProvider ?? throw new ArgumentNullException(nameof(snowflakeProvider));
        _capacity = capacity;
    }

    public Result Search()
    {
        // Create a dictionary keyed by a Snowflake's size and a value that is a collection
        // of snowflakes, as there may be multiple Snowflakes of the same size. Let's assume
        // that the majority of snowflakes have a unique size, so only store the initial
        // Snowflake of a given size separately, so that the list containing subsequent
        // Snowflakes of the same size is only created as and when needed.
        var dictionary = new Dictionary<int, (Snowflake initial, ICollection<Snowflake> rest)>(_capacity);

        foreach (var snowflake in _snowflakeProvider)
        {
            // See if there's a Snowflake of the same size in the dictionary.
            // If there is, check if it's like any of the other Snowflakes with the
            // same size; if it is we've found a twin! If not, append it to the
            // list of Snowflakes of that size
            if (dictionary.TryGetValue(snowflake.Size, out var sameSizedSnowflakes))
                if (AreAlike(snowflake, sameSizedSnowflakes))
                    return Result.TwinsFound;
                else
                {
                    if (sameSizedSnowflakes.rest == null)
                        sameSizedSnowflakes.rest = new List<Snowflake>();
                    sameSizedSnowflakes.rest.Add(snowflake);
                }

            // If there's no Snowflakes of same size, create a dictionary entry
            else
                dictionary.Add(snowflake.Size, (snowflake, null));
        }

        return Result.NoTwinsFound;
    }

    private static bool AreAlike(Snowflake snowflake, (Snowflake initial, ICollection<Snowflake> rest) sameSizedSnowflakes)
    {
        return sameSizedSnowflakes.initial.IsLike(snowflake) ||
               sameSizedSnowflakes.rest?.Any(s => s.IsLike(snowflake)) == true;
    }
}

public struct Snowflake
{
    private const int NumArms = 6;

    private readonly int[] _arms;

    public Snowflake(params int[] arms)
    {
        if (arms == null)
            throw new ArgumentNullException(nameof(arms));
        
        if (arms.Length != NumArms)
            throw new ArgumentException(
                $"Arg must be contain exactly {NumArms} items",
                    nameof(arms));

        _arms = arms;
        Size = _arms.Sum();
    }

    public int Size { get; }

    public override bool Equals(object obj)
    {
        return obj is Snowflake other && Equals(other);
    }

    /// <summary>
    /// Returns a valid indicating whether the this Snowflake equals the other Snowflake
    /// </summary>
    /// <remarks>Two Snowflakes are equal if the arms of both Snowflakes are the same
    /// in the same positions around the Snowflake</remarks>
    /// <param name="other">The other Snowflake</param>
    /// <returns><see langword="true" /> when this Snowflake is equal to the other
    /// Snowflake, otherwise <see langword="false" /></returns>
    public bool Equals(Snowflake other)
    {
        //if (ReferenceEquals(other, null))
        //    return false;
        //if (ReferenceEquals(this, other))
        //    return true;
        return _arms.SequenceEqual(other._arms);
    }

    public override int GetHashCode() => CalcHashCode();

    public override string ToString()
    {
        return string.Join(" ", _arms);
    }

    /// <summary>
    /// Returns a value indicating whether this Snowflake is like the other Snowflake
    /// </summary>
    /// <remarks>Two Snowflakes are alike if they are <see cref="Equals">equal</see> or
    /// can be made equal moving the second snowflake leftward or rightward through
    /// the first Snowflake</remarks>
    /// <param name="other">The other Snowflake</param>
    /// <returns><see langword="true" /> if this Snowflake is like the other Snowflake; otherwise
    /// <see langword="false" /></returns>
    public bool IsLike(Snowflake other)
    {
        if (this.Equals(other))
            return true;

        for (int start = 0; start < NumArms; start++)
        {
            if (IsSameMovingRight(other, start))
                return true;
            if (IsSameMovingLeft(other, start))
                return true;
        }

        return false;
    }

    private int CalcHashCode()
    {
        //return (int)(Size % 100_000);

        int hash = 17;
        for (int i = 0; i < NumArms; i++)
        {
            hash = hash * 31 + _arms[i].GetHashCode();
        }

        return hash;
    }

    private bool IsSameMovingRight(Snowflake other, int start)
    {
        for (int offset = 0; offset < NumArms; offset++)
        {
            int otherIndex = (start + offset) % NumArms;
            if (this._arms[offset] != other._arms[otherIndex])
                return false;
        }

        return true;
    }

    private bool IsSameMovingLeft(Snowflake other, int start)
    {
        for (int offset = 0; offset < NumArms; offset++)
        {
            int otherIndex = start - offset;
            if (otherIndex < 0)
                otherIndex += NumArms;
            if (_arms[offset] != other._arms[otherIndex])
                return false;
        }

        return true;
    }

    public static bool operator ==(Snowflake left, Snowflake right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Snowflake left, Snowflake right)
    {
        return !(left == right);
    }
}