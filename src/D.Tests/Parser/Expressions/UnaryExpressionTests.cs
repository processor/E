using System.Collections.Generic;

namespace E.Parsing.Tests;

using Syntax;

public class UnaryExpressionTests : TestBase
{
    public static IEnumerable<object[]> ComparisonOperators
    {
        get
        {
            yield return ["+", Operator.UnaryPlus];
            yield return ["-", Operator.Negation];
        }
    }

    [Fact]
    public void Negate()
    {
        var expression = Parse<UnaryExpressionSyntax>("-(a - b)");

        Assert.Equal(Operator.Negation, expression.Operator);

        var arg = (BinaryExpressionSyntax)expression.Argument;

        Assert.Equal(Operator.Subtract, arg.Operator);
    }

    [Fact]
    public void Not()
    {
        var expression = Parse<UnaryExpressionSyntax>("!(1 == 1)");

        Assert.Equal(Operator.Not, expression.Operator);
        // Assert.Equal(Kind.EqualsExpression, expression.Argument.Kind);
    }

    [Fact]
    public void B()
    {
        var expression = Parse<UnaryExpressionSyntax>("!a");

        Assert.Equal(SyntaxKind.Symbol, expression.Argument.Kind);
    }
}
