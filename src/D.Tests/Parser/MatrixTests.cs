using E.Expressions;
using E.Numerics;
using E.Syntax;

namespace E.Parsing.Tests;

// tuples, vectors, and arrays share elements & may be enumerated & accessed by index

public class MatrixTests : TestBase
{
    public static Matrix<double> FromText(string text)
    {
        var syntax = Parse<ArrayInitializerSyntax>(text);

        var arrayExpression = (ArrayInitializer)new Compiler().Visit(syntax);

        return Matrix<double>.Create(arrayExpression);
    }

    [Fact]
    public void Matrix2x4()
    {
        var matrix = FromText("""
            [ 
                [ 0, 1, 2, 3 ],
                [ 4, 5, 6, 7 ]
            ]
            """);

        Assert.Equal(0d, matrix[0, 0]);
        Assert.Equal(1d, matrix[0, 1]);
        Assert.Equal(2d, matrix[0, 2]);
    }

    [Fact]
    public void Matrix4x4()
    {
        var matrix = FromText(
            """
            [ 
              [ 0, 1, 2, 3 ], 
              [ 4, 5, 6, 7 ],
              [ 8, 9, 10, 11 ],
              [ 12, 13, 14, 15 ]
            ]
            """);

        Assert.Equal(4, matrix.ColumnCount);
        Assert.Equal(4, matrix.RowCount);

        Assert.Equal(0d,  matrix[0, 0]);
        Assert.Equal(1d,  matrix[0, 1]);
        Assert.Equal(2d,  matrix[0, 2]);
        Assert.Equal(5d,  matrix[1, 1]);
        Assert.Equal(10d, matrix[2, 2]);
    }   

    [Fact]
    public void Matrix5x5()
    {
        var matrix = FromText(
            """
            [
                [  0,   0,  -1,   0,   0 ],
                [  0,  -1,  -2,  -1,   0 ],
                [ -1,  -2,  16,  -2,  -1 ],
                [  0,  -1,  -2,  -1,   0 ],
                [  0,   0,  -1,   0,   0 ]
            ]
            """);

        Assert.Equal(5,  matrix.RowCount);
        Assert.Equal(5,  matrix.ColumnCount);
        Assert.Equal(25, matrix.ElementCount);

        Assert.Equal(0d,  matrix[0, 0]);
        Assert.Equal(-1d, matrix[2, 0]);
        Assert.Equal(-2d, matrix[2, 1]);
        Assert.Equal(16d, matrix[2, 2]);

        var r = matrix * 5;

        Assert.Equal(0d, r[1, 0]);
        Assert.Equal(-5d, r[1, 1]);
        Assert.Equal(-10d, r[1, 2]);
    }
}