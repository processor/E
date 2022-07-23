using E.Parsing.Tests;

namespace E.Syntax.Tests;

public class BlockTests : TestBase
{
    [Fact]
    public void While()
    {
        var statement = Parse<WhileStatementSyntax>(
            """
            while a > 1 {
              a = a + 1
            }
            """);
    }
}