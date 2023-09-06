using E.Syntax;

namespace E.Parsing.Tests;

public class UsingStatementTests : TestBase
{
    [Fact]
    public void SingleUsing()
    {
        var statement = Parse<UsingStatement>("using imaging");

        Assert.Equal("imaging", statement.Domains[0].Name);
    }

    [Fact]
    public void CanParseUsingList()
    {
        var statement = Parse<UsingStatement>("using accounting, finance, taxation;");

        Assert.Equal("accounting", statement[0]);
        Assert.Equal("finance",    statement[1]);
        Assert.Equal("taxation",   statement[2]);
    }
}