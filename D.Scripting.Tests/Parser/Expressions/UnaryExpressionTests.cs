using System.Collections.Generic;

using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class UnaryExpressionTests : TestBase
    {
        public static IEnumerable<object[]> ComparisonOperators
        {
            get
            {
                yield return new object[] { "+", Operator.UnaryPlus };
                yield return new object[] { "-", Operator.Negation };
            }
        }

        [Fact]
        public void Negate()
        {
            var expression = Parse<UnaryExpression>("-(a - b)");

            Assert.Equal(Operator.Negation, expression.Operator);

            var arg = (BinaryExpression)expression.Argument;

            Assert.Equal(Operator.Subtraction, arg.Operator);
        }

        [Fact]
        public void Not()
        {
            var expression = Parse<UnaryExpression>("!(1 == 1)");

            Assert.Equal(Operator.Not,          expression.Operator);
            Assert.Equal(Kind.EqualsExpression, expression.Argument.Kind);
        }

        [Fact]
        public void B()
        {
            var expression = Parse<UnaryExpression>("!a");

            Assert.Equal(Kind.Symbol, expression.Argument.Kind);
        }
    }
}