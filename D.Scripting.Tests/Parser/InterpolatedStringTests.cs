using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class InterpolatedStringTests : TestBase
    {
        [Fact]
        public void A()
        {
            var s = Parse<InterpolatedStringExpression>(@"$""hello""");

            Assert.Equal(1, s.Children.Length);
            Assert.Equal("hello", (StringLiteral)s[0]);
        }
        
        [Fact]
        public void B()
        {
            var s = Parse<InterpolatedStringExpression>(@"$""dear {name}:""");

            Assert.Equal(3, s.Children.Length);
            Assert.Equal("dear ", (StringLiteral)s[0]);
            Assert.Equal("name",  (Symbol)s[1]);
            Assert.Equal(":",     (StringLiteral)s[2]);
        }

        [Fact]
        public void C()
        {
            var s = Parse<InterpolatedStringExpression>(@"$""{x},{y},{z}""");

            Assert.Equal(5, s.Children.Length);

            Assert.Equal("x", (Symbol)s[0] as Symbol);
            Assert.Equal(",", (StringLiteral)s[1]);
            Assert.Equal("y", (Symbol)s[2]);
            Assert.Equal(",", (StringLiteral)s[3]);
            Assert.Equal("z", (Symbol)s[4]);
        }

    }
}