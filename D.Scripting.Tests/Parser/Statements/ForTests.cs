using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class ForTests : TestBase
    {
        [Fact]
        public void ForPattern()
        {
            var statements = new Parser(@"
let points = [ (0, 1), (2, 3) ]

var sum: i64 = 0

for (x, y) in points {
  sum += x
  sum += y
}");

            var pointsVar    = statements.Next() as VariableDeclaration;
            var sumVar       = statements.Next() as VariableDeclaration;
            var forStatement = (ForStatement)statements.Next();

            var pattern = forStatement.VariableExpression as TuplePattern;

            Assert.Equal("x",   pattern.Variables[0].Name);
            Assert.Equal("y",   pattern.Variables[1].Name);

            Assert.Equal("points", forStatement.GeneratorExpression.ToString());
        }

        [Fact]
        public void ForAny()
        {
            var f = Parse<ForStatement>(@"
for _ in 0...100 {
  a = a + 1
}");

            Assert.True(f.VariableExpression is AnyPattern);
            Assert.True(f.GeneratorExpression  is RangeExpression);
        }

        [Fact]
        public void ForRange()
        {
            var f = Parse<ForStatement>(@"
for 0 ... i8.max {
  a = a + 1
}");

            var sequence = f.GeneratorExpression as RangeExpression;

            Assert.Null(f.VariableExpression);
            Assert.True(sequence.End is MemberAccessExpression);
        }

        [Fact]
        public void For_X_In_Range()
        {
            var f = Parse<ForStatement>(@"
for x in 0...100 {
  a = a + 1
}");

            Assert.Equal("x", f.VariableExpression.ToString());
            Assert.True(f.GeneratorExpression is RangeExpression);
        }

        [Fact]
        public void For_X_In_Dataset()
        {
            var f = Parse<ForStatement>(@"
for crime in Crimes {
  a = a + 1
}");

            Assert.Equal("crime", f.VariableExpression.ToString());
            Assert.Equal("Crimes", f.GeneratorExpression.ToString());
        }


        [Fact]
        public void ForCollection()
        {
            var f = Parse<ForStatement>(@"
for criminology::Crimes {
  a = a + 1
}");

            var generator = (Symbol)f.GeneratorExpression;

            Assert.Equal("criminology", generator.Domain);
            Assert.Equal("Crimes",      generator.Name);
        }
    }
}
