using Xunit;

using D.Symbols;
using D.Syntax;

namespace D.Parsing.Tests
{
    public class SymbolTests : TestBase
    {
        [Fact]
        public void Multiwords()
        {
            Assert.Equal("BuildingDemolition",      Parse<Symbol>("Building`Demolition"));
            Assert.Equal("BuildingDemolitionEvent", Parse<Symbol>("Building`Demolition`Event"));
        }

        [Fact]
        public void WithComplexModule()
        {
            var symbol = GetTypeSymbol("Carbon::Components::Media::Video");

            Assert.Equal("Carbon",     symbol.Module.Parent.Parent.Name);
            Assert.Equal("Components", symbol.Module.Parent.Name);
            Assert.Equal("Media",      symbol.Module.Name);
            Assert.Equal("Video",      symbol.Name);
        }

        [Fact]
        public void WithModule()
        {
            var symbol = GetTypeSymbol("Physics::Momentum");

            Assert.Equal("Physics", symbol.Module);
            Assert.Equal("Momentum", symbol.Name);
        }

        [Fact]
        public void ArrayWithModule()
        {
            var symbol = GetTypeSymbol("[ Physics::Momentum ]");
     
            Assert.Equal("Array", symbol.Name);

            var a = symbol.Arguments[0];

            Assert.Equal("Physics",  a.Module);
            Assert.Equal("Momentum", a.Name);
        }

        [Fact]
        public void Unicode()
        {
            Assert.Equal("☃", Parse<Symbol>("☃").Name);
            Assert.Equal("★", Parse<Symbol>("★").Name);
        }

        [Fact]
        public void SingleWord()
        {
            var symbol = GetTypeSymbol("Momentum");

            Assert.Null(symbol.Module);
            Assert.Equal("Momentum", symbol.Name);
        }

        private static TypeSymbol GetTypeSymbol(string text)
        {
            return (Parse<TypeDeclarationSyntax>($@"
Unit struct {{ 
    a: {text}
}}").Members[0] as PropertyDeclarationSyntax).Type;
        }
    }
}