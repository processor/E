using E.Syntax;
using Xunit;

namespace E.Parsing.Tests
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