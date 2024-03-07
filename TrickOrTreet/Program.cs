using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TrickOrTreet
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class TrickOrTreetSolver
    {
        public static int CountCandy(BinaryTreeNode<int?> tree)
        {
            int total = 0;
            var stack = new Stack<BinaryTreeNode<int?>>();

            while (tree != null)
            {
                if (tree.Left != null && tree.Right != null)
                {
                    stack.Push(tree.Left);
                    tree = tree.Right;
                }
                else
                {
                    total += tree.Value ?? 0;
                    tree = stack.Count == 0 ? null : stack.Pop();
                }
            }

            return total;
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
