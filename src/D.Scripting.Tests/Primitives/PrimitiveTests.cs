using System.Linq;

using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class PrimitiveTests : TestBase
    {
        [Fact]
        public void A()
        {
            var a = Parse<TypeDeclarationSyntax>("Float32 struct");

            Assert.Equal("Float32", a.Name);
        }

        [Fact]
        public void B()
        {
            var a = Parse<TypeDeclarationSyntax>("Int64 struct @size(8)");

            Assert.Equal("Int64", a.Name);
            Assert.Equal(1, a.Annotations.Length);
            Assert.Equal("size", a.Annotations[0].Name);
            Assert.Equal(8, (NumberLiteralSyntax)a.Annotations[0].Arguments[0].Value);
        }

        [Fact]
        public void PrimitiveConstraints()
        {
            // type jsnum = i64 between (-2**53 - 1).. (2**53 - 1);

            /*
            var a = Parse<TypeDeclaration>("clamped  type : Float");                    // between 0..1                    // inclusive
            var b = Parse<TypeDeclaration>("positive type : Float >= 0");
            var c = Parse<TypeDeclaration>("evens    type : Int32 % 2 == 0");
            var d = Parse<TypeDeclaration>("notempty type : String where length > 0");

            Assert.Equal("clamped", a.Name.ToString());
            */   
        }
    }       
}