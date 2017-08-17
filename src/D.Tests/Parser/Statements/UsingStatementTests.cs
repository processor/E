using Xunit;

namespace D.Parsing.Tests
{
    using Syntax;

    public class UsingStatementTests : TestBase
    {
        [Fact]
        public void SingleUsing()
        {
            var statement = Parse<UsingStatement>(@"using imaging");
           
            Assert.Equal("imaging", statement.Domains[0].Name);
        }

        [Fact]
        public void MultipleUsings()
        {
            var statement = Parse<UsingStatement>(@"using accounting, finance, taxation;");

            Assert.Equal("accounting", statement[0]);
            Assert.Equal("finance",    statement[1]);
            Assert.Equal("taxation",   statement[2]);
        }
    }
}