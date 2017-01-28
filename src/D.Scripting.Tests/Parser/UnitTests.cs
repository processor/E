using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class UnitTests : TestBase
    {
        [Fact]
        public void A()
        {
            var unit = Parse<UnitLiteralSyntax>(@"50deg");

            Assert.Equal(50, (NumberLiteralSyntax)unit.Expression);
            Assert.Equal("deg", unit.UnitName);
        }
    }
}