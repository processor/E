using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class PatternTests : TestBase
    {
        [Fact]
        public void X()
        {
            var m = Parse<MatchExpression>(@"
match x >> 4 { 
  1 => true
}");

            Assert.True(m.Expression is BinaryExpression);
        }

        [Fact]
        public void MatchTypes()
        {

            var match = Parse<MatchExpression>(
                @"match instance { 
                  (image: Image) => image.resize(100, 100)
                }
            ");

            Assert.Equal(1, match.Cases.Count);
            Assert.Equal("image", ((TypePattern)match.Cases[0].Pattern).VariableName);

        }

        [Fact]
        public void Match()
        {
            // ... |> format mp4
            var pipe = Parse<PipeStatement>(
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


            var match = (MatchExpression)((PipeStatement)pipe.Callee).Expression;

            Assert.Equal(3, match.Cases.Count);

            var pattern1 = (TypePattern)match.Cases[0].Pattern;

            Assert.Equal("Audio", pattern1.TypeExpression.ToString());
            Assert.Equal("a",    pattern1.VariableName);
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

            var a = (VariableDeclaration)parser.Next();
            var b = (VariableDeclaration)parser.Next();
            var c = parser.ReadMatch();      // switch

            Assert.Equal("i", a.Name.ToString());
            Assert.Equal("debug", b.Name.ToString());

            Assert.Equal(2, c.Cases.Count);

            var pattern1 = (RangePattern)c.Cases[0].Pattern;

            Assert.Equal("0", pattern1.Start.ToString());
            Assert.Equal("100", pattern1.End.ToString());
        }
    }
}