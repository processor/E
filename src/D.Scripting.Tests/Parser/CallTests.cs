using System;
using System.Linq;

using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class CallTests : TestBase
    {
        [Fact]
        public void Parethensis()
        {
            var a = Parse<CallExpression>("(a - b).negate(1)");

            var left = (BinaryExpression)a.Callee;

            Assert.Equal(Operator.Subtraction , left.Operator);
            Assert.Equal("negate"             , a.FunctionName);
            Assert.Equal(1                    , (Integer)a.Arguments[0]);
        }

        [Fact]
        public void CeilingAndFloor()
        {
            Assert.Equal("ceiling", Parse<CallExpression>("ceiling(5.5)").FunctionName);
            Assert.Equal("floor",   Parse<CallExpression>("floor(5.9)").FunctionName);

        }
        [Fact]
        public void Lambda()
        {
            var call = Parse<CallExpression>("hi(a => a * 2, b, c)");

            var lambda = (FunctionDeclaration)call.Arguments[0];

            Assert.True(lambda.IsAnonymous);
            Assert.Equal("a",           lambda.Parameters[0].Name);
            Assert.Equal(Kind.MultiplyExpression, lambda.Body.Kind);

            Assert.Equal("b", call.Arguments[1].ToString());
        }

        [Fact]
        public void Call()
        {
            var call = Parse<CallExpression>("run(x, y, z)");

            Assert.Equal("run", call.FunctionName);

            Assert.Equal(3, call.Arguments.Count);

            Assert.Equal("x", (Symbol)call.Arguments[0]);
            Assert.Equal("y", (Symbol)call.Arguments[1]);
            Assert.Equal("z", (Symbol)call.Arguments[2]);
        }

        [Fact]
        public void CallNamed()
        {
            var call = Parse<CallExpression>("move(x: 1, y: 2, z: 3)");

            Assert.Equal("move", call.FunctionName);

            Assert.Equal(3, call.Arguments.Count);

            Assert.Equal(1, (Integer)call.Arguments[0]);
            Assert.Equal(2, (Integer)call.Arguments[1]);
            Assert.Equal(3, (Integer)call.Arguments[2]);

            var args = call.Arguments.ToArray();

            Assert.Equal(1, (Integer)args[0].Value);
            Assert.Equal(2, (Integer)args[1].Value);
            Assert.Equal(3, (Integer)args[2].Value);

            Assert.Equal("x", args[0].Name);
            Assert.Equal("y", args[1].Name);
            Assert.Equal("z", args[2].Name);
        }

        [Fact]
        public void IndexAccess()
        {
            var call = Parse<CallExpression>("move(x: 1, y: 2, z: 3)");

            Assert.Equal(1, (Integer)call.Arguments["x"]);
            Assert.Equal(2, (Integer)call.Arguments["y"]);
            Assert.Equal(3, (Integer)call.Arguments["z"]);

            Assert.Throws<Exception>(() => call.Arguments["a"]);
        }
    }
}
