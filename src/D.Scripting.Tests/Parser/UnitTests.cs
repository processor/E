using Xunit;

namespace D.Parsing.Tests
{
    using Units;

    public class UnitTests : TestBase
    {
        [Fact]
        public void A()
        {
            var unit = Parse<Unit<double>>(@"50deg");

            Assert.Equal(50, unit.Quantity);
            Assert.Equal("deg", unit.Type.Name);
            Assert.Equal("50deg", unit.ToString());
        }
    }
}