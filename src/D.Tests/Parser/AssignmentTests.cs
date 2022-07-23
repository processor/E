﻿using E.Symbols;
using E.Syntax;

namespace E.Parsing.Tests;

public class AssignmentTests : TestBase
{
    [Fact]
    public void OperatorAssign()
    {
        var assignment = Parse<BinaryExpressionSyntax>("i = 1");

        Assert.Equal("i", (Symbol)assignment.Left);
        Assert.Equal(ObjectType.AssignmentExpression, assignment.Operator.OpKind);
        Assert.Equal(1, (NumberLiteralSyntax)assignment.Right);
    }

    [Fact]
    public void TupleMutplicationWithComment()
    {
        var assignment = Parse<BinaryExpressionSyntax>("b = (10, 10) * 5kg // neat");

        var left = assignment.Left;

        var right = ((BinaryExpressionSyntax)assignment.Right);
    }


    [Fact]
    public void Logic1()
    {
        var statement = Parse<BinaryExpressionSyntax>("x = a || b && c");

        Assert.Equal("x", statement.Left.ToString());

        Assert.Equal(SyntaxKind.BinaryExpression, statement.Right.Kind);
    }

    [Fact]
    public void Read7()
    {
        var assignment = Parse<BinaryExpressionSyntax>("b = (10, 10) * 5kg");

        Assert.Equal(Operator.Assign, assignment.Operator);

        var right = ((BinaryExpressionSyntax)assignment.Right);

        Assert.NotNull(right);
    }

    [Fact]
    public void AssignmentPattern()
    {
        var assignment = Parse<BinaryExpressionSyntax>("(a, b) = (1, 3)");

        var l = (TupleExpressionSyntax)assignment.Left;
        var r = (TupleExpressionSyntax)assignment.Right;
    }

    [Fact]
    public void Read2()
    {
        var parser = new Parser(
            """
            image = 10
            b = 2
            """);

        var one = (BinaryExpressionSyntax)parser.Next();
        var two = (BinaryExpressionSyntax)parser.Next();

        Assert.Equal("image", one.Left.ToString());
        Assert.Equal("10", one.Right.ToString());

        Assert.Equal("b", two.Left.ToString());
        Assert.Equal("2", two.Right.ToString());
    }
}