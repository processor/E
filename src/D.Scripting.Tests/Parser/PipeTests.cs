using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;
    using Units;

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

        [InlineData("image |> resize(width: 800px, height: 600px)")]
        [InlineData("image |> resize 800 px 600 px")]
        [InlineData("image |> resize 800px 600px")]
        public void Funcs(string text)
        {
            var pipe = Parse<CallExpressionSyntax>(text);

            Assert.Equal("image", pipe.Callee.ToString());
            Assert.Equal("resize", pipe.Name);

            // Assert.Equal(2, call.Function.Definition.Arguments.Length);

            Assert.Equal(2, pipe.Arguments.Length);

            var arg1 = (UnitLiteralSyntax)pipe.Arguments[0].Value;
            var arg2 = (UnitLiteralSyntax)pipe.Arguments[1].Value;

            Assert.Equal("800px", arg1.ToString());
            Assert.Equal("600px", arg2.ToString());
        }       

        [Fact]
        public void Read3()
        {
            var pipe = Parse<CallExpressionSyntax>("image |> resize (800px, 600px) |> format(Gif)");

            Assert.Equal("format", pipe.Name);

            var resizePipe = (CallExpressionSyntax)pipe.Callee;
            
            Assert.Equal("resize", resizePipe.Name);

            Assert.Equal("800 px", ((UnitLiteralSyntax)resizePipe.Arguments[0].Value).ToString());
            Assert.Equal("600 px", ((UnitLiteralSyntax)resizePipe.Arguments[1].Value).ToString());
            Assert.Equal("image",  (Symbol)resizePipe.Callee);

            Assert.Equal("Gif", pipe.Arguments[0].Value.ToString());
          
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
            Assert.Equal(3, pipe.Arguments.Length);

            Assert.Equal("x", args[0].Name);
            Assert.Equal("y", args[1].Name);
            Assert.Equal("z", args[2].Name);
        }
    }
}
