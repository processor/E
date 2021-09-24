﻿using System.Collections.Generic;

using E.Symbols;
using E.Syntax;

namespace E.Parsing.Tests;

public class CallTests : TestBase
{
    [Fact]
    public void LambdaArgument()
    {
        var call = Parse<CallExpressionSyntax>("a |> map(c => c.height)");

        var arg = call.Arguments[0].Value as FunctionDeclarationSyntax;

        var lambda = (LambdaExpressionSyntax)arg.Body;

        Assert.Equal("c", arg.Parameters[0].Name);
    }

    [Fact]
    public void CssCalc()
    {
        var call = Parse<CallExpressionSyntax>("calc(1px - 2 * 3em)");

        Assert.Single(call.Arguments);

        var inner = call.Arguments[0].Value as BinaryExpressionSyntax;

        Assert.Equal("1 px - 2 * 3 em", inner.ToString());
    }

    [Fact]
    public void Parethensis()
    {
        var a = Parse<CallExpressionSyntax>("(a - b).negate(1)");

        var left = (BinaryExpressionSyntax)a.Callee;

        Assert.Equal(Operator.Subtract, left.Operator);
        Assert.Equal("negate", a.Name);
        Assert.Equal(1, (NumberLiteralSyntax)a.Arguments[0].Value);
    }

    [Fact]
    public void CeilingAndFloor()
    {
        Assert.Equal("ceiling", Parse<CallExpressionSyntax>("ceiling(5.5)").Name);
        Assert.Equal("floor", Parse<CallExpressionSyntax>("floor(5.9)").Name);
    }

    [Fact]
    public void Lambda()
    {
        var call = Parse<CallExpressionSyntax>("hi(a => a * 2, b, c)");

        var lambda = (FunctionDeclarationSyntax)call.Arguments[0].Value;

        Assert.True(lambda.IsAnonymous);
        Assert.Equal("a", lambda.Parameters[0].Name);
        // Assert.Equal(SyntaxKind.MultiplyExpression, lambda.Body.Kind);

        Assert.Equal("b", call.Arguments[1].Value.ToString());
    }

    [Fact]
    public void Call()
    {
        var call = Parse<CallExpressionSyntax>("run(x, y, z)");

        Assert.Equal("run", call.Name);

        Assert.Equal(3, call.Arguments.Count);

        Assert.Equal("x", (Symbol)call.Arguments[0].Value);
        Assert.Equal("y", (Symbol)call.Arguments[1].Value);
        Assert.Equal("z", (Symbol)call.Arguments[2].Value);
    }

    [Fact]
    public void CallNamed()
    {
        var syntax = Parse<CallExpressionSyntax>("move(x: 1, y: 2, z: 3)");

        Assert.Equal("move", syntax.Name);

        Assert.Equal(3, syntax.Arguments.Count);

        Assert.Equal(1, (NumberLiteralSyntax)syntax.Arguments[0].Value);
        Assert.Equal(2, (NumberLiteralSyntax)syntax.Arguments[1].Value);
        Assert.Equal(3, (NumberLiteralSyntax)syntax.Arguments[2].Value);

        var args = syntax.Arguments;

        Assert.Equal(1, (NumberLiteralSyntax)args[0].Value);
        Assert.Equal(2, (NumberLiteralSyntax)args[1].Value);
        Assert.Equal(3, (NumberLiteralSyntax)args[2].Value);

        Assert.Equal("x", args[0].Name);
        Assert.Equal("y", args[1].Name);
        Assert.Equal("z", args[2].Name);

        var complier = new Compiler();

        var call = complier.VisitCall(syntax);

        Assert.Equal(1, (Integer)call.Arguments["x"]);
        Assert.Equal(2, (Integer)call.Arguments["y"]);
        Assert.Equal(3, (Integer)call.Arguments["z"]);

        Assert.Throws<KeyNotFoundException>(() => call.Arguments["a"]);
    }
}
