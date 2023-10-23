namespace E.Parsing.Tests;

using Syntax;

public class IfTests
{
    [Fact]
    public void IfElse()
    {
        var statement = ParseIf(
            """
            if a > 16 {
              log "if branch"
            }
            else if a < 5 { 
              log "else if branch"
            }
            else {
              log "else branch"
            }
            """);

        var elseIf = statement.ElseBranch as ElseIfStatementSyntax;
        var e = elseIf.ElseBranch as ElseStatementSyntax;

        Assert.True(elseIf.Condition is BinaryExpressionSyntax);

        // Assert.Equal(1, elseIf.Body.Statements.Count);
        // Assert.Equal(1, e.Body.Statements.Count);
    }

    [Fact]
    public void If()
    {
        var statement = ParseIf(
            """
            if a > 16 {
              log "hello"
            }
            """);
    }

    [Fact]
    public void SingleOperator()
    {
        var statement = ParseIf(
            """
            if i > 100 {
                a = a + 1
            }
            """);
    }

    [Fact]
    public void MultipleOperators()
    {
        var statement = ParseIf(
            """
            if i > 100 && i < 1000 {
              a = a + 1
            }
            """);
    }

    [Fact]
    public void Parenthesis()
    {
        var statement = ParseIf(
            """
            if (1 >= 100) || (i <= 1000) {
               var a = match i {
                 0...100 => true
                 _       => false
               }
            }
            """);
    }

    /*
    [Fact]
    public void Between()
    {
        var f = ParseIf(@"
if i ∈ 100...1000 {
a++
}");
    }
    */

    public static IfStatementSyntax ParseIf(string text) => new Parser(text).ReadIf();
}
