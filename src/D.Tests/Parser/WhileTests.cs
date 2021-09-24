using E.Syntax;

namespace E.Parsing.Tests;

public class BlockTests : TestBase
{
    [Fact]
    public void While()
    {
        var statement = Parse<WhileStatementSyntax>(@"
while a > 1 {
  a = a + 1
}");
    }
}