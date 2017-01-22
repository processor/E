using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    // tuples, vectors, and arrays share elements & may be enumerated & accessed by index

    public class MatrixTests : TestBase
    {
        [Fact]
        public void Matrix1x4()
        {
            var matrix = Parse<MatrixLiteral>(@"[ [ 0, 1, 2, 3 ] ]");

            Assert.Equal(0, (Integer)matrix.Elements[0]);
            Assert.Equal(1, (Integer)matrix.Elements[1]);
            Assert.Equal(2, (Integer)matrix.Elements[2]);
        }

        [Fact]
        public void Matrix4x4()
        {
            var matrix = Parse<MatrixLiteral>(@"[ 
                [ 0, 1, 2, 3 ], 
                [ 4, 5, 6, 7 ],
                [ 8, 9, 10, 11 ],
                [ 12, 13, 14, 15 ]
            ]");

            Assert.Equal(16, matrix.Elements.Length);

            Assert.Equal("0", matrix[0, 0].ToString());
            Assert.Equal("1", matrix[0, 1].ToString());
            Assert.Equal("2", matrix[0, 2].ToString());
            Assert.Equal("5", matrix[1, 1].ToString());
            Assert.Equal("10", matrix[2, 2].ToString());
        }

        [Fact]
        public void Matrix5x5()
        {
            var matrix = Parse<MatrixLiteral>(@"[ 
              [  0,   0,  -1,   0,   0 ],
              [  0,  -1,  -2,  -1,   0 ],
              [ -1,  -2,  16,  -2,  -1 ],
              [  0,  -1,  -2,  -1,   0 ],
              [  0,   0,  -1,   0,   0 ]
            ]");

            Assert.Equal(5, matrix.RowCount);
            Assert.Equal(5, matrix.ColumnCount);
            Assert.Equal(25, matrix.Elements.Length);


            var m2 = Numerics.Matrix<double>.Create(matrix);

            Assert.Equal(0d, m2[0, 0]);
            Assert.Equal(-1d, m2[2, 0]);
            Assert.Equal(-2d, m2[2, 1]);
            Assert.Equal(16d, m2[2, 2]);

            var r = m2 * 5;

            Assert.Equal(0d, r[1, 0]);

            Assert.Equal(-5d, r[1, 1]);

            Assert.Equal(-10d, r[1, 2]);
        }
    }
}
