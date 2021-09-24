using E.Syntax;

namespace E.Parsing.Tests;

public class TypeInitializerTests : TestBase
{
    [Fact]
    public void Nested()
    {
        var type = Parse<ObjectInitializerSyntax>(@"
Account(
  balance : 100,
  owner   : ""me"",
  created : Date(year: 2000, month: 01, day: 01)
)
");

        Assert.Equal("Account", type.Type);
        Assert.Equal(3, type.Arguments.Count);

        var dateObject = (ObjectInitializerSyntax)type.Arguments[2].Value;

        Assert.Equal("Date", dateObject.Type);
    }
    [Fact]
    public void RootScoped()
    {
        var type = Parse<ObjectInitializerSyntax>(@"
Point(
  x: 1,
  y: 2,
  z: 3
)");
        Assert.Equal("Point", type.Type.Name);
        Assert.Equal(3, type.Arguments.Count);
    }

    [Fact]
    public void Let()
    {
        var let = Parse<PropertyDeclarationSyntax>("let zero = Point(x: 0, y: 0, z: 0);");

        var value = (ObjectInitializerSyntax)let.Value;

        Assert.Equal("Point", value.Type.Name);
    }

    [Fact]
    public void BlockScoped()
    {
        var ifS = Parse<IfStatementSyntax>(@"
if 1 + 1 == 3 {
  return Point(
    x: 1 + 1,
    y: 2 * 1,
    z: 3 / 1
  )
}");

        var r = (ReturnStatementSyntax)ifS.Body.Statements[0];

        var type = (ObjectInitializerSyntax)r.Expression;

        Assert.Equal("Point", type.Type.Name);

        Assert.Equal(3, type.Arguments.Count);

        Assert.Equal("x", type.Arguments[0].Name);
        Assert.Equal("y", type.Arguments[1].Name);
        Assert.Equal("z", type.Arguments[2].Name);
    }
}
