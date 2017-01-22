using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class ArrayTests : TestBase
    {
        [Fact]
        public void Array1x4()
        {
            var statement = Parse<ArrayLiteral>(@"[ 0, 1, 2, 3 ]");

            Assert.Equal(4, statement.Count);

            Assert.Equal(0L, (Integer)statement[0]);
            Assert.Equal(1L, (Integer)statement[1]);
            Assert.Equal(2L, (Integer)statement[2]);
        }

        [Fact]
        public void ArrayInit()
        {
            var call = Parse<CallExpression>("[5] Element");

            Assert.Equal("List<Element>", call.FunctionName);
            Assert.Equal(5, (Integer)call.Arguments[0]);
        }

        [Fact]
        public void OfTuples()
        {
            var array = Parse<ArrayLiteral>("[(0, 1), (2, 3)]");

            Assert.Equal(2, array.Count);
        }

        [Fact]
        public void JaggedArray()
        {
            var statement = Parse<ArrayLiteral>(@"[ 
                [ 0, 1, 2, 3 ], 
                [ 4, 5, 6, 7 ],
                [ 8, 9, 10, 11 ],
                [ 12, 13, 14 ]
            ]");

            var row1 = (ArrayLiteral)(statement.Elements[0]);

            Assert.Equal(0, (Integer)row1[0]);
            Assert.Equal(1, (Integer)row1[1]);
            Assert.Equal(2, (Integer)row1[2]);
        }

        [Fact]
        public void OfNumbers()
        {
            var array = Parse<ArrayLiteral>(@"
[
  0, 1, 8, 16, 9, 2, 3, 10, 17, 24, 32, 25, 18, 11, 4, 5, 12, 19, 26,
  33, 40, 48, 41, 34, 27, 20, 13, 6, 7, 14, 21, 28, 35, 42, 49, 56, 57,
  50, 43, 36, 29, 22, 15, 23, 30, 37, 44, 51, 58, 59, 52, 45, 38, 31,
  39, 46, 53, 60, 61, 54, 47, 55, 62, 63
]");

            Assert.Equal(64, array.Count);
        }
    }
}