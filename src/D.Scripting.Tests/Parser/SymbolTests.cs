using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class SymbolTests : TestBase
    {
        [Fact]
        public void Multiwords()
        {
            Assert.Equal("BuildingDemolition",      Parse<Symbol>("Building`Demolition"));
            Assert.Equal("BuildingDemolitionEvent", Parse<Symbol>("Building`Demolition`Event"));
        }

        [Fact]
        public void WithDomain()
        {
            var symbol = GetTypeSymbol("physics::Momentum");

            Assert.Equal("physics", symbol.Module);
            Assert.Equal("Momentum", symbol.Name);
        }

        private TypeSymbol GetTypeSymbol(string text)
        {
            return Parse<TypeDeclarationSyntax>($@"
Unit type {{ 
    a: {text}
}}").Members[0].Type;


        }

        [Fact]
        public void ArrayWithDomain()
        {
            var symbol = GetTypeSymbol("[ physics::Momentum ]");
     
            Assert.Equal("List", symbol.Name);

            var a = symbol.Arguments[0];

            Assert.Equal("physics",  a.Module);
            Assert.Equal("Momentum", a.Name);
        }

        [Fact]
        public void Unicode()
        {
            Assert.Equal("☃", Parse<Symbol>("☃").Name);
            Assert.Equal("★", Parse<Symbol>("★").Name);
        }

        [Fact]
        public void Simple()
        {
            var symbol = GetTypeSymbol("Momentum");

            Assert.Equal(null, symbol.Module);
            Assert.Equal("Momentum", symbol.Name);
        }
    }
}