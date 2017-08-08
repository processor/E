using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class UnitDeclarationTests : TestBase
    {
        [Fact]
        public void B()
        {
            var unit = Parse<UnitDeclarationSyntax>(@"Pascal unit { symbol: ""Pa"" }");

            Assert.Equal("Pascal", unit.Name);
            Assert.Equal("Pa",     unit.Symbol.ToString());

        }

        [Fact]
        public void C()
        {
            var unit = Parse<UnitDeclarationSyntax>("Pascal unit : Pressure { symbol: \"Pa\"; value: 1 }");

            Assert.Equal("Pascal", unit.Name);
            Assert.Equal("Pressure", unit.BaseType);
            Assert.Equal("Pa", unit.Symbol.ToString());
            Assert.Equal(1, (unit.Value as NumberLiteralSyntax));
        }

        [Fact]
        public void UnitDeclarationWithUnitValue()
        {
            var unit = Parse<UnitDeclarationSyntax>(@"Degree unit: Angle { 
                symbol: ""deg""
                value: (π/180) rad 
            }");

            var value = unit.Value as UnitLiteralSyntax;

            Assert.Equal("Degree", unit.Name);
            Assert.Equal("Angle",  unit.BaseType);
            Assert.Equal("deg",    unit.Symbol.ToString());
            Assert.Equal("rad",    value.UnitName);
        }
    }
}

