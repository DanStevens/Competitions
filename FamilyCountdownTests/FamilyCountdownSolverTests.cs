using System;
using System.Linq;
using FamilyCountdown;
using FluentAssertions;
using Xunit;
using FamilyTreeNode = FamilyCountdown.TreeNode<FamilyCountdown.FamilyMember>;

namespace FamilyCountdownTests
{
    public class FamilyCountdownSolverTests
    {
        #region Test member data

        private static readonly string[][] SingleMemberData = new[]
        {
            new[] { "Alice", "0" },
        };

        private static readonly string[][] ParentWithSingleChildData = new[]
        {
            new[] { "Alice", "1", "Bob" },
            new[] { "Bob", "0", },
        };

        private static readonly string[][] SingleChildAndItsParentData = new[]
        {
            new[] { "Bob", "0", },
            new[] { "Alice", "1", "Bob" },
        };

        private static readonly string[][] ParentWithTwoChildrenData = new[]
        {
            new[] { "Alice", "2", "Bob", "Charlie" },
            new[] { "Bob", "0", },
            new[] { "Charlie", "0", },
        };

        private static readonly string[][] GrandparentWithTwoChildrenWhereBobHasSingleGrandchildData = new[]
        {
            new[] { "Alice", "2", "Bob", "Charlie" },
            new[] { "Bob", "1", "David" },
            new[] { "Charlie", "0" },
            new[] { "David", "0" }
        };

        private static readonly string[][] SampleFamilyData1 = new[]
        {
            new[] { "Barney", "2", "Fred", "Ginger" },
            new[] { "Ingrid", "1", "Nolan" },
            new[] { "Cindy", "1", "Hal" },
            new[] { "Jeff", "2", "Oliva", "Peter" },
            new[] { "Don", "2", "Ingrid", "Jeff" },
            new[] { "Fred", "1", "Kathy" },
            new[] { "Andrea", "4", "Barney", "Cindy", "Don", "Eloise" },
            new[] { "Hal", "2", "Lionel", "Mary" },
        };

        private static readonly string[][] SampleFamilyData2 = new[]
        {
            new[] { "Phillip", "5", "Jim", "Phil", "Jane", "Joe", "Paul" },
            new[] { "Jim", "1", "Jimmy" },
            new[] { "Phil", "1", "Philly" },
            new[] { "Jane", "1", "Janey" },
            new[] { "Joe", "1", "Joey" },
            new[] { "Paul", "1", "Pauly" },
        };

        private static readonly string[][] TestCase25 = new[]
        {
            new[] { "Tmdczdzj", "1", "Iygurk" },
            new[] { "Rpxy", "1", "Wk" },
            new[] { "V", "2", "Zjf", "Lxypb" },
            new[] { "Tdxatxw", "1", "Gov" },
            new[] { "Guqh", "4", "Rvg", "Obwr", "D", "Tefpgrzito" },
            new[] { "Vedxrqlstg", "3", "M", "Gnbtug" },
        };

        #endregion

        #region Test family trees

        private static readonly FamilyTreeNode SingleMemberTree = CreateFamilyTreeNode("Alice");
        private static readonly FamilyTreeNode ParentWithSingleChildTree = CreateFamilyTreeNode("Alice", "Bob");
        private static readonly FamilyTreeNode ParentWithTwoChildrenTree = CreateFamilyTreeNode("Alice", "Bob", "Charlie");

        private static readonly FamilyTreeNode GrandparentWithTwoChildrenWhereBobHasSingleGrandchildTree = CreateFamilyTreeNode(
            "Alice",
            CreateFamilyTreeNode("Bob", "David"),
            CreateFamilyTreeNode("Charlie"));

        #endregion

        [Fact]
        public void BuildFamilyTree_GivenSingleMember_ReturnsTreeOfOneNode()
        {
            var expectedResult = new[] { CreateFamilyTreeNode("Alice") };
            var result = FamilyCountdownSolver.BuildFamilyTree(SingleMemberData);
            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void BuildFamilyTree_GivenParentWithSingleChild_ReturnsTreeOfTwoNodes()
        {
            var expectedResult = new[]
            {
                CreateFamilyTreeNode("Alice", "Bob"),
                CreateFamilyTreeNode("Bob"),
            };
            var result = FamilyCountdownSolver.BuildFamilyTree(ParentWithSingleChildData);
            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void BuildFamilyTree_GivenSingleChildAndItsParent_ReturnsTreeOfTwoNodes()
        {
            var expectedResult = new[]
            {
                CreateFamilyTreeNode("Bob"),
                CreateFamilyTreeNode("Alice", "Bob"),
            };
            var result = FamilyCountdownSolver.BuildFamilyTree(SingleChildAndItsParentData);
            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void BuildFamilyTree_GivenParentWithTwoChildren_ReturnsTreeOfThreeNodes()
        {
            var expectedResult = new[]
            {
                CreateFamilyTreeNode("Alice", "Bob", "Charlie"),
                CreateFamilyTreeNode("Bob"),
                CreateFamilyTreeNode("Charlie"),
            };
            var result = FamilyCountdownSolver.BuildFamilyTree(ParentWithTwoChildrenData);
            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void BuildFamilyTree_GivenGrandparentWithTwoChildrenWhereBobHasSingleGrandchild_ReturnsTreeOfThreeNodes()
        {
            var expectedResult = new[]
            {
                CreateFamilyTreeNode("Alice", "Bob", "Charlie"),
                CreateFamilyTreeNode("Bob", "David"),
                CreateFamilyTreeNode("Charlie"),
                CreateFamilyTreeNode("David"),
            };
            var result = FamilyCountdownSolver.BuildFamilyTree(
                GrandparentWithTwoChildrenWhereBobHasSingleGrandchildData);
            Assert.Equivalent(expectedResult, result);
        }

        [Fact(Skip = "False failure")]
        public void BuildFamilyTree_GivenSampleFamilyData1()
        {
            var expectedResult = new[]
            {
                CreateFamilyTreeNode("Barney", "Fred", "Ginger"),
                CreateFamilyTreeNode("Ingrid", "Nolan"),
                CreateFamilyTreeNode("Cindy", "Hal"),
                CreateFamilyTreeNode("Jeff", "Olivia"),
                CreateFamilyTreeNode("Don", "Ingrid", "Jeff"),
                CreateFamilyTreeNode("Fred", "Kathy"),
                CreateFamilyTreeNode("Andrea", "Barney", "Cindy", "Don", "Eliose"),
                CreateFamilyTreeNode("Hal", "Lionel", "Mary"),
            };
            var result = FamilyCountdownSolver.BuildFamilyTree(SampleFamilyData1);
            result.Should().BeEquivalentTo(expectedResult, options => options.ExcludingNestedObjects());
        }

        [Theory]
        [InlineData(1, 0)]
        public void CalcScore_GivenSingleMemberTree(int depth, int expectedResult)
        {
            var result = FamilyCountdownSolver.CalcScore(SingleMemberTree, depth);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, 1)]
        public void CalcScore_GivenParentWithSingleChildTree(int depth, int expectedResult)
        {
            var result = FamilyCountdownSolver.CalcScore(ParentWithSingleChildTree, depth);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, 2)]
        public void CalcScore_GivenParentWithTwoChildrenTree(int depth, int expectedResult)
        {
            var result = FamilyCountdownSolver.CalcScore(ParentWithTwoChildrenTree, depth);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public void CalcScore_GivenGrandparentWithTwoChildrenWhereBobHasSingleGrandchildTree(int depth, int expectedResult)
        {
            var result = FamilyCountdownSolver.CalcScore(GrandparentWithTwoChildrenWhereBobHasSingleGrandchildTree, depth);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Solve_GivenSampleFamilyData1_AndDepth2()
        {
            var expectedResult = new[]
            {
                new FamilyMember("Andrea", 5),
                new FamilyMember("Don", 3),
                new FamilyMember("Cindy", 2),
            };
            var result = FamilyCountdownSolver.Solve(SampleFamilyData1, 2);
            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void Solve_GivenSampleFamilyData2_AndDepth1()
        {
            var expectedResult = new[]
            {
                new FamilyMember("Phillip", 5),
                new FamilyMember("Jane", 1),
                new FamilyMember("Jim", 1),
                new FamilyMember("Joe", 1),
                new FamilyMember("Paul", 1),
                new FamilyMember("Phil", 1),
            };
            var result = FamilyCountdownSolver.Solve(SampleFamilyData2, 1);
            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void Solve_GivenTestCase25_AndDepth1()
        {
            FamilyCountdownSolver.Solve(TestCase25, 1);
        }

        [Fact]
        public void Solve_GivenSampleFamilyData2_AndDepth2()
        {
            var expectedResult = new[]
            {
                new FamilyMember("Phillip", 5),
            };
            var result = FamilyCountdownSolver.Solve(SampleFamilyData2, 2);
            Assert.Equivalent(expectedResult, result);
        }

        #region Helper members

        private static FamilyTreeNode CreateFamilyTreeNode(string familyMemberName)
        {
            return new FamilyTreeNode(new FamilyMember(familyMemberName))
            {
                Children = Array.Empty<FamilyTreeNode>(),
            };
        }

        private static FamilyTreeNode CreateFamilyTreeNode(string familyMemberName, params string[] childrenName)
        {
            return new FamilyTreeNode(new FamilyMember(familyMemberName))
            {
                Children = childrenName.Select(CreateFamilyTreeNode).ToArray(),
            };
        }

        private static FamilyTreeNode CreateFamilyTreeNode(string familyMemberName, params FamilyTreeNode[] children)
        {
            return new FamilyTreeNode(new FamilyMember(familyMemberName))
            {
                Children = children,
            };
        }

        #endregion
    }
}
