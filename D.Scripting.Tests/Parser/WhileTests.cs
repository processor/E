using Xunit;

namespace D.Parsing.Tests
{
    using Expressions;

    public class BlockTests : TestBase
    {
        [Fact]
        public void While()
        {
            var statement = Parse<WhileStatement>(@"
while a > 1 {
  a = a + 1
}");
        }
    }
}