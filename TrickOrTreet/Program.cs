using System;
using System.Linq;
using System.Text.RegularExpressions;

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
            private static readonly Regex TokenizerRegex = new Regex(@"\d+|\(|\)");
            private int _pos = 0;
            private string[] _tokens;

            public BinaryTreeNode<int?> Parse(string representation)
            {
                if (string.IsNullOrWhiteSpace(representation))
                    return null;

                _tokens = Tokenize(representation);
                _pos = 0;

                return Helper();
            }

            BinaryTreeNode<int?> Helper()
            {
                var node = new BinaryTreeNode<int?>();

                if (_tokens[_pos] == "(")
                {
                    _pos += 1;
                    node.Left = Helper();
                    _pos += 1;
                    node.Right = Helper();
                    _pos += 1;
                }
                else
                {
                    node.Value = int.Parse(_tokens[_pos]);
                }

                return node;
            }

            internal static string[] Tokenize(string representation)
            {
                return TokenizerRegex.Matches(representation).Select(m => m.Value).ToArray();
            }
        }

        public static string[] Tokenize(string representation) => TreeParser.Tokenize(representation);

        public static BinaryTreeNode<int?> Parse(string representation)
        {
            return new TreeParser().Parse(representation);
        }

        public static Result Solve(BinaryTreeNode<int?> tree)
        {
            var numCandy = CountCandy(tree);
            var height = Height(tree);
            var minStreetsWalked = CountStreetsWalked(tree) - height;

            return new Result
            {
                NumCandy = numCandy,
                MinStreetsWalked = minStreetsWalked,
            };
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
            public int MinStreetsWalked { get; set; }

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
