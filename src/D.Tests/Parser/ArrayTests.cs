using System;

using Xunit;

namespace E.Parsing.Tests
{
    using Syntax;

    public class ArrayTests : TestBase
    {
        

        [Fact]
        public void Initialize_Fixed()
        {
            var statement = Parse<ArrayInitializerSyntax>(@"[5] Pixel");

            Assert.Equal("Pixel", statement.ElementType);
            Assert.Equal("5", statement.Elements[0].ToString());

        }

        [Fact]
        public void Array1x4()
        {
            var statement = Parse<ArrayInitializerSyntax>(@"[ 0, 1, 2, 3 ]");

            var elements = statement.Elements;

            Assert.Equal(4, elements.Length);

            Assert.Equal(0L, (NumberLiteralSyntax)elements[0]);
            Assert.Equal(1L, (NumberLiteralSyntax)elements[1]);
            Assert.Equal(2L, (NumberLiteralSyntax)elements[2]);
        }

        /*
        [Fact]
        public void ArrayInit()
        {
            var call = Parse<CallExpressionSyntax>("[5] Element");

            Assert.Equal("List<Element>", call.FunctionName);
            Assert.Equal(5, (NumberLiteral)call.Arguments[0]);
        }
        */

        [Fact]
        public void OfTuples()
        {
            var array = Parse<ArrayInitializerSyntax>("[(0, 1), (2, 3)]");

            Assert.Equal(2, array.Elements.Length);
        }


        [Fact]
        public void UniformArray()
        {
            var statement = Parse<ArrayInitializerSyntax>(@"[ 
                [ 0, 1, 2 ], 
                [ 3, 4, 5 ],
                [ 6, 7, 8 ]
            ]");

            Assert.Equal(3, statement.Elements.Length);
            Assert.Equal(3, statement.Stride.Value);

            long i = 0;

            foreach (var row in statement.Elements)
            {
                foreach(var column in ((ArrayInitializerSyntax)row).Elements)
                {
                    Assert.Equal(i, (NumberLiteralSyntax)column);

                    i++;
                }
            }

        }
        [Fact]
        public void JaggedArray()
        {
            var statement = Parse<ArrayInitializerSyntax>(@"[ 
                [ 0, 1, 2, 3 ], 
                [ 4, 5, 6, 7 ],
                [ 8, 9, 10, 11 ],
                [ 12, 13, 14 ]
            ]");

            Assert.Equal(4, statement.Elements.Length);

            var row1 = (ArrayInitializerSyntax)(statement.Elements[0]);

            Assert.Equal(0, (NumberLiteralSyntax)row1.Elements[0]);
            Assert.Equal(1, (NumberLiteralSyntax)row1.Elements[1]);
            Assert.Equal(2, (NumberLiteralSyntax)row1.Elements[2]);
        }

        [Fact]
        public void OfNumbers()
        {
            var array = Parse<ArrayInitializerSyntax>(@"
[
  0, 1, 8, 16, 9, 2, 3, 10, 17, 24, 32, 25, 18, 11, 4, 5, 12, 19, 26,
  33, 40, 48, 41, 34, 27, 20, 13, 6, 7, 14, 21, 28, 35, 42, 49, 56, 57,
  50, 43, 36, 29, 22, 15, 23, 30, 37, 44, 51, 58, 59, 52, 45, 38, 31,
  39, 46, 53, 60, 61, 54, 47, 55, 62, 63
]");

            Assert.Equal(64, array.Elements.Length);
        }
    }
}