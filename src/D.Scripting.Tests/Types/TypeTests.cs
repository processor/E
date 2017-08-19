using Xunit;

namespace D.Language.Tests
{
    public class TypeTests
    {
        [Fact]
        public void A()
        {
            Assert.Equal("Map<String,Int64>", new Type(Kind.Map, new Type(Kind.String), new Type(Kind.Int64)).ToString());
            Assert.Equal("Set<String>",       new Type(Kind.Set, new Type(Kind.String)).ToString());
            Assert.Equal("Array<String>",     new Type(Kind.Array, new Type(Kind.String)).ToString());
            Assert.Equal("String",            new Type(Kind.String).ToString());
        }
    }
}
