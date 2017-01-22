using Xunit;

namespace D.Parsing.Tests
{
    public class NumberTests : TestBase
    {
        [Fact]
        public void Numbers()
        {
            Assert.Equal(1,   Parse<Integer>(@"1"));
            Assert.Equal(1.1, Parse<Float>(@"1.1"));
        }

        [Fact]
        public void Underscores()
        {
            Assert.Equal(100,             Parse<Integer>(@"1_0_0"));
            Assert.Equal(1000,            Parse<Integer>(@"1_000"));
            Assert.Equal(1000000,         Parse<Integer>(@"1_000_000"));
            Assert.Equal(1000000.1,       Parse<Float>(@"1_000_000.1"));
            Assert.Equal(1000000.0001,    Parse<Float>(@"1_000_000.000_1"));
            Assert.Equal(1000000.0000001, Parse<Float>(@"1_000_000.000_000_1"));

            Assert.Equal(123456,          Parse<Integer>(@"1_2__3___4____5_____6"));
        }

        [Fact]
        public void Exponents()
        {
            Assert.Equal(10,        Parse<Integer>(@"1e1"));
            Assert.Equal(100,       Parse<Integer>(@"1e2"));
            Assert.Equal(1000,      Parse<Integer>(@"1e3"));
            Assert.Equal(10000,     Parse<Integer>(@"1e4"));
            Assert.Equal(100000,    Parse<Integer>(@"1e5"));
            Assert.Equal(1000000,   Parse<Integer>(@"1e6"));
            Assert.Equal(10000000,  Parse<Integer>(@"1e7"));
            Assert.Equal(0.0000001, Parse<Float>(@"1e-7"));
        }
    }
}