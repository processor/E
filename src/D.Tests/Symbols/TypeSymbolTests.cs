using Xunit;

namespace D.Parsing.Tests
{
    public class SymbolTableTests
    {
        public void A()
        {
            var table = new SymbolTable();

            table.Add("Int32", TypeSymbol.Int32);
            
            var result = table.TryGetValue("Int32", out var symbol);

            Assert.Equal(TypeSymbol.Int32, symbol);
        }
    }

    public class TypeSymbolTests
    {
        [Fact]
        public void Construct()
        {
            var symbol = new TypeSymbol("Int32");

            Assert.Equal("Int32", symbol.Name);
            Assert.Equal(SymbolFlags.None, symbol.Flags);
        }

        [Fact]
        public void ConstructWithArguments()
        {
            var symbol = new TypeSymbol("Array", new TypeSymbol("Int32"));

            Assert.Equal("Array<Int32>", symbol.ToString());
        }
    }
}