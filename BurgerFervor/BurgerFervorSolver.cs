
using System;
using System.Linq;

public class BurgerFervorSolver
{
    public static void Main(string[] args)
    {
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            int[] mnt = line.Split(' ').Select(int.Parse).ToArray();
            var result = Solve(mnt[0], mnt[1], mnt[2]);
            Console.WriteLine(result);
        }
    }

    public static Result Solve(int m, int n, int t)
    {
        int result = SolveForT(m, n, t);
        if (result >= 0)
            return new Result(result);

        int i = t - 1;
        result = SolveForT(m, n, i);
        while (result == -1)
        {
            i -= 1;
            result = SolveForT(m, n, i);
        }
        return new Result(result, t - i);
    }

    public static int SolveForT(int m, int n, int t)
    {
        if (t == 0)
            return 0;

        int mSolution = t >= m ? SolveForT(m, n, t - m) : -1;
        int nSolution = t >= n ? SolveForT(m, n, t - n) : -1;

        if (mSolution == -1 && nSolution == -1)
            return -1;

        return Math.Max(mSolution, nSolution) + 1;
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
