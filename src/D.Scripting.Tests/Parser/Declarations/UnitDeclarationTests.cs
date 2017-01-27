using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class UnitDeclarationTests : TestBase
    {
        [Fact]
        public void A()
        {
            var unit = Parse<UnitDeclarationSyntax>("Pascal unit");

            Assert.Equal("Pascal", unit.Name);
        }

        [Fact]
        public void B()
        {
            var unit = Parse<UnitDeclarationSyntax>("Pa unit : Pressure");

            Assert.Equal("Pa", unit.Name);
            Assert.Equal("Pressure", unit.BaseType);
        }

        [Fact]
        public void C()
        {
            var unit = Parse<UnitDeclarationSyntax>("Pa unit : Pressure @name(\"Pascal\") @SI");

            Assert.Equal("Pa", unit.Name);
            Assert.Equal("Pressure", unit.BaseType);
            Assert.Equal("name", unit.Annotations[0].Name);
            Assert.Equal("Pascal", (StringLiteralSyntax)unit.Annotations[0].Arguments[0].Value);
            Assert.Equal("SI", unit.Annotations[1].Name);
        }

        [Fact]
        public void D()
        {
            var unit = Parse<UnitDeclarationSyntax>("Pa unit : Pressure @name(\"Pascal\") @SI = 1");

            Assert.Equal("Pa", unit.Name);
            Assert.Equal("Pressure", unit.BaseType);
            Assert.Equal("name", unit.Annotations[0].Name);
            Assert.Equal("Pascal", (StringLiteralSyntax)unit.Annotations[0].Arguments[0].Value);
            Assert.Equal(1, (NumberLiteralSyntax)unit.Expression);
        }
    }
}

