using Xunit;

namespace E.Parsing.Tests
{
    using Syntax;

    public class BlockTests : TestBase
    {
        [Fact]
        public void While()
        {
            var statement = Parse<WhileStatementSyntax>(@"
while a > 1 {
  a = a + 1
}");
        }
    }
}