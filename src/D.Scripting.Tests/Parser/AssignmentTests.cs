using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class AssignmentTests : TestBase
    {      
        [Fact]
        public void OperatorAssign()
        {
            var assignment = Parse<BinaryExpression>($"i = 1");

            Assert.Equal("i",                           (Symbol)assignment.Left);
            Assert.Equal(Kind.AssignmentExpression,     assignment.Kind);
            Assert.Equal(1,                             (Integer)assignment.Right);
        }

        [Fact]
        public void TupleMutplicationWithComment()
        {
            var assignment = Parse<BinaryExpression>(@"b = (10, 10) * 5kg // neat");

            var left = assignment.Left;

            var right = ((BinaryExpression)assignment.Right);
        }

       
        [Fact]
        public void Logic1()
        {
            var statement = Parse<BinaryExpression>(@"x = a || b && c");

            Assert.Equal("x", statement.Left.ToString());

            Assert.Equal(Kind.LogicalOrExpression, statement.Right.Kind);
        }

        [Fact]
        public void Read7()
        {
            var assignment = Parse<BinaryExpression>("b = (10, 10) * 5kg");

            Assert.Equal(Operator.Assign, assignment.Operator);
            
            var right = ((BinaryExpression)assignment.Right);
        }

        [Fact]
        public void AssignmentPattern()
        {
            var assignment = Parse<BinaryExpression>("(a, b) = (1, 3)");

            // Assert.Equal("image", ((Symbol)assignment.Left).Name);
            // Assert.Equal("10", assignment.Right.ToString());
        }

        [Fact]
        public void Read2()
        {
            var parser = new Parser(@"
image = 10
b = 2
");

            var one = (BinaryExpression)parser.Next();
            var two = (BinaryExpression)parser.Next();

            Assert.Equal("image", one.Left.ToString());
            Assert.Equal("10",    one.Right.ToString());

            Assert.Equal("b",     two.Left.ToString());
            Assert.Equal("2",     two.Right.ToString());
        }
    }
}