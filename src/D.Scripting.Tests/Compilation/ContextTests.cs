using Xunit;

namespace D.Compilation.Tests
{
    using Expressions;
    using Parsing.Tests;

    public class ContextTests : TestBase
    {
        [Fact]
        public void PrimaryContext() 
        {
            var context = new Scope();

            context.Add("a", new StringLiteral("a"));
            context.Add("b", new Integer(1));

            Assert.Equal("Any", context.Get<Type>(Symbol.Any).Name);

            Assert.Equal("a", context.Get<StringLiteral>(Symbol.Local("a")));
            Assert.Equal(1,   context.Get<Integer>(Symbol.Local("b")));
        }

        [Fact]
        public void ChildContext()
        {
            var context = new Scope();

            context.Add("name", new StringLiteral("name"));

            var child = context.Nested();

            Assert.Equal("name", child.Get<StringLiteral>(Symbol.Local("name")));
            Assert.Equal("Any",  child.Get<Type>(Symbol.Any).Name);

        }
    }
}
