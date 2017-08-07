using Xunit;

namespace D.Graph.Tests
{
    using Expressions;
    using Parsing.Tests;

    public class GraphTests : TestBase
    {
        [Fact]
        public void Root() 
        {
            var context = new Node();

            context.Add("a", new StringLiteral("a"));
            context.Add("b", new Integer(1));

            Assert.Equal("Any", context.Get<Type>(TypeSymbol.Any).Name);

            Assert.Equal("a", context.Get<StringLiteral>(Symbol.Variable("a")));
            Assert.Equal(1,   context.Get<Integer>(Symbol.Variable("b")));
        }

        [Fact]
        public void Child()
        {
            var context = new Node();

            context.Add("name", new StringLiteral("name"));

            var child = context.Nested("child");

            Assert.Equal("name", child.Get<StringLiteral>(Symbol.Variable("name")));
            Assert.Equal("Any",  child.Get<Type>(TypeSymbol.Any).Name);

        }
    }
}
