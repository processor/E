using Xunit;

using D.Syntax;

namespace D.Parsing.Tests
{
    public class NumberTests : TestBase
    {
        [Fact]
        public void Numbers()
        {
            Assert.Equal(1,   Parse<NumberLiteral>(@"1"));
            Assert.Equal(1.1, Parse<NumberLiteral>(@"1.1"));
        }

        [Fact]
        public void Underscores()
        {
            Assert.Equal(100,             Parse<NumberLiteral>(@"1_0_0"));
            Assert.Equal(1000,            Parse<NumberLiteral>(@"1_000"));
            Assert.Equal(1000000,         Parse<NumberLiteral>(@"1_000_000"));
            Assert.Equal(1000000.1,       Parse<NumberLiteral>(@"1_000_000.1"));
            Assert.Equal(1000000.0001,    Parse<NumberLiteral>(@"1_000_000.000_1"));
            Assert.Equal(1000000.0000001, Parse<NumberLiteral>(@"1_000_000.000_000_1"));

            Assert.Equal(123456,          Parse<NumberLiteral>(@"1_2__3___4____5_____6"));
        }

        [Fact]
        public void Exponents()
        {
            Assert.Equal(10,        Parse<NumberLiteral>(@"1e1"));
            Assert.Equal(100,       Parse<NumberLiteral>(@"1e2"));
            Assert.Equal(1000,      Parse<NumberLiteral>(@"1e3"));
            Assert.Equal(10000,     Parse<NumberLiteral>(@"1e4"));
            Assert.Equal(100000,    Parse<NumberLiteral>(@"1e5"));
            Assert.Equal(1000000,   Parse<NumberLiteral>(@"1e6"));
            Assert.Equal(10000000,  Parse<NumberLiteral>(@"1e7"));
            // Assert.Equal(0.0000001, Parse<Float>(@"1e-7"));
        }
    }
}