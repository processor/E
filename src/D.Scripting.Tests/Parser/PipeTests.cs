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
            var pipe = Parse<PipeStatementSyntax>("a |> function1 |> function2");

            var b = (PipeStatementSyntax)pipe.Callee;

            Assert.Equal("function2", ((CallExpressionSyntax)pipe.Expression).FunctionName);
            Assert.Equal("function1", ((CallExpressionSyntax)b.Expression).FunctionName);
        }

        [InlineData("image |> resize(width: 800px, height: 600px)")]
        [InlineData("image |> resize 800 px 600 px")]
        [InlineData("image |> resize 800px 600px")]
        public void Funcs(string text)
        {
            var pipe = Parse<PipeStatementSyntax>(text);

            var call = (CallExpressionSyntax)pipe.Expression;

            Assert.Equal("image", pipe.Callee.ToString());
            Assert.Equal("resize", call.FunctionName);

            // Assert.Equal(2, call.Function.Definition.Arguments.Length);

            Assert.Equal(2, call.Arguments.Length);

            var arg1 = (UnitLiteralSyntax)call.Arguments[0].Value;
            var arg2 = (UnitLiteralSyntax)call.Arguments[1].Value;

            Assert.Equal("800px", arg1.ToString());
            Assert.Equal("600px", arg2.ToString());
        }       

        [Fact]
        public void Read3()
        {
            var pipe = Parse<PipeStatementSyntax>("image |> resize (800px, 600px) |> format Gif");

            Assert.Equal("format", ((CallExpressionSyntax)pipe.Expression).FunctionName);

            var resizePipe = (PipeStatementSyntax)pipe.Callee;
            
            var resizePipeFunc = (CallExpressionSyntax)resizePipe.Expression;

            Assert.Equal("resize", resizePipeFunc.FunctionName);

            Assert.Equal("800 px", ((UnitLiteralSyntax)resizePipeFunc.Arguments[0].Value).ToString());
            Assert.Equal("600 px", ((UnitLiteralSyntax)resizePipeFunc.Arguments[1].Value).ToString());
            Assert.Equal("image",  (Symbol)resizePipe.Callee);

            Assert.Equal("Gif", ((CallExpressionSyntax)pipe.Expression).Arguments[0].Value.ToString());
          
        }

        [Fact]
        public void ObjectDef()
        {
            var pipe = Parse<PipeStatementSyntax>(@"
f |> plot {
  x: 0...100
  y: 0...100
  z: 0...100
}");

            Assert.Equal("f", pipe.Callee.ToString());

            var call = (CallExpressionSyntax)pipe.Expression;

            var record = (NewObjectExpressionSyntax)call.Arguments[0].Value;

            Assert.Equal(null, record.Type);
            Assert.Equal("x", record.Members[0].Name);
            Assert.Equal("y", record.Members[1].Name);
            Assert.Equal("z", record.Members[2].Name);
        }
    }
}
