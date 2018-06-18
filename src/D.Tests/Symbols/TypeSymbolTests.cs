using Xunit;

namespace D.Parsing.Tests
{
    public class TypeSymbolTests
    {
        [Fact]
        public void Construct()
        {
            var symbol = new TypeSymbol("Int32");

            Assert.Equal("Int32", symbol.Name);
            Assert.Equal(SymbolFlags.None, symbol.Flags);
        }
    }
}