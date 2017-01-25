using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;
    using Units;

    public class UnitTests : TestBase
    {
        [Fact]
        public void A()
        {
            Unit<double> unit = Parse<UnitLiteral>(@"50deg");

            Assert.Equal(50, unit.Quantity);
            Assert.Equal("deg", unit.Type.Name);
            Assert.Equal("50deg", unit.ToString());
        }
    }
}