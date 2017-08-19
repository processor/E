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

            Assert.Equal("Object", context.Get<Type>(TypeSymbol.Object).Name);

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
            Assert.Equal("Object",  child.Get<Type>(TypeSymbol.Object).Name);

        }
    }
}
