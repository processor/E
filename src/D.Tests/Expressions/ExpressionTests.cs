using E.Expressions;
using E.Symbols;

namespace E.Tests.Expressions;

public class ExpressionTests
{
    [Fact]
    public void Parse()
    {
        var expression = (BinaryExpression)Expression.Parse("π / 180 rad");

        var lhs = (Symbol)expression.Left;
        var rhs = (UnitValueLiteral)expression.Right;

        Assert.Equal("π",             lhs.Name);
        Assert.Equal(Operator.Divide, expression.Operator);
        Assert.Equal("180",           rhs.Expression.ToString());
        Assert.Equal("rad",           rhs.UnitName);
    }
}