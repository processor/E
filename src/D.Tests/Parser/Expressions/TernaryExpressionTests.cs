using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class TernaryExpressionTests : TestBase
    {
        [Fact]
        public void A()
        {
            var ternary = Parse<TernaryExpressionSyntax>("a ? 1 : b");

            // Assert.Equal(Kind.TernaryExpression, ternary.Kind);

            Assert.Equal("a", (Symbol)ternary.Condition);
            // Assert.Equal(1,   (Integer)ternary.Left);
            Assert.Equal("b", (Symbol)ternary.Right);
        }

        [Fact]
        public void B()
        {
            var ternary = Parse<TernaryExpressionSyntax>("x < 0.5 ? (x * 2) ** 3 / 2 : ((x - 1) * 2) ** 3 + 2) / 2");

            Assert.Equal(Operator.LessThan, (ternary.Condition as BinaryExpressionSyntax).Operator);
        }
    }
}