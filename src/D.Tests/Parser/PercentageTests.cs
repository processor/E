using E.Syntax;

namespace E.Parsing.Tests;

public class PercentageTests : TestBase
{
    [Fact]
    public void CanParse()
    {
        var node = Parse<QuantitySyntax>("100%");

        Assert.Equal("100", node.Expression.ToString());
        Assert.Equal("%", node.UnitName);
    }
}