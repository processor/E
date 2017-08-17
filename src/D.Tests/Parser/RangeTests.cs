using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class RangeTests : TestBase
    {
        [Fact]
        public void RangeExpression()
        {
            var range = Parse<RangeExpression>("0...(5 * 2)");

            // Assert.Equal(0L, (Integer)range.Start);
            Assert.Equal(Kind.MultiplyExpression, range.End.Kind);
        }

        [Theory]
        [InlineData("0...10")]
        [InlineData("0 ... 10")]
        public void NumericRanges(string text)
        {
            var range = Parse<RangeExpression>(text);

            // Assert.Equal(0L,  (Integer)range.Start);
            // Assert.Equal(10L, (Integer)range.End);
        }

        [Theory]
        [InlineData("a...z")]
        [InlineData("A...Z")]
        public void LetterRanges(string text)
        {
            var range = Parse<RangeExpression>(text);

            Assert.Equal("a", range.Start.ToString().ToLower());
            Assert.Equal("z", range.End.ToString().ToLower());
        }
    }
}
