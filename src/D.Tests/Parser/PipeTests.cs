using Xunit;

namespace E.Parsing.Tests
{
    using E.Symbols;

    using Syntax;

    public class PipeTests : TestBase
    {
        [Fact]
        public void Simple()
        {
            var pipe = Parse<CallExpressionSyntax>("a |> function1 |> function2");

            var b = (CallExpressionSyntax)pipe.Callee;

            Assert.Equal("function2", pipe.Name);
            Assert.Equal("function1", b.Name);
        }

        [Theory]
        [InlineData("image |> resize(width: 800px, height: 600px)")]
        [InlineData("image |> resize(800 px, 600 px)")]
        [InlineData("image |> resize(800px, 600px)")]
        public void Funcs(string text)
        {
            var expression = Parse<CallExpressionSyntax>(text);

            Assert.Equal("image", expression.Callee.ToString());
            Assert.Equal("resize", expression.Name);

            Assert.Equal(2, expression.Arguments.Count);

            var arg1 = (UnitValueSyntax)expression.Arguments[0].Value;
            var arg2 = (UnitValueSyntax)expression.Arguments[1].Value;

            Assert.Equal("800 px", arg1.ToString());
            Assert.Equal("600 px", arg2.ToString());
        }       

        [Fact]
        public void Read3()
        {
            var pipe = Parse<CallExpressionSyntax>("image |> resize (800px, 600px) |> GIF::encode");

            var namePipe = (MethodSymbol)pipe.Name;
            Assert.Equal("GIF::encode", pipe.Name);

            Assert.Equal("GIF", namePipe.Module.Name);
            Assert.Equal("encode", namePipe.Name);

            var resizePipe = (CallExpressionSyntax)pipe.Callee;
            
            Assert.Equal("resize", resizePipe.Name);

            Assert.Equal("800 px", ((UnitValueSyntax)resizePipe.Arguments[0].Value).ToString());
            Assert.Equal("600 px", ((UnitValueSyntax)resizePipe.Arguments[1].Value).ToString());
            Assert.Equal("image",  (Symbol)resizePipe.Callee);

          
        }

        [Fact]
        public void Read5()
        {
            var type = Parse<TypeSymbol>("GIF::encode");

            // This should be a method symbol... lowercase call

            Assert.Equal("GIF::encode", type.ToString());

        }

        [Fact]
        public void ObjectDef()
        {
            var pipe = Parse<CallExpressionSyntax>(@"
f |> plot(
  x: 0...100,
  y: 0...100,
  z: 0...100
)");

            Assert.Equal("f", pipe.Callee.ToString());

            var args = pipe.Arguments;

            Assert.True(pipe.IsPiped);
            Assert.Equal(3, pipe.Arguments.Count);

            Assert.Equal("x", args[0].Name);
            Assert.Equal("y", args[1].Name);
            Assert.Equal("z", args[2].Name);
        }
    }
}
