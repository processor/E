using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class PatternTests : TestBase
    {
        [Fact]
        public void X()
        {
            var m = Parse<MatchExpressionSyntax>(@"
match x >> 4 { 
  1 => true
}");

            Assert.True(m.Expression is BinaryExpressionSyntax);
        }

        [Fact]
        public void MatchTypes()
        {

            var match = Parse<MatchExpressionSyntax>(
                @"match instance { 
                  (image: Image) => image.resize(100, 100)
                }
            ");

            Assert.Single(match.Cases);
            Assert.Equal("image", ((TypePatternSyntax)match.Cases[0].Pattern).VariableName);

        }

        /*
        [Fact]
        public void Match()
        {
            // ... |> format mp4
            var pipe = Parse<PipeStatementSyntax>(
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


            var match = (MatchExpressionSyntax)((PipeStatementSyntax)pipe.Callee).Expression;

            Assert.Equal(3, match.Cases.Length);

            var pattern1 = (TypePatternSyntax)match.Cases[0].Pattern;

            Assert.Equal("Audio", pattern1.TypeExpression.ToString());
            Assert.Equal("a",    pattern1.VariableName);
        }
        */
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

            Assert.Equal(2, c.Cases.Length);

            var pattern1 = (RangePatternSyntax)c.Cases[0].Pattern;

            Assert.Equal("0", pattern1.Start.ToString());
            Assert.Equal("100", pattern1.End.ToString());
        }
    }
}