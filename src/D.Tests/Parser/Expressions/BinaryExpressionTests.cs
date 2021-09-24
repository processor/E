using System.Collections.Generic;
using System.Text;

using E.Symbols;
using E.Syntax;

namespace E.Parsing.Tests;

using static Operator;

public class BinaryExpressionTests : TestBase
{
    public static IEnumerable<object[]> ComparisonOperators
    {
        get
        {
            yield return new object[] { "==", Equal };
            yield return new object[] { "!=", NotEqual };
            yield return new object[] { ">",  GreaterThan };
            yield return new object[] { ">=", GreaterOrEqual };
            yield return new object[] { "<=", LessOrEqual };
            yield return new object[] { "<",  LessThan };
        }
    }

    public static IEnumerable<object[]> LogicalOperators
    {
        get
        {
            yield return new object[] { "&&", LogicalAnd };
            yield return new object[] { "||", LogicalOr };
        }

    }
    public static IEnumerable<object[]> ArithmeticOperators
    {
        get
        {
            yield return new object[] { "+"  , Add };
            yield return new object[] { "-"  , Subtract };
            yield return new object[] { "*"  , Multiply };
            yield return new object[] { "/"  , Divide };
            yield return new object[] { "**" , Power };
            yield return new object[] { "%"  , Remainder };
        }
    }

    public static IEnumerable<object[]> BitwiseOperators
    {
        get
        {
            yield return new object[] { "<<",  LeftShift };
            yield return new object[] { ">>",  SignedRightShift };
            yield return new object[] { ">>>", UnsignedRightShift };
            yield return new object[] { "^",   BitwiseXor };
            yield return new object[] { "&",   BitwiseAnd };
            // yield return new object[] { "|",   BitwiseOr };      --- last one....
        }
    }

    [Fact]
    public void Assign1()
    {
        var assignment = Parse<BinaryExpressionSyntax>($"a *= 3");

        Assert.Equal("a", (Symbol)assignment.Left);

        Assert.Equal(Assign, assignment.Operator);

        Assert.Equal("a * 3", assignment.Right.ToString());
    }

    [Fact]
    public void ModulusOperator()
    {
        Assert.False(char.IsLetter('%'));

        var expression = Parse<BinaryExpressionSyntax>("a % 3");

        Assert.Equal("a", (Symbol)expression.Left);

        Assert.Equal(3,   (NumberLiteralSyntax)expression.Right);

        Assert.Equal(Remainder, expression.Operator);

        Assert.Equal("a % 3", expression.ToString());
    }

    [Theory]
    [MemberData(nameof(ArithmeticOperators))]
    [MemberData(nameof(BitwiseOperators))]
    public void CompoundAssignment(string symbol, Operator op)
    {
        var assignment = Parse<BinaryExpressionSyntax>($"a {symbol}= b");

        var expression = (BinaryExpressionSyntax)assignment.Right;

        Assert.Equal("a", (Symbol)assignment.Left);

        Assert.Equal(Assign, assignment.Operator);
      
        Assert.Equal("a", (Symbol)expression.Left);

        Assert.Equal(op, expression.Operator);
    }

    [Theory]
    [MemberData(nameof(ArithmeticOperators))]
    public void ArithmeticOps(string symbol, Operator op)
    {
        Assert.Equal(op, Parse<BinaryExpressionSyntax>($"a {symbol} b").Operator);
        Assert.Equal(op, Parse<BinaryExpressionSyntax>($"a{symbol}b").Operator);      // whitespace optional
    }

    [Theory]
    [MemberData(nameof(LogicalOperators))]
    public void LogicalOps(string symbol, Operator op)
    {
        Assert.Equal(op, Parse<BinaryExpressionSyntax>($"a {symbol} b").Operator);
        Assert.Equal(op, Parse<BinaryExpressionSyntax>($"a{symbol}b").Operator);      // whitespace optional
    }

    [Theory]
    [MemberData(nameof(ComparisonOperators))]
    public void Comparisions(string symbol, Operator op)
    {
        Assert.Equal(op, Parse<BinaryExpressionSyntax>($"a {symbol} b").Operator);
    }

    [Theory]
    [MemberData(nameof(BitwiseOperators))]
    public void BitwiseOps(string symbol, Operator op)
    {
        Assert.Equal(op, Parse<BinaryExpressionSyntax>($"a {symbol} b").Operator);
    }

    [Fact]
    public void G1()
    {
        Assert.Equal("1 + 1",                                 Debug("1 + 1"));
        Assert.Equal("1 + (1 * 3)",                           Debug("1 + 1 * 3"));
        Assert.Equal("1 + (4 ** 3)",                          Debug("1 + 4 ** 3"));
        Assert.Equal("1 + (1 - (2 * (3 / (4 ** (5 % 6)))))",  Debug("1 + 1 - 2 * 3 / 4 ** 5 % 6"));
        Assert.Equal("1 + ((4 ** 3) + 8)",                    Debug("1 + (4 ** 3) + 8"));
        Assert.Equal("(1 + 4) ** (3 + 8)",                    Debug("(1 + 4) ** (3 + 8)"));
        Assert.Equal("((1 + 4) ** (3 + 8)) * 15",             Debug("((1 + 4) ** (3 + 8)) * 15"));
        Assert.Equal("3 + (4 * 5)",                           Debug("3 + 4 * 5"));
        Assert.Equal("2 * (2 * 3)",                           Debug("2 * 2 * 3"));
        Assert.Equal("1 * (2 * (3 * 4))",                     Debug("1 * 2 * 3 * 4"));
        Assert.Equal("1 + ((4 ** 3) + 8)",                    Debug("1 + 4 ** 3 + 8"));
        Assert.Equal("3 + ((4 * 5) + 3)",                     Debug("3 + 4 * 5 + 3"));
    }

    [Fact]
    public void IsOperator()
    {
        Assert.Equal(Is, Parse<BinaryExpressionSyntax>("a is Integer").Operator);
        Assert.Equal(Is, Parse<BinaryExpressionSyntax>("b is String").Operator);
    }

    [Fact]
    public void AsOperator()
    {
        Assert.Equal(As, Parse<BinaryExpressionSyntax>("a as Integer").Operator);
    }

    [Fact]
    public void WithMemberAccess()
    {
        Assert.Equal("(a * b) * c", Debug("(a * b) * c"));
        Assert.Equal("(this.a * b) * c", Debug("(this.a * b) * c"));
    }

    [Fact]
    public void G2()
    {
        Assert.Equal("(1 + (1 + 3)) * 3",       Debug("(1 + 1 + 3) * 3"));
        Assert.Equal("(1 + (1 + 3)) * (3 * 4)", Debug("(1 + 1 + 3) * 3 * 4"));
        Assert.Equal("(1 + (1 + (3 * 5))) * a", Debug("(1 + 1 + 3 * 5) * a"));
        Assert.Equal("(a * b) * c",             Debug("(a * b) * c"));


        Assert.Equal("((this.n24 * (this.n33 * this.n41)) - ((this.n23 * (this.n34 * this.n41)) - ((this.n24 * (this.n31 * this.n43)) + ((this.n21 * (this.n34 * this.n43)) + ((this.n23 * (this.n31 * this.n44)) - (this.n21 * (this.n33 * this.n44))))))) * d", 
                
            Debug("(this.n24 * this.n33 * this.n41 - this.n23 * this.n34 * this.n41 - this.n24 * this.n31 * this.n43 + this.n21 * this.n34 * this.n43 + this.n23 * this.n31 * this.n44 - this.n21 * this.n33 * this.n44) * d"));


    }

    private string Debug(string text)
    {
        return BinaryExpressionWriter.Write(Parse<BinaryExpressionSyntax>(text));
    }

    [Fact]
    public void Groupings1()
    {
        Assert.True(Parse<BinaryExpressionSyntax>("(4 ** 3)").IsParenthesized);
        Assert.False(Parse<BinaryExpressionSyntax>("4 ** 3").IsParenthesized);

        var a = Parse<BinaryExpressionSyntax>("4 ** (3 * 16)");

        Assert.False(a.IsParenthesized);
        Assert.True(((BinaryExpressionSyntax)a.Right).IsParenthesized);

        Assert.Equal("4 ** (3 * 16)", a.ToString());
    }


    private string Debug2(string text)
        => BEWriter2.Write(Parse<BinaryExpressionSyntax>(text));

    [Fact]
    public void PrecedenceTests()
    {
        Assert.True(GreaterThan.Precedence > LogicalAnd.Precedence);

        Assert.True(Multiply.Precedence > Add.Precedence);

        Assert.True(Power.Precedence > Add.Precedence);

        Assert.True(Divide.Precedence == Multiply.Precedence);

        Assert.True(Add.Precedence == Subtract.Precedence);
    }

    [Fact]
    public void NestedGrouping()
    {
        var statement = Parse<BinaryExpressionSyntax>("(a * (b - 1)) + (d * c)");

        Assert.Equal(SyntaxKind.BinaryExpression, statement.Left.Kind);

        Assert.Equal("(a * (b - 1)) + (d * c)", statement.ToString());
    }

    [Fact]
    public void Grouping()
    {
        var statement = Parse<BinaryExpressionSyntax>("(1 * 5) + 3");

        var left = (BinaryExpressionSyntax)statement.Left;

        //Assert.Equal(1L, (Integer)left.Left);
        //Assert.Equal(5L, (Integer)left.Right);

        //Assert.Equal(3L, (Integer)statement.Right);
    }

    [Fact]
    public void A()
    {
        var b = Parse<BinaryExpressionSyntax>("5 * x * y");

        var l = (NumberLiteralSyntax)b.Left;
        var r = (BinaryExpressionSyntax)b.Right;

        Assert.Equal(5, l);

        Assert.Equal("x", r.Left.ToString());
        Assert.Equal("y", r.Right.ToString());
        Assert.Equal(SyntaxKind.Symbol, r.Left.Kind);
        Assert.Equal(SyntaxKind.Symbol, r.Right.Kind);
    }

    [Fact]
    public void Parse3()
    {
        var statement = Parse<BinaryExpressionSyntax>("1 g * 1 g * 2 g");

        var l = (UnitValueSyntax)statement.Left;
        var r = (BinaryExpressionSyntax)statement.Right;

        Assert.Equal("1 g", l.ToString());
        Assert.Equal("1 g", r.Left.ToString());
        Assert.Equal("2 g", r.Right.ToString());
    }

    [Fact]
    public void Grouping3()
    {
        var statement = Parse<BinaryExpressionSyntax>("(1 * 5) + (3 + 5)");

        Assert.Equal(SyntaxKind.BinaryExpression, statement.Left.Kind);
        Assert.Equal(SyntaxKind.BinaryExpression, statement.Right.Kind);
    }

    [Fact]
    public void Group()
    {
        var b = Parse<BinaryExpressionSyntax>("3 * (5 + 5)");

        var l = b.Left;
        var r = b.Right;

        //Assert.Equal(3L, (Integer)l);

        Assert.Equal(Multiply, b.Operator);

        var rr = (BinaryExpressionSyntax)b.Right;

        //Assert.Equal(5L,        (Integer)rr.Left);
        Assert.Equal(Add,  rr.Operator);
        //Assert.Equal(5L,        (Integer)rr.Right);
            
        Assert.Equal("3 * (5 + 5)", b.ToString());
    }

    [Fact]
    public void Read4()
    {
        var statement = Parse<BinaryExpressionSyntax>(@"5 * 10px");

        Assert.Equal(Multiply, statement.Operator);

        Assert.Equal("5", statement.Left.ToString());

        var right = (UnitValueSyntax)statement.Right;

        Assert.Equal(10, (NumberLiteralSyntax)right.Expression);
        Assert.Equal("px", right.UnitName);
    }

    [Fact]
    public void Read6()
    {
        var statement = Parse<BinaryExpressionSyntax>(@"(10, 10) * 5kg");

        Assert.Equal(Multiply, statement.Operator);

        var left = (TupleExpressionSyntax)statement.Left;
        var right = (UnitValueSyntax)statement.Right;


        Assert.Equal(5,     (NumberLiteralSyntax)right.Expression);
        Assert.Equal("kg",   right.UnitName);
        Assert.Equal("5 kg", right.ToString());
    }

    [Fact]
    public void X7()
    {
        var a = Parse<BinaryExpressionSyntax>("1kg + 1000g");

        Assert.Equal("1 kg", a.Left.ToString());
        Assert.Equal("1000 g", a.Right.ToString());
    }
}


public class BinaryExpressionWriter
{
    public static string Write(BinaryExpressionSyntax be)
    {
        var sb = new StringBuilder();

        WritePair(sb, be);

        return sb.ToString();
    }

    public static void WritePair(StringBuilder sb, BinaryExpressionSyntax be, int i = 0)
    {
        if (i > 0)
        {
            sb.Append('(');
        }

        if (be.Left is BinaryExpressionSyntax l)
        {
            WritePair(sb, l, i + 1);
        }
        else
        {
            sb.Append(be.Left.ToString());
        }

        sb.Append(' ');
        sb.Append(be.Operator.Name);
        sb.Append(' ');

        if (be.Right is BinaryExpressionSyntax r)
        {
            WritePair(sb, r, i + 1);
        }
        else
        {
            sb.Append(be.Right.ToString());
        }

        if (i > 0)
        {
            sb.Append(')');
        }
    }
}

public class BEWriter2
{
    public static string Write(BinaryExpressionSyntax be)
    {
        var sb = new StringBuilder();

        WritePair(sb, be);

        return sb.ToString();
    }

    public static void WritePair(StringBuilder sb, BinaryExpressionSyntax be, int i = 0)
    {
        var a = ((be.Right as BinaryExpressionSyntax)?.Operator.Precedence ?? 100) < be.Operator.Precedence;

        if (i > 0 && a)
        {
            sb.Append('(');
        }

        if (be.Left is BinaryExpressionSyntax lhs)
        {
            WritePair(sb, lhs, i + 1);
        }
        else
        {
            sb.Append(be.Left.ToString());
        }

        sb.Append(' ');
        sb.Append(be.Operator.Name);
        sb.Append(' ');

        if (be.Right is BinaryExpressionSyntax rhs)
        {
            WritePair(sb, rhs, i + 1);
        }
        else
        {
            sb.Append(be.Right.ToString());
        }

        if (i > 0 && a)
        {
            sb.Append(')');
        }
    }
}