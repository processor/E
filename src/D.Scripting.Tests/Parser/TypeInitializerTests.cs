using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class TypeInitializerTests : TestBase
    {
        [Fact]
        public void Nested()
        {
            var type = Parse<TypeInitializerSyntax>(@"

Account {
  balance : 100,
  owner   : ""me"",
  created : Date { year: 2000, month: 01, day: 01 }
}

");


            Assert.Equal("Account", type.Type);

            Assert.Equal(3, type.Count);

            Assert.Equal("Date", ((TypeInitializerSyntax)type.Members[2].Value).Type);
        }
        [Fact]
        public void RootScoped()
        {
            var type = Parse<TypeInitializerSyntax>(@"
Point {
  x: 1,
  y: 2,
  z: 3 
}");
            Assert.Equal("Point", type.Type.Name);
            Assert.Equal(3, type.Members.Length);
        }

        [Fact]
        public void Let()
        {
            var let = Parse<VariableDeclarationSyntax>("let zero = Point { x: 0, y: 0, z: 0 };");

            var value = (TypeInitializerSyntax)let.Value;

            Assert.Equal("Point", value.Type.Name);
        }

        [Fact]
        public void BlockScoped()
        {
            var ifS = Parse<IfStatementSyntax>(@"
if 1 + 1 == 3 {
  return Point {
    x: 1 + 1,
    y: 2 * 1,
    z: 3 / 1,
  }
}");

            var r = (ReturnStatementSyntax)ifS.Body.Statements[0];

            var type = (TypeInitializerSyntax)r.Expression;

            Assert.Equal("Point", type.Type.Name);

            Assert.Equal(3, type.Members.Length);

            Assert.Equal("x", type.Members[0].Name);
            Assert.Equal("y", type.Members[1].Name);
            Assert.Equal("z", type.Members[2].Name);
        }
    }
}
