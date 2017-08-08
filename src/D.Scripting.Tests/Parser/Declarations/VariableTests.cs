using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class VariableDeclarationTests : TestBase
    {
        [Fact]
        public void Z()
        {
            var var = Parse<VariableDeclarationSyntax>("let parts = split($0)");

            Assert.Equal("parts", var.Name);
            Assert.Equal(Kind.CallExpression, var.Value.Kind);
        }

        [Fact]
        public void FunctionVariable()
        {
            var statements = Parse<VariableDeclarationSyntax>(@"
    let sum = ƒ(a: Integer, b: Integer) => a + b
    ");

        }

        [Fact]
        public void Strings()
        {
            var let = Parse<VariableDeclarationSyntax>("let hi = \"fox\"");

            Assert.False(let.Flags.HasFlag(VariableFlags.Mutable));
            Assert.Equal("fox", (StringLiteralSyntax)let.Value);
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
            var var = Parse<VariableDeclarationSyntax>(text);

            Assert.Equal("x", var.Name.ToString());
            Assert.Equal("1", var.Value.ToString());
        }

        [Fact]
        public void TupleAssignment()
        {
            var var = Parse<VariableDeclarationSyntax>("let points = [ (0, 1), (2, 3) ]");

            Assert.Equal("points", var.Name.ToString());
        }

        [Fact]
        public void VarAssigns()
        {
            var var = Parse<VariableDeclarationSyntax>("var i = 1");

            Assert.Equal("i", var.Name.ToString());
            Assert.Equal("1", var.Value.ToString());
        }

        [Fact]
        public void Complex()
        {
            var b = Parse<VariableDeclarationSyntax>("let x = (5)");
            var c = Parse<VariableDeclarationSyntax>("let x: Integer | None = None");
            var d = Parse<VariableDeclarationSyntax>("let x: A & B = c");
            var e = Parse<VariableDeclarationSyntax>("let x: Integer > 10 = c;");
            // var f = Parse<VariableDeclaration>("let x: Integer between 0..1000 = c;");
        }

        [Fact]
        public void Compound()
        {
            var b = Parse<CompoundVariableDeclaration>(@"
let a = 1, 
    b = 2, 
    c: i32, 
    d: i32 = 4, 
    e: String = ""five"", 
    f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z: Number");

            Assert.Equal(26, b.Declarations.Length);

            // Assert.Equal("five", b.Declarations[4].Value.ToString());
        }
    }
}