using Xunit;

using D.Symbols;
using D.Syntax;

namespace D.Parsing.Tests
{
    public class TupleTests : TestBase
    {
        [Fact]
        public void Complicated()
        {
            var tuple = Parse<TupleExpressionSyntax>(@"(
                width: (columnGap * (columnCount - 1)) + (columnWidth * columnCount),
                height: height
            )");

            var a = tuple.Elements[0] as TupleElementSyntax;
            
            var binary = a.Value as BinaryExpressionSyntax;

            Assert.Equal("(columnGap * (columnCount - 1)) + (columnWidth * columnCount)", binary.ToString());
        }

        [Fact]
        public void NestedParenthesis()
        {
            var tuple = Parse<TupleExpressionSyntax>(@"(
                width  : (1 * 2),
                height : height
            )");

            var a = tuple.Elements[0] as TupleElementSyntax;
        }

        [Fact]
        public void NestedTuple()
        {
            var tuple = Parse<TupleExpressionSyntax>(@"(
                width: (1, 2),
                height: height
            )");

            var a = tuple.Elements[0] as TupleElementSyntax;
        }

        [Fact]
        public void ValueTuple()
        {
            var tuple = Parse<TupleExpressionSyntax>(@"(0, 100, ""a"")");

            Assert.Equal(3, tuple.Size);

            Assert.Equal(0L,   (NumberLiteralSyntax)tuple.Elements[0]);
            Assert.Equal(100L, (NumberLiteralSyntax)tuple.Elements[1]);
            Assert.Equal("a",  (StringLiteralSyntax)tuple.Elements[2]);
        }

        [Fact]
        public void TuplePattern()
        {
            var tuple = Parse<TupleExpressionSyntax>("(x, y)");

            Assert.Equal(2, tuple.Elements.Length);

            Assert.Equal("x", (Symbol)tuple.Elements[0]);
            Assert.Equal("y", (Symbol)tuple.Elements[1]);

            Assert.Equal(2, tuple.Elements.Length);
        }

        [Fact]
        public void TupleDefination()
        {
            var tuple = Parse<TupleExpressionSyntax>("(x: i32, y: i64)");

            Assert.Equal(2, tuple.Elements.Length);

            var x = (TupleElementSyntax)tuple.Elements[0];
            var y = (TupleElementSyntax)tuple.Elements[1];

            Assert.Equal("x",   x.Name);
            Assert.Equal("y",   y.Name);
            Assert.Equal("i32", x.Value.ToString());
            Assert.Equal("i64", y.Value.ToString());

            Assert.Equal(2, tuple.Elements.Length);
        }
    }
}