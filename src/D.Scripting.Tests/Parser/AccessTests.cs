using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class AccessTests : TestBase
    {
        [Fact]
        public void Assignment()
        {
            var declaration = Parse<VariableDeclarationSyntax>(@"
let mutable n = data.length
let quant = Color[255]");

            Assert.True(declaration.Flags.HasFlag(VariableFlags.Mutable));

            Assert.Equal("n", declaration.Name);            
        }

        [Fact]
        public void Assignment2()
        {
            var declaration = Parse<VariableDeclarationSyntax>(@"
var quant = Color[255]");

            Assert.True(declaration.Flags.HasFlag(VariableFlags.Mutable));
        }

        [Fact]
        public void Call()
        {
            var a = Parse<CallExpressionSyntax>("tree[500].bananas.pick()");

            Assert.Equal("pick", a.Name);
            Assert.Equal(0, a.Arguments.Length);
        }

        [Fact]
        public void Multilevel()
        {
            var a = Parse<MemberAccessExpressionSyntax>("tree[500].bananas");
            var b = (IndexAccessExpressionSyntax)a.Left;

            Assert.Equal("tree",    b.Left.ToString());
            Assert.Equal(500,       (NumberLiteralSyntax)b.Arguments[0].Value);
            Assert.Equal("bananas", a.Name);
        }

        [Fact]
        public void IndexLevel1()
        {
            var statement = Parse<IndexAccessExpressionSyntax>("members[0]");

            Assert.Equal(0, (NumberLiteralSyntax)statement.Arguments[0].Value);
        }

        [Fact]
        public void IndexLevel1MultiArg()
        {
            var statement = Parse<IndexAccessExpressionSyntax>("members[0, 1]");

            Assert.Equal(0, (NumberLiteralSyntax)statement.Arguments[0].Value);
            Assert.Equal(1, (NumberLiteralSyntax)statement.Arguments[1].Value);
        }

        [Fact]
        public void IndexLevel2()
        {
            var statement = Parse<IndexAccessExpressionSyntax>("matrix[0][3]");

            var left = (IndexAccessExpressionSyntax)statement.Left;

            Assert.Equal(0L, (NumberLiteralSyntax)left.Arguments[0].Value);
            Assert.Equal(3L, (NumberLiteralSyntax)statement.Arguments[0].Value);
        }

        [Fact]
        public void Interleaved10()
        {
            var alphabet = new[] { "_", "a", "b", "c", "d", "e", "f", "g", "h", "i" };

            var statement = Parse<IndexAccessExpressionSyntax>("matrix[0].a[1].b[2].c[3].d[4].e[5].f[6].g[7].h[8].i[9]");

            IndexAccessExpressionSyntax left = statement;

            for (var i = 9; i > 0; i--)
            {
                Assert.Equal(i, (NumberLiteralSyntax)left.Arguments[0].Value);

                var a = (MemberAccessExpressionSyntax)left.Left;

                Assert.Equal(alphabet[i], a.Name);

                left = (IndexAccessExpressionSyntax)a.Left;
            }

            Assert.Equal("matrix", (Symbol)left.Left);
        }

        [Fact]
        public void IndexLevel10()
        {
            var statement = Parse<IndexAccessExpressionSyntax>("matrix[0][1][2][3][4][5][6][7][8][9]");

            IndexAccessExpressionSyntax left = statement;

            for (var i = 9; i > 0; i--)
            {
                Assert.Equal(i, (NumberLiteralSyntax)left.Arguments[0].Value);

                left = (IndexAccessExpressionSyntax)left.Left;
            }

            Assert.Equal("matrix", (Symbol)left.Left);        
        }
    }
}