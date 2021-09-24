using E.Syntax;

namespace E.Parsing.Tests;

public class UnitDeclarationTests : TestBase
{
    [Fact]
    public void B()
    {
        var unit = Parse<UnitDeclarationSyntax>(@"Pascal unit(symbol: ""Pa"") { }");

        Assert.Equal("Pascal", unit.Name);
        Assert.Equal("Pa", unit.Symbol.ToString());

    }

    [Fact]
    public void C()
    {
        var unit = Parse<UnitDeclarationSyntax>("Pascal unit(symbol: \"Pa\", value: 1) : Pressure");

        Assert.Equal("Pascal", unit.Name);
        Assert.Equal("Pressure", unit.BaseType);
        Assert.Equal("Pa", unit.Symbol.ToString());
        Assert.Equal(1, (unit.Value as NumberLiteralSyntax));
    }

    [Fact]
    public void UnitDeclarationWithUnitValue()
    {
        var unit = Parse<UnitDeclarationSyntax>(@"
Degree unit (
  symbol: ""deg"", 
  value: (π/180) rad
): Angle { }".Trim());

        Assert.Equal("Degree", unit.Name);
        Assert.Equal("Angle", unit.BaseType);

        Assert.Equal("symbol", unit.Arguments[0].Name);
        Assert.Equal("deg", unit.Arguments[0].Value.ToString());

        Assert.Equal("(π / 180) rad", unit.Arguments[1].Value.ToString());
    }
}