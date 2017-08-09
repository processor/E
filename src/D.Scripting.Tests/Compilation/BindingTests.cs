using Xunit;

namespace D.Scripting.Tests
{
    using Syntax;
    using Parsing.Tests;

    public class BindingTests : TestBase
    {
        [Fact]
        public void Constructor() 
        {
            var func = Parse<FunctionDeclarationSyntax>(@"
                Point ƒ <T: Number>(x: T, y: T, z: T) => Point<T>(x, y, z)
            ");

            Assert.Equal(null, func.ReturnType);
            
            var compiler = new Compiler();

            var f = compiler.VisitFunctionDeclaration(func);

            Assert.Equal("Point", f.ReturnType.Name);
            Assert.Equal("T", f.ReturnType.Arguments[0].Name);
        }

        [Fact]
        public void FuncToString()
        {
            var func = Parse<FunctionDeclarationSyntax>("toString ƒ () => $\"{x},{y},{z}\"");

            Assert.Equal("toString", func.Name);

            var compiler = new Compiler();

            var f = compiler.VisitFunctionDeclaration(func);

            Assert.Equal(Type.Get(Kind.String), f.ReturnType);
        }
    }
}
