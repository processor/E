using Xunit;

namespace D.Language.Tests
{
    public class TypeTests
    {
        [Fact]
        public void A()
        {
            Assert.Equal("Map<String,Integer>", new Type(Kind.Map, new Type(Kind.String), new Type(Kind.Integer)).ToString());
            Assert.Equal("Set<String>",         new Type(Kind.Set, new Type(Kind.String)).ToString());
            Assert.Equal("List<String>",        new Type(Kind.List, new Type(Kind.String)).ToString());
            Assert.Equal("String",              new Type(Kind.String).ToString());
        }
    }
}
