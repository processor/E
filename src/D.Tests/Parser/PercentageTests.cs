using D.Syntax;
using Xunit;

namespace D.Parsing.Tests
{
    public class PercentageTests : TestBase
    {
        [Fact]
        public void A()
        {
            var node = Parse<UnitValueSyntax>("100%");

            Assert.Equal("100", node.Expression.ToString());
            Assert.Equal("%", node.UnitName);
        }
    }
}