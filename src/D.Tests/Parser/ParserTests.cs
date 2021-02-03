using System.Linq;

using Xunit;

namespace E.Parsing.Tests
{
    using Syntax;

    public class ParserTests : TestBase
    {
        [Fact]
        public void Semicolons()
        {
            var statements = new Parser(@"
let a = 3;
let b = (a, b);
let c = (100, ""fox"");
let d = (a: 1, b: 2, c: 3);
let e = (a: 100, b: ""fox"");
d.v = 15
").Enumerate().ToArray();

            var s5 = (BinaryExpressionSyntax)statements[5];

            var l = (MemberAccessExpressionSyntax)s5.Left;

            Assert.Equal("d", l.Left.ToString());
            Assert.Equal("v", l.Name);

            Assert.Equal(15, (NumberLiteralSyntax)s5.Right);

            Assert.Equal(6, statements.Length);
        }       

        /*
        [Fact]
        public void Predicates()
        {
            // For instance, {x | x is a positive integer less than 4} is the set {1,2,3}.

            var predicate = (Predicate)Script.Evaluate("y > 100");

            Assert.Equal("y", predicate.Left.Name);
            Assert.Equal(Operator.GreaterThan, predicate.Operator);
            Assert.Equal(100, (Integer)predicate.Right);

            Assert.Equal("y > 100", predicate.ToString());
        }
        */


        [Fact]
        public void Read9()
        {
            var statements = new Parser(
                @"a = 1
                  b = 2
                
                  image |> resize 100px 100px").Enumerate().ToArray();

            // Assert.Equal(3, statements.Length);
        }
        

        [Fact]
        public void Trailing()
        {
            var parser = new Parser(
@"a = 1
b = 2
c ");
            var statements = parser.Enumerate().ToArray();

            Assert.Equal(3, statements.Length);
        }

        [Fact]
        public void SpreadTests()
        {
            var spread = Parse<SpreadExpressionSyntax>(@"...r");

            Assert.Equal("r", spread.Expression.ToString());
        }
    }
}
