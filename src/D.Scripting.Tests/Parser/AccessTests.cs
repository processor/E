using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class AccessTests : TestBase
    {
        [Fact]
        public void Assignment()
        {
            var declaration = Parse<VariableDeclaration>(@"
let mutable n = data.length
let quant = Color[255]");

            Assert.True(declaration.IsMutable);

            Assert.Equal("n", declaration.Name);            
        }

        [Fact]
        public void Assignment2()
        {
            var declaration = Parse<VariableDeclaration>(@"
var quant = Color[255]");

            Assert.True(declaration.IsMutable);
        }

        [Fact]
        public void Call()
        {
            var a = Parse<CallExpression>("tree[500].bananas.pick()");

            Assert.Equal("pick", a.FunctionName);
            Assert.Equal(0, a.Arguments.Count);
        }

        [Fact]
        public void Multilevel()
        {
            var a = Parse<MemberAccessExpression>("tree[500].bananas");
            var b = (IndexAccessExpression)a.Left;

            Assert.Equal("tree",    b.Left.ToString());
            Assert.Equal(500,       (Integer)b.Arguments[0]);
            Assert.Equal("bananas", a.MemberName);
        }

        [Fact]
        public void IndexLevel1()
        {
            var statement = Parse<IndexAccessExpression>("members[0]");

            Assert.Equal(0, (Integer)statement.Arguments[0]);
        }

        [Fact]
        public void IndexLevel1MultiArg()
        {
            var statement = Parse<IndexAccessExpression>("members[0, 1]");

            Assert.Equal(0, (Integer)statement.Arguments[0]);
            Assert.Equal(1, (Integer)statement.Arguments[1]);
        }

        [Fact]
        public void IndexLevel2()
        {
            var statement = Parse<IndexAccessExpression>("matrix[0][3]");

            var left = (IndexAccessExpression)statement.Left;

            Assert.Equal(0L, (Integer)left.Arguments[0]);
            Assert.Equal(3L, (Integer)statement.Arguments[0]);
        }

        [Fact]
        public void Interleaved10()
        {
            var alphabet = new[] { "_", "a", "b", "c", "d", "e", "f", "g", "h", "i" };

            var statement = Parse<IndexAccessExpression>("matrix[0].a[1].b[2].c[3].d[4].e[5].f[6].g[7].h[8].i[9]");

            IndexAccessExpression left = statement;

            for (var i = 9; i > 0; i--)
            {
                Assert.Equal(i, (Integer)left.Arguments[0]);

                var a = (MemberAccessExpression)left.Left;

                Assert.Equal(alphabet[i], a.MemberName);

                left = (IndexAccessExpression)a.Left;
            }

            Assert.Equal("matrix", (Symbol)left.Left);
        }

        [Fact]
        public void IndexLevel10()
        {
            var statement = Parse<IndexAccessExpression>("matrix[0][1][2][3][4][5][6][7][8][9]");

            IndexAccessExpression left = statement;

            for (var i = 9; i > 0; i--)
            {
                Assert.Equal(i, (Integer)left.Arguments[0]);

                left = (IndexAccessExpression)left.Left;
            }

            Assert.Equal("matrix", (Symbol)left.Left);        
        }
    }
}