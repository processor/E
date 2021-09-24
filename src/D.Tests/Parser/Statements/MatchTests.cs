using E.Syntax;

namespace E.Parsing.Tests;

public class MatchTests : TestBase
{
    [Fact]
    public void X()
    {
        var match = Parse<MatchExpressionSyntax>(@"
match x >> 4 { 
  1 => true
}");
        Assert.Single(match.Cases);

        Assert.True(match.Expression is BinaryExpressionSyntax);
    }

    [Fact]
    public void MatchLiterals()
    {

        var match = Parse<MatchExpressionSyntax>(
            @"match instance { 
                  1        => 1,
                  2        => 2,
                  ""text"" => 3,
                  50 m     => ""really far""
                }
            ");

        Assert.Equal("1", ((ConstantPatternSyntax)match.Cases[0].Pattern).Constant.ToString());
        Assert.Equal("2", ((ConstantPatternSyntax)match.Cases[1].Pattern).Constant.ToString());
        Assert.Equal("text", ((ConstantPatternSyntax)match.Cases[2].Pattern).Constant.ToString());
        Assert.Equal("50 m", ((ConstantPatternSyntax)match.Cases[3].Pattern).Constant.ToString());
    }

    [Fact]
    public void MatchVariant()
    {

        var match = Parse<MatchExpressionSyntax>(
            @"match instance { 
                  A | B => 1,
                  _     => ""*""
                }
            ");

        Assert.Equal("Variant<A,B>", ((ConstantPatternSyntax)match.Cases[0].Pattern).Constant.ToString());

        Assert.True(match.Cases[1].Pattern is AnyPatternSyntax);
    }

    /*
    [Fact]
    public void MatchRecord()
    {

        var match = Parse<MatchExpressionSyntax>(
            @"match instance { 
               { x, b } => 1
            }
        ");

        Assert.Equal("Variant<A,B>", ((ConstantPatternSyntax)match.Cases[0].Pattern).Constant.ToString());

        Assert.True(match.Cases[1].Pattern is AnyPatternSyntax);
    }
    */

    [Fact]
    public void MatchTypes()
    {

        var match = Parse<MatchExpressionSyntax>(
            @"match instance { 
                  (image: Image) => image.resize(100, 100);
                  (video: Video) => video.resize(100, 100);
                }
            ");

        Assert.Equal("image", ((TypePatternSyntax)match.Cases[0].Pattern).VariableName);
        Assert.Equal("video", ((TypePatternSyntax)match.Cases[1].Pattern).VariableName);

    }


    [Fact]
    public void Match()
    {
        // ... |> format mp4
        var pipe = Parse<CallExpressionSyntax>(
            @"image 
                  |> split 1s
                  |> group a
                  |> match kind {
                     (a: Audio) => a;
                     (i: Image) => b;
                     (v: Video) => c;
                  }
                  |> format mp4
                ");
    }


    [Fact]
    public void MatchWhen()
    {
        var parser = new Parser(
            @"
                  let i     = 100
                  let debug = false

                  match i {
                    0...100 when debug == true  => a
                    0...100 when debug == false => a
                  }
                ");

        var a = (PropertyDeclarationSyntax)parser.Next();
        var b = (PropertyDeclarationSyntax)parser.Next();
        var c = parser.ReadMatch();      // switch

        Assert.Equal("i", a.Name.ToString());
        Assert.Equal("debug", b.Name.ToString());

        Assert.Equal(2, c.Cases.Count);

        var pattern1 = (RangePatternSyntax)c.Cases[0].Pattern;

        Assert.Equal("0", pattern1.Start.ToString());
        Assert.Equal("100", pattern1.End.ToString());
    }
}
