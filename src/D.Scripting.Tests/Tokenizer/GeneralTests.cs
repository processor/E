using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E.Mathematics;

namespace E.Parsing.Tests;

using static TokenKind;

public class TokenizerTests
{
    private static readonly Node env = new Node(new ArithmeticModule());

    [Theory]
    [InlineData('a')]
    [InlineData('1')]
    [InlineData('*')]
    [InlineData('ƒ')]
    [InlineData('π')]
    [InlineData('丐')]
    [InlineData('丵')]
    [InlineData('仙')]
    [InlineData(' ')]
    [InlineData(',')]
    public void CharacterTokens(char c)
    {
        var tokens = new Tokenizer($"'{c}'");

        Assert.Equal("'", tokens.Read(Apostrophe));
        Assert.Equal(c.ToString(), tokens.Read(Character));
        Assert.Equal("'", tokens.Read(Apostrophe));
    }

    [Theory]
    [InlineData("Compound'Word")]
    [InlineData("Compound 'Word")]
    [InlineData("Compound\t'Word")]
    public void CompoundWords(string text)
    {
        var tokens = new Tokenizer(text);

        Assert.Equal("Compound", tokens.Read(Identifier));
        Assert.Equal("'", tokens.Read(Apostrophe));
        Assert.Equal("Word", tokens.Read(Identifier));
    }

    [Fact]
    public void StringTokens()
    {
        var tokens = new Tokenizer("let animal = \"fox\"");

        Assert.Equal("let", tokens.Read(Let));
        Assert.Equal("animal", tokens.Read(Identifier));
        Assert.Equal("=", tokens.Read(Op));
        Assert.Equal("\"", tokens.Read(Quote));
        Assert.Equal("fox", tokens.Read(String));
        Assert.Equal("\"", tokens.Read(Quote));
    }

    [Fact]
    public void ReadLogical()
    {
        var tokens = new Tokenizer("10 + 5 || 20 - 5", env);

        Assert.Equal("10", tokens.Read(Number));
        Assert.Equal("+", tokens.Read(Op));
        Assert.Equal("5", tokens.Read(Number));

        Assert.Equal("||", tokens.Read(Op));

        Assert.Equal("20", tokens.Read(Number));
        Assert.Equal("-", tokens.Read(Op));
        Assert.Equal("5", tokens.Read(Number));
    }

    [Fact]
    public void OpAssign()
    {
        var tokens = new Tokenizer("a %= 3");

        Assert.Equal("a", tokens.Read(Identifier));
        Assert.Equal("%", tokens.Read(Op));
        Assert.Equal("=", tokens.Read(Op));
        Assert.Equal("3", tokens.Read(Number));
    }

    [Fact]
    public void ReadTuple()
    {
        var tokens = new Tokenizer("b = (10, 10) * 5 kg // comment!", env);

        Assert.Equal("b", tokens.Read(Identifier));

        Assert.Equal("=", tokens.Read(Op));

        Assert.Equal("(", tokens.Read(ParenthesisOpen));
        Assert.Equal("10", tokens.Read(Number));
        Assert.Equal(",", tokens.Read(Comma));
        Assert.Equal("10", tokens.Read(Number));
        Assert.Equal(")", tokens.Read(ParenthesisClose));

        Assert.Equal("*", tokens.Read(Op));
        Assert.Equal("5", tokens.Read(Number));

        var last = tokens.Next();

        Assert.Equal(Identifier, last.Kind);
        Assert.Equal(1, last.Start.Line);
        Assert.Equal("kg", last.Text);
        Assert.Equal(" // comment!", last.Trailing);

        Assert.Equal(EOF, tokens.Next().Kind);
    }

    [Fact]
    public void ReadNumbers()
    {
        var tokens = new Tokenizer("1 1.1 1.1e100 1.1e+100 1.1e-100");

        Assert.Equal("1", tokens.Read(Number));
        Assert.Equal("1.1", tokens.Read(Number));
        Assert.Equal("1.1e100", tokens.Read(Number));
        Assert.Equal("1.1e+100", tokens.Read(Number));
        Assert.Equal("1.1e-100", tokens.Read(Number));

        Assert.Equal(EOF, tokens.Next().Kind);
    }

    [Fact]
    public void Read()
    {
        var tokens = new Tokenizer("image |> resize 100 100");

        Assert.Equal("image", tokens.Read(Identifier));
        Assert.Equal(PipeForward, tokens.Next().Kind);

        Assert.Equal("resize", tokens.Read(Identifier));

        Assert.Equal("100", tokens.Read(Number));
        Assert.Equal("100", tokens.Read(Number));
        Assert.Equal(EOF, tokens.Next().Kind);
    }

    [Fact]
    public void ReadPositions()
    {
        var tokens = new Tokenizer(
            """
            image |> resize 100px
            |> format Gif
            |> stream
            """);

        Assert.Equal(new Location(1, 0, 0), tokens.Next().Start); // image
        Assert.Equal(new Location(1, 6, 6), tokens.Next().Start); // pipe

        Assert.Equal("resize", tokens.Read(Identifier));
        Assert.Equal("100", tokens.Read(Number));
        Assert.Equal("px", tokens.Read(Identifier));

        Assert.Equal(new Location(2, 0, 23), tokens.Next().Start); // pipe
        Assert.Equal(new Location(2, 3, 26), tokens.Next().Start); // format
        Assert.Equal(new Location(2, 10, 33), tokens.Next().Start); // Gif
        Assert.Equal(new Location(3, 0, 38), tokens.Next().Start); // pipe
    }

    [Fact]
    public void CanReadTwoStatements()
    {
        var tokenizer = new Tokenizer(
            """
            image = 10
            b = 2
            """);

        Assert.Equal(
            """
            image | Identifier |    1 |    0
            =     | Op         |    1 |    6
            10    | Number     |    1 |    8
            b     | Identifier |    2 |    0
            =     | Op         |    2 |    2
            2     | Number     |    2 |    4
            """.ReplaceLineEndings("\n"), tokenizer.Dump());
    }

    [Fact]
    public void CanReadTwoStatements_Linux()
    {
        var tokenizer = new Tokenizer(
            """
            image = 10
            b = 2
            """.ReplaceLineEndings("\n"));

        Assert.Equal(
            """
            image | Identifier |    1 |    0
            =     | Op         |    1 |    6
            10    | Number     |    1 |    8
            b     | Identifier |    2 |    0
            =     | Op         |    2 |    2
            2     | Number     |    2 |    4
            """.ReplaceLineEndings("\n"), tokenizer.Dump());
    }

    [Fact]
    public void Read2()
    {
        var tokens = new Tokenizer(
            @"|> composite
                  = image");

        Assert.Equal("|>", tokens.Read(PipeForward));
        Assert.Equal("composite", tokens.Read(Identifier));
        Assert.Equal("=", tokens.Read(Op));
        Assert.Equal("image", tokens.Read(Identifier));
        Assert.Equal(EOF, tokens.Next().Kind);
    }

    [Fact]
    public void Read3()
    {
        var tokens = new Tokenizer(
        """
        let image = get source key
        let faces = image |> detect Face

        if count faces == 0 
          Image.create 100px 100px #000
        else
          image 
          |> crop faces[0]
          |> resize 100px 100px 
          |> format extension
        """);

        Assert.Equal("let", tokens.Read(Let));
        Assert.Equal("image", tokens.Read(Identifier));
        Assert.Equal("=", tokens.Read(Op));
        Assert.Equal("get", tokens.Read(Identifier));
        Assert.Equal("source", tokens.Read(Identifier));
        Assert.Equal("key", tokens.Read(Identifier));

        Assert.Equal("let", tokens.Read(Let));
        Assert.Equal("faces", tokens.Read(Identifier));
        Assert.Equal("=", tokens.Read(Op));
        Assert.Equal("image", tokens.Read(Identifier));
        Assert.Equal("|>", tokens.Read(PipeForward));
        Assert.Equal(Identifier, tokens.ReadKind());
        Assert.Equal(Identifier, tokens.ReadKind());

        // if count faces == 0 
        Assert.Equal("if", tokens.Read(If));
        Assert.Equal("count", tokens.Read(Identifier));
        Assert.Equal("faces", tokens.Read(Identifier));
        Assert.Equal("==", tokens.Read(Op));
        Assert.Equal("0", tokens.Read(Number));

        // Image.create 100px 100px #000
        Assert.Equal("Image", tokens.Read(Identifier));
        Assert.Equal(".", tokens.Read(Dot));
        Assert.Equal("create", tokens.Read(Identifier));
        Assert.Equal("100", tokens.Read(Number));
        Assert.Equal("px", tokens.Read(Identifier));
        Assert.Equal("100", tokens.Read(Number));
        Assert.Equal("px", tokens.Read(Identifier));
        Assert.Equal("#000", tokens.Read(Identifier));

        // else
        Assert.Equal(Else, tokens.Next().Kind);
    }
}

public static class TokenizerExtensions
{
    public static string Dump(this Tokenizer tokenizer)
    {
        var tokens = new List<Token>();

        Token token;

        while ((token = tokenizer.Next()).Kind != TokenKind.EOF)
        {
            tokens.Add(token);
        }


        var sb = new StringBuilder();

        int textWidth = Math.Max("Text".Length, tokens.Max(t => t.Text.Length));
        int kindWidth = Math.Max("Kind".Length, tokens.Max(t => t.Kind.ToString().Length));

        int i = 0;

        foreach (var t in tokens)
        {
            if (i > 0)
            {
                sb.Append('\n');
            }
            sb.AppendFormat("{0,-" + textWidth + "} | {1,-" + kindWidth + "} | {2,4} | {3,4}", t.Text, t.Kind, t.Start.Line, t.Start.Column);

            i++;
        }
        
        return sb.ToString();
    }

    public static TokenKind ReadKind(this Tokenizer tokenizer) => tokenizer.Next().Kind;

    public static string Read(this Tokenizer tokenizer, TokenKind kind)
    {
        var token = tokenizer.Next();

        if (token.Kind != kind)
        {
            throw new Exception($"Expected:{kind}. Was {token}");
        }

        return token.Text;
    }
}
