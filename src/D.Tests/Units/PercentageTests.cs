using Xunit;

namespace D.Units.Tests
{
    public class PercentageTests
    {
        [Fact]
        public void Construct()
        {
            var a = new Percentage(1);

            Assert.Equal(1d, a.Value);

            Assert.Equal("100%", a.ToString());
        }
    }
}

