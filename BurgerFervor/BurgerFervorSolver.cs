
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            int[] mnt = line.Split(' ').Select(int.Parse).ToArray();
            var solver = new BurgerFervorSolver(mnt[0], mnt[1]);
            var result = solver.Solve(mnt[2]);
            Console.WriteLine(result);
        }
    }
}

public class BurgerFervorSolver
{
    private readonly int _m;
    private readonly int _n;
    private readonly Dictionary<int, int> _memo;

    public BurgerFervorSolver(int m, int n)
    {
        _m = m;
        _n = n;
        _memo = new Dictionary<int, int>();
    }

    public Result Solve(int t)
    {
        _memo[0] = 0;

        for (int i = 1; i <= t; i++)
        {
            int mSolution = i >= _m && _memo.TryGetValue(i - _m, out int rm) ? rm : -1;
            int nSolution = i >= _n && _memo.TryGetValue(i - _n, out int rn) ? rn : -1;
            if (mSolution >= 0 || nSolution >= 0)
                _memo[i] = Math.Max(mSolution, nSolution) + 1;
        }

        if (_memo.TryGetValue(t, out int r1))
            return new Result(r1);

        for (int i = t - 1; i > 0; i--)
        {
            if (_memo.TryGetValue(i, out int r2))
                return new Result(r2, t - i);
        }

        return new Result(0);
    }
}

public readonly struct Result
{
    public Result(int numBurgers, int minsRemaining = 0)
    {
        NumBurgers = numBurgers;
        MinsRemaining = minsRemaining;
    }

    public int NumBurgers { get; }
    public int MinsRemaining { get; }

    public override string ToString()
    {
        return NumBurgers +
               (MinsRemaining == 0 ? "" : " " + MinsRemaining);
    }
}
