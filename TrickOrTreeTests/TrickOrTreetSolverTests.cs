using TrickOrTreet;
using Node = TrickOrTreet.BinaryTreeNode<int?>;

namespace TrickOrTreeTests
{
    public class TrickOrTreetSolverTests
    {
        [Fact]
        public void Create()
        {
            var solver = new TrickOrTreetSolver();
            Assert.NotNull(solver);
        }

        [Fact]
        public void BuildATree()
        {
            var root = new Node()
            {
                Left = new Node()
                {
                    Left = new Node(4),
                    Right = new Node(5),
                },
                Right = new Node(15),
            };

            Assert.Null(root.Value);
            Assert.Null(root.Left.Value);
            Assert.Equal(4, root.Left.Left.Value);
            Assert.Equal(5, root.Left.Right.Value);
            Assert.Null(root.Left.Value);
            Assert.Equal(15, root.Right.Value);

            var result = TrickOrTreetSolver.CountCandy(root);
            Assert.Equal(4 + 5 + 15, result);
        }
    }
}