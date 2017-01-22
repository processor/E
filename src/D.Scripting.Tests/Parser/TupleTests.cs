using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class TupleTests : TestBase
    {
        [Fact]
        public void ValueTuple()
        {
            var tuple = Parse<TupleExpression>(@"(0, 100, ""a"")");

            Assert.Equal(3, tuple.Size);

            Assert.Equal(0L,   (Integer)tuple.Elements[0]);
            Assert.Equal(100L, (Integer)tuple.Elements[1]);
            Assert.Equal("a", (StringLiteral)tuple.Elements[2]);
        }

        [Fact]
        public void TuplePattern()
        {
            var tuple = Parse<TupleExpression>("(x, y)");

            Assert.Equal(2, tuple.Elements.Count);

            Assert.Equal("x", (Symbol)tuple.Elements[0]);
            Assert.Equal("y", (Symbol)tuple.Elements[1]);

            Assert.Equal(2, tuple.Elements.Count);
        }

        [Fact]
        public void TupleDefination()
        {
            var tuple = Parse<TupleExpression>("(x: i32, y: i64)");

            Assert.Equal(2, tuple.Elements.Count);

            var x = (NamedType)tuple.Elements[0];
            var y = (NamedType)tuple.Elements[1];

            Assert.Equal("x", x.Name);
            Assert.Equal("y", y.Name);
            Assert.Equal("i32", x.Type.ToString());
            Assert.Equal("i64", y.Type.ToString());

            Assert.Equal(2, tuple.Elements.Count);
        }
    }
}