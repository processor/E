using Xunit;

using D.Symbols;
using D.Syntax;

namespace D.Parsing.Tests
{
    public class VariableDeclarationTests : TestBase
    {
        [Fact]
        public void Z()
        {
            var var = Parse<PropertyDeclarationSyntax>("let parts = split($0)");

            Assert.Equal("parts", var.Name);
            Assert.Equal(SyntaxKind.CallExpression, var.Value.Kind);
        }

        [Fact]
        public void FunctionVariable()
        {
            var statements = Parse<PropertyDeclarationSyntax>(@"
    let sum = ƒ(a: Integer, b: Integer) => a + b
    ");
        }

        [Fact]
        public void Strings()
        {
            var declaration = Parse<PropertyDeclarationSyntax>("let hi = \"fox\"");

            Assert.False(declaration.IsMutable);
            Assert.Equal("fox", (StringLiteralSyntax)declaration.Value);
        }

        [Fact]
        public void Destructing()
        {
            var var = Parse<DestructuringAssignmentSyntax>("let (x, y, z) = point");

            Assert.Equal(3, var.Variables.Length);
            Assert.Equal("point", (Symbol)var.Instance);

            Assert.Equal("x", var.Variables[0].Name);
            Assert.Equal("y", var.Variables[1].Name);
            Assert.Equal("z", var.Variables[2].Name);
        }

        [Fact]
        public void TypedDestructing()
        {
            var var = Parse<DestructuringAssignmentSyntax>("let (x: Integer, y: Integer, z: Integer) = point");

            Assert.Equal("Integer", var.Variables[0].Type);
            Assert.Equal("Integer", var.Variables[1].Type);
            Assert.Equal("Integer", var.Variables[2].Type);
        }

        [Theory]
        [InlineData("let x = 1")]
        [InlineData("let x: Integer = 1")]
        // [InlineData("let (x) = 1")]
        // [InlineData("let (x: Integer) = (1)")] // ensure parenthsis have no effect
        public void ParseTests(string text)
        {
            var var = Parse<PropertyDeclarationSyntax>(text);

            Assert.Equal("x", var.Name.ToString());
            Assert.Equal("1", var.Value.ToString());
        }

        [Fact]
        public void TupleAssignment()
        {
            var var = Parse<PropertyDeclarationSyntax>("let points = [ (0, 1), (2, 3) ]");

            Assert.Equal("points", var.Name.ToString());
        }

        [Fact]
        public void VarAssigns()
        {
            var var = Parse<PropertyDeclarationSyntax>("var i = 1");

            Assert.Equal("i", var.Name.ToString());
            Assert.Equal("1", var.Value.ToString());
        }

        [Fact]
        public void Complex()
        {
            var b = Parse<PropertyDeclarationSyntax>("let x = (5)");
            var c = Parse<PropertyDeclarationSyntax>("let x: Integer | None = None");
            var d = Parse<PropertyDeclarationSyntax>("let x: A & B = c");
            var e = Parse<PropertyDeclarationSyntax>("let x: Integer > 10 = c;");
            // var f = Parse<VariableDeclaration>("let x: Integer between 0..1000 = c;");
        }

        [Fact]
        public void Compound()
        {
            var b = Parse<CompoundPropertyDeclaration>(@"
let a = 1, 
    b = 2, 
    c: i32, 
    d: i32 = 4, 
    e: String = ""five"", 
    f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z: Number");

            Assert.Equal(26, b.Members.Length);

            // Assert.Equal("five", b.Declarations[4].Value.ToString());
        }
    }
}