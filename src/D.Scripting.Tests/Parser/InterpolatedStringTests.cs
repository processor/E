using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class InterpolatedStringTests : TestBase
    {
        [Fact]
        public void A()
        {
            var s = Parse<InterpolatedStringExpressionSyntax>(@"$""hello""");

            Assert.Equal(1, s.Children.Length);
            Assert.Equal("hello", (StringLiteralSyntax)s[0]);
        }
        
        [Fact]
        public void B()
        {
            var s = Parse<InterpolatedStringExpressionSyntax>(@"$""dear {name}:""");

            Assert.Equal(3, s.Children.Length);
            Assert.Equal("dear ", (StringLiteralSyntax)s[0]);
            Assert.Equal("name",  (Symbol)s[1]);
            Assert.Equal(":",     (StringLiteralSyntax)s[2]);
        }

        [Fact]
        public void C()
        {
            var s = Parse<InterpolatedStringExpressionSyntax>(@"$""{x},{y},{z}""");

            Assert.Equal(5, s.Children.Length);

            Assert.Equal("x", (Symbol)s[0] as Symbol);
            Assert.Equal(",", (StringLiteralSyntax)s[1]);
            Assert.Equal("y", (Symbol)s[2]);
            Assert.Equal(",", (StringLiteralSyntax)s[3]);
            Assert.Equal("z", (Symbol)s[4]);
        }

    }
}