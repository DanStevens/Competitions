
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
        int result = SolveForT(t);
        if (result >= 0)
            return new Result(result);

        int i = t - 1;
        result = SolveForT(i);
        while (result == -1)
        {
            i -= 1;
            result = SolveForT(i);
        }
        return new Result(result, t - i);
    }

    public int SolveForT(int t)
    {
        if (_memo.TryGetValue(t, out int x))
            return x;

        if (t == 0)
        {
            _memo[t] = 0;
            return 0;
        }

        int mSolution = t >= _m ? SolveForT(t - _m) : -1;
        int nSolution = t >= _n ? SolveForT(t - _n) : -1;

        if (mSolution == -1 && nSolution == -1)
        {
            _memo[t] = -1;
            return -1;
        }

        var y = Math.Max(mSolution, nSolution) + 1;
        _memo[t] = y;
        return y;
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
