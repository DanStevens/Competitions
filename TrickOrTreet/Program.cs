using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrickOrTreet
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            string line;

            while ((line = Console.ReadLine()) != null)
            {
                var tree = TrickOrTreetSolver.Parse(line);
                var result = TrickOrTreetSolver.Solve(tree);
                Console.WriteLine(result);
            }
        }
    }

    public static class TrickOrTreetSolver
    {
        private class TreeParser
        {
            private static readonly Regex TokenizerRegex = new Regex(@"\d+|\(|\)", RegexOptions.Compiled);
            private Match _matcher;

            public BinaryTreeNode<int?> Parse(string representation)
            {
                if (string.IsNullOrWhiteSpace(representation))
                    return null;

                _matcher = TokenizerRegex.Match(representation);
                return Helper();
            }

            BinaryTreeNode<int?> Helper()
            {
                var node = new BinaryTreeNode<int?>();

                if (_matcher.Value == "(")
                {
                    _matcher = _matcher.NextMatch();
                    node.Left = Helper();
                    _matcher = _matcher.NextMatch();
                    node.Right = Helper();
                    _matcher = _matcher.NextMatch();
                }
                else
                    node.Value = int.Parse(_matcher.Value);

                return node;
            }
        }

        public static BinaryTreeNode<int?> Parse(string representation)
        {
            return new TreeParser().Parse(representation);
        }

        public static Result Solve(BinaryTreeNode<int?> tree)
        {
            var result = new Result();

            var actions = new Action[]
            {
                () => result.NumCandy = CountCandy(tree),
                () => result.Height = Height(tree),
                () => result.StreetsWalked = CountStreetsWalked(tree),
            };
            Parallel.Invoke(actions);

            return result;
        }

        public static int CountCandy(BinaryTreeNode<int?> tree)
        {
            if (tree.Left == null && tree.Right == null)
                return tree.Value ?? 0;
            return CountCandy(tree.Left) + CountCandy(tree.Right);
        }

        public static int CountNodes(BinaryTreeNode<int?> tree)
        {
            if (tree.Left == null && tree.Right == null)
                return 1;
            return 1 + CountNodes(tree.Left) + CountNodes(tree.Right);
        }

        public static int CountStreetsWalked(BinaryTreeNode<int?> tree)
        {
            if (tree.Left == null && tree.Right == null)
                return 0;
            return CountStreetsWalked(tree.Left) + CountStreetsWalked(tree.Right) + 4;
        }

        public static int Height(BinaryTreeNode<int?> tree)
        {
            if (tree.Left == null && tree.Right == null)
                return 0;
            return 1 + Math.Max(Height(tree.Left), Height(tree.Right));
        }

        public class Result
        {
            public int NumCandy { get; set; }
            public int Height { get; set; }
            public int StreetsWalked { get; set; }
            public int MinStreetsWalked => StreetsWalked - Height;

            public override string ToString()
            {
                return $"{MinStreetsWalked} {NumCandy}";
            }
        }
    }

    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode(T value = default(T))
        {
            Value = value;
        }

        public BinaryTreeNode<T> Left { get; set; }

        public BinaryTreeNode<T> Right { get; set; }

        public T Value { get; set; }
    }
}
