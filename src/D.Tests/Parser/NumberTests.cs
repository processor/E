using Xunit;

using E.Syntax;

namespace E.Parsing.Tests
{
    public class NumberTests : TestBase
    {
        [Fact]
        public void Numbers()
        {
            Assert.Equal(1,          Parse<NumberLiteralSyntax>("1"));
            Assert.Equal(1.1,        Parse<NumberLiteralSyntax>("1.1"));
            Assert.Equal(1234567890, Parse<NumberLiteralSyntax>("1234567890"));
            Assert.Equal(9876543210, Parse<NumberLiteralSyntax>("9876543210"));

            Assert.Equal(-1234567890, Parse<NumberLiteralSyntax>("-1234567890"));
            Assert.Equal(-9876543210, Parse<NumberLiteralSyntax>("-9876543210"));
        }

        [Fact]
        public void Underscores()
        {
            Assert.Equal(100,             Parse<NumberLiteralSyntax>(@"1_0_0"));
            Assert.Equal(1000,            Parse<NumberLiteralSyntax>(@"1_000"));
            Assert.Equal(1000000,         Parse<NumberLiteralSyntax>(@"1_000_000"));
            Assert.Equal(1000000.1,       Parse<NumberLiteralSyntax>(@"1_000_000.1"));
            Assert.Equal(1000000.0001,    Parse<NumberLiteralSyntax>(@"1_000_000.000_1"));
            Assert.Equal(1000000.0000001, Parse<NumberLiteralSyntax>(@"1_000_000.000_000_1"));

            Assert.Equal(123456,          Parse<NumberLiteralSyntax>(@"1_2__3___4____5_____6"));
        }

        [Fact]
        public void Exponents()
        {
            Assert.Equal(10,        Parse<NumberLiteralSyntax>(@"1e1"));
            Assert.Equal(100,       Parse<NumberLiteralSyntax>(@"1e2"));
            Assert.Equal(1000,      Parse<NumberLiteralSyntax>(@"1e3"));
            Assert.Equal(10000,     Parse<NumberLiteralSyntax>(@"1e4"));
            Assert.Equal(100000,    Parse<NumberLiteralSyntax>(@"1e5"));
            Assert.Equal(1000000,   Parse<NumberLiteralSyntax>(@"1e6"));
            Assert.Equal(10000000,  Parse<NumberLiteralSyntax>(@"1e7"));
            // Assert.Equal(0.0000001, Parse<Float>(@"1e-7"));
        }
    }
}