using Xunit;

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
        public void WithDomain()
        {
            var symbol = Parse<Symbol>("physics::Momentum");

            Assert.Equal("physics", symbol.Domain);
            Assert.Equal("Momentum", symbol.Name);
        }

        [Fact]
        public void ArrayWithDomain()
        {
            var symbol = Parse<Symbol>("[ ] physics::Momentum");

            Assert.Equal("List", symbol.Name);

            var a = symbol.Arguments[0];

            Assert.Equal("physics",  a.Domain);
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
            var symbol = Parse<Symbol>("Momentum");

            Assert.Equal(null, symbol.Domain);
            Assert.Equal("Momentum", symbol.Name);
        }
    }
}