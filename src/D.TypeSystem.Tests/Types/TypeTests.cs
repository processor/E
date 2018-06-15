using Xunit;

namespace D.TypeSystem.Tests
{
    public class TypeTests
    {
        [Fact]
        public void A()
        {
            Assert.Equal(Type.Get(Kind.Object), Type.Get(Kind.Int32).BaseType);
        }

        [Fact]
        public void Names()
        {
            Assert.Equal("Object", Type.Get(Kind.Object).Name);
        }
    }
}