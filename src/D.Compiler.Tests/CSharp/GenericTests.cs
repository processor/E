using D.Expressions;
using D.Parsing;
using D.Syntax;

using Xunit;

namespace D.Compilier.Tests
{
    public class GenericTests
    {

        [Fact]
        public void X()
        {
            var text = @"
let matrix = [
    [ [ 0, 4, 6, 2 ], [ -1,  0,  0 ] ],
    [ [ 1, 3, 7, 5 ], [ +1,  0,  0 ] ],
    [ [ 0, 1, 5, 4 ], [  0, -1,  0 ] ],
    [ [ 2, 6, 7, 3 ], [  0, +1,  0 ] ],
    [ [ 0, 2, 3, 1 ], [  0,  0, -1 ] ],
    [ [ 4, 5, 7, 6 ], [  0,  0, +1 ] ]
]";

            var def = (VariableDeclarationSyntax)new Parser(text).Next();

            Assert.Equal("matrix", def.Name);

            var compiler = new Compilation.Compiler();

            var declaration = (VariableDeclaration)compiler.Visit(def);

            Assert.Equal("matrix", declaration.Name);

        }
    }
}
