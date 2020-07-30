
using Xunit;

namespace D.Parsing.Tests
{
    public class ArraySymbolTests : TestBase
    {
        [Fact]
        public void ArraySymbols()
        {
            Assert.Equal("Array<Pixel>", Parse<TypeSymbol>("[] Pixel").ToString());
            Assert.Equal("Array<Shape>", Parse<TypeSymbol>("[] Shape").ToString());
        }

        [Fact]
        public void SimpleSimple()
        {
            Assert.Equal("Fruit", Parse<TypeSymbol>("Fruit").ToString());
        }
    }
}