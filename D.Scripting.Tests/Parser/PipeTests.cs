using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;
    using Units;

    public class PipeTests : TestBase
    {
        [Fact]
        public void Simple()
        {
            var pipe = Parse<PipeStatement>("a |> function1 |> function2");

            var b = (PipeStatement)pipe.Callee;

            Assert.Equal("function2", ((CallExpression)pipe.Expression).FunctionName);
            Assert.Equal("function1", ((CallExpression)b.Expression).FunctionName);
        }

        [InlineData("image |> resize(width: 800px, height: 600px)")]
        [InlineData("image |> resize 800 px 600 px")]
        [InlineData("image |> resize 800px 600px")]
        public void Funcs(string text)
        {
            var pipe = Parse<PipeStatement>(text);

            var call = (CallExpression)pipe.Expression;

            Assert.Equal("image", pipe.Callee.ToString());
            Assert.Equal("resize", call.FunctionName);

            // Assert.Equal(2, call.Function.Definition.Arguments.Length);

            Assert.Equal(2, call.Arguments.Count);

            var arg1 = (IUnit)call.Arguments[0];
            var arg2 = (IUnit)call.Arguments[1];

            Assert.Equal("800px", arg1.ToString());
            Assert.Equal("600px", arg2.ToString());
        }       

        [Fact]
        public void Read3()
        {
            var pipe = Parse<PipeStatement>("image |> resize (800px, 600px) |> format Gif");

            Assert.Equal("format", ((CallExpression)pipe.Expression).FunctionName);

            var resizePipe = (PipeStatement)pipe.Callee;
            
            var resizePipeFunc = (CallExpression)resizePipe.Expression;

            Assert.Equal("resize", resizePipeFunc.FunctionName);

            Assert.Equal("800px", ((IUnit)resizePipeFunc.Arguments[0]).ToString());
            Assert.Equal("600px", ((IUnit)resizePipeFunc.Arguments[1]).ToString());
            Assert.Equal("image",  (Symbol)resizePipe.Callee);

            Assert.Equal("Gif", ((CallExpression)pipe.Expression).Arguments[0].ToString());
          
        }

        [Fact]
        public void ObjectDef()
        {
            var pipe = Parse<PipeStatement>(@"
f |> plot {
  x: 0...100
  y: 0...100
  z: 0...100
}");

            Assert.Equal("f", pipe.Callee.ToString());

            var call = (CallExpression)pipe.Expression;

            var record = (TypeInitializer)call.Arguments[0];

            Assert.Equal(null, record.Type);
            Assert.Equal("x", record[0].Name);
            Assert.Equal("y", record[1].Name);
            Assert.Equal("z", record[2].Name);
        }
    }
}
