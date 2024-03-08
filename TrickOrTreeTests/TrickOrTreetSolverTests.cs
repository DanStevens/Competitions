using System.Diagnostics.CodeAnalysis;
using TrickOrTreet;
using Node = TrickOrTreet.BinaryTreeNode<int?>;

namespace TrickOrTreeTests
{
    public class TrickOrTreetSolverTests
    {
        private static readonly Node SmallTree = new Node
        {
            Left = new Node
            {
                Left = new Node(4),
                Right = new Node(5),
            },
            Right = new Node(15),
        };

        private static readonly Node MediumTree = new Node
        {
            Left = new Node
            {
                Left = new Node
                {
                    Left = new Node(72),
                    Right = new Node(3)
                },
                Right = new Node
                {
                    Left = new Node(6),
                    Right = new Node
                    {
                        Left = new Node
                        {
                            Left = new Node
                            {
                                Left = new Node(4),
                                Right = new Node(9),
                            },
                            Right = new Node(15),
                        },
                        Right = new Node(2),
                    }
                }
            },
            Right = new Node()
            {
                Left = new Node(7),
                Right = new Node(41),
            }
        };

        [Fact]
        public void CountCandyInSmallTree()
        {
            var result = TrickOrTreetSolver.CountCandy(SmallTree);
            Assert.Equal(4 + 5 + 15, result);
        }

        [Fact]
        public void CountNodesInSmallTree()
        {
            var result = TrickOrTreetSolver.CountNodes(SmallTree);
            Assert.Equal(5, result);
        }

        [Fact]
        public void CountStreetsWalkedInSmallTree()
        {
            var result = TrickOrTreetSolver.CountStreetsWalked(SmallTree);
            Assert.Equal(8, result);
        }

        [Fact]
        public void HeightOfSmallTree()
        {
            var result = TrickOrTreetSolver.Height(SmallTree);
            Assert.Equal(2, result);
        }

        [Fact]
        public void CountStreetsWalkedInMediumTree()
        {
            var result = TrickOrTreetSolver.CountStreetsWalked(MediumTree);
            Assert.Equal(32, result);
        }

        [Fact]
        public void HeightOfMediumTree()
        {
            var result = TrickOrTreetSolver.Height(MediumTree);
            Assert.Equal(6, result);
        }

        [Fact]
        public void SolveForSmallTree()
        {
            var result = TrickOrTreetSolver.Solve(SmallTree);
            Assert.Equal(4 + 5 + 15, result.NumCandy);
            Assert.Equal(6, result.MinStreetsWalked);
            Assert.Equal("6 24", result.ToString());
        }

        [Fact]
        public void SolveForMediumTree()
        {
            var result = TrickOrTreetSolver.Solve(MediumTree);
            Assert.Equal(159, result.NumCandy);
            Assert.Equal(26, result.MinStreetsWalked);
            Assert.Equal("26 159", result.ToString());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Parse_GivenNullOrWhitespace_ReturnsNull(string representation)
        {
            Assert.Null(TrickOrTreetSolver.Parse(null));
        }

        [Theory]
        [InlineData(4)]
        public void Parse_GivenSingleNumber_ReturnsNodeWithNumber(int n)
        {
            var expected = new Node(n);
            var result = TrickOrTreetSolver.Parse(n.ToString());
            Assert.Equivalent(expected, result);
        }

        [Theory]
        [InlineData(4, 9)]
        public void Parse_GivenPairOfNumbers_ReturnsThreeNodeTree(int l, int r)
        {
            var expected = new Node
            {
                Left = new Node(l),
                Right = new Node(r)
            };
            var result = TrickOrTreetSolver.Parse($"({l} {r})");
            Assert.Equivalent(expected, result);
        }

        [Fact]
        public void GivenPairNestedInLeftOfRootNode_ReturnsFiveNodeTree()
        {
            const string representation = "((4 9) 15)";
            var expected = new Node
            {
                Left = new Node
                {
                    Left = new Node(4),
                    Right = new Node(9),
                },
                Right = new Node(15),
            };
            var result = TrickOrTreetSolver.Parse(representation);
            Assert.Equivalent(expected, result);
        }

        [Theory]
        [InlineData("((1 5) 8)", "6 14")]
        [InlineData("(1 3)", "3 4")]
        [InlineData("13", "0 13")]
        [InlineData("((1 2) (3 4))", "10 10")]
        [InlineData("(((((1 1) 1) 1) 1) 1)", "15 6")]
        [InlineData("(((1 2) (3 4)) ((6 7) (8 9))", "25 40")]
        [InlineData("((1 2) (((10 10) (3 4)) ((((1 1) 1) 1) 1)))", "34 35")]
        public void Solve(string input, string expectedOutput)
        {
            var tree = TrickOrTreetSolver.Parse(input);
            var result = TrickOrTreetSolver.Solve(tree);
            Assert.Equal(expectedOutput, result.ToString());
        }
    }
}