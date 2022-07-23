using E.Expressions;
using E.Parsing;
using E.Syntax;

namespace E.Compilation.Tests;

public class GenericTests
{
    [Fact]
    public void X()
    {
        var def = (PropertyDeclarationSyntax)new Parser(
            """

            let matrix = [
              [ [ 0, 4, 6, 2 ], [ -1,  0,  0 ] ],
              [ [ 1, 3, 7, 5 ], [ +1,  0,  0 ] ],
              [ [ 0, 1, 5, 4 ], [  0, -1,  0 ] ],
              [ [ 2, 6, 7, 3 ], [  0, +1,  0 ] ],
              [ [ 0, 2, 3, 1 ], [  0,  0, -1 ] ],
              [ [ 4, 5, 7, 6 ], [  0,  0, +1 ] ]
            ]
            """).Next();

        Assert.Equal("matrix", def.Name);

        var compiler = new Compiler();

        var declaration = (VariableDeclaration)compiler.Visit(def);

        Assert.Equal("matrix", declaration.Name);

    }
}
