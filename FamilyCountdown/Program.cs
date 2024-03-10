using System;
using System.Collections.Generic;
using System.Linq;
using FamilyTreeNode = FamilyCountdown.TreeNode<FamilyCountdown.FamilyMember>;

namespace FamilyCountdown
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var numTestCases = int.Parse(Console.ReadLine());

            for (int i = 1; i <= numTestCases; i++)
            {
                var testCaseArgs = Console.ReadLine().Split(' ');
                var numFamilyMembers = int.Parse(testCaseArgs[0]);
                var atDepth = int.Parse(testCaseArgs[1]);

                var familyMemberReader = FamilyMemberReader(numFamilyMembers);
                var results = FamilyCountdownSolver.Solve(familyMemberReader, atDepth);

                Console.WriteLine($"Tree {i}:");
                foreach (var result in results)
                    Console.WriteLine(result);
                Console.WriteLine();
            }
        }

        private static IEnumerable<string[]> FamilyMemberReader(int numFamilyMembers)
        {
            for (int j = 0; j < numFamilyMembers; j++)
            {
                yield return Console.ReadLine().Split(' ');
            }
        }
    }

    public class TreeNode<T>
    {
        public TreeNode(T value)
        {
            Value = value;
        }

        public IList<TreeNode<T>> Children { get; set; }
        public T Value { get; set; }
    }

    public class FamilyMember
    {
        public FamilyMember(string name, int score = 0)
        {
            Name =  name;
            Score = score;
        }

        public string Name { get; set; }
        public int Score { get; set; }

        public override string ToString() => $"{Name} {Score}";
    }

    public static class FamilyCountdownSolver
    {
        public static IEnumerable<FamilyMember> Solve(IEnumerable<string[]> familyMemberReader, int atDepth)
        {
            var treeNodes = BuildFamilyTree(familyMemberReader);
            UpdateScores(treeNodes, atDepth);

            var sortedNodes = from n in treeNodes
                              orderby
                                  n.Value.Score descending,
                                  n.Value.Name ascending
                              select n.Value;

            return CollateResults(sortedNodes.ToList());
        }

        private static IEnumerable<FamilyMember> CollateResults(IList<FamilyMember> members)
        {
            int i = 0;
            while (i < 3 && i < members.Count && members[i].Score > 0)
            {
                yield return members[i];
                i += 1;

                while (i < members.Count && members[i].Score == members[i - 1].Score)
                {
                    yield return members[i];
                    i += 1;
                }
            }
        }

        public static IList<FamilyTreeNode> BuildFamilyTree(IEnumerable<string[]> familyMemberDataProvider)
        {
            var treeNodes = new List<FamilyTreeNode>();
            var treeNodeMap = new Dictionary<string, FamilyTreeNode>();

            foreach (var memberData in familyMemberDataProvider)
            {
                var memberName = memberData[0];
                var numChildren = int.Parse(memberData[1]);
                var memberNode = treeNodeMap.TryGetValue(memberName, out var existingMemberNode)
                    ? existingMemberNode
                    : CreateFamilyTreeNode(memberName);
                memberNode.Children = new FamilyTreeNode[numChildren];

                var i = 0;
                foreach (var childName in memberData.Skip(2).Take(numChildren))
                {
                    var childNode = treeNodeMap.TryGetValue(childName, out var existingChildNode)
                        ? existingChildNode
                        : CreateFamilyTreeNode(childName);
                    treeNodeMap[childNode.Value.Name] = childNode;
                    memberNode.Children[i] = childNode;
                    i += 1;
                }

                treeNodeMap[memberNode.Value.Name] = memberNode;
                treeNodes.Add(memberNode);
            }

            return treeNodes;
        }

        private static FamilyTreeNode CreateFamilyTreeNode(string familyMemberName)
        {
            return new FamilyTreeNode(new FamilyMember(familyMemberName));
        }

        public static int CalcScore(FamilyTreeNode fromNode, int atDepth)
        {
            if (fromNode?.Children == null)
                return 0;

            if (atDepth == 1)
                return fromNode.Children.Count;
            
            var total = 0;
            foreach (var child in fromNode.Children)
                total += CalcScore(child, atDepth - 1);

            return total;
        }

        public static void UpdateScores(IEnumerable<FamilyTreeNode> nodes, int atDepth)
        {
            foreach (var node in nodes)
            {
                node.Value.Score = CalcScore(node, atDepth);
            }
        }
    }
}
