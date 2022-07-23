using E.Syntax;

namespace E.Parsing.Units.Tests;

using E.Parsing.Tests;

public class UnitTests : TestBase
{
    [Fact]
    public void A()
    {
        var unit = Parse<UnitValueSyntax>("50deg");

        Assert.Equal(50, (NumberLiteralSyntax)unit.Expression);
        Assert.Equal("deg", unit.UnitName);
    }

    [Fact]
    public void B()
    {
        var unit = Parse<UnitValueSyntax>("50 deg");

        Assert.Equal(50, (NumberLiteralSyntax)unit.Expression);
        Assert.Equal("deg", unit.UnitName);
    }

    [Fact]
    public void C()
    {
        var unit = Parse<UnitValueSyntax>("(50 / 3) m²");

        var binary = (BinaryExpressionSyntax)unit.Expression;

        Assert.Equal(50, (NumberLiteralSyntax)binary.Left);
        Assert.Equal(3, (NumberLiteralSyntax)binary.Right);

        Assert.Equal("m", unit.UnitName);
        Assert.Equal(2, unit.UnitPower);
    }
}
