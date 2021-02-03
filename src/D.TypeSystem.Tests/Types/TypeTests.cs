using Xunit;

namespace E.TypeSystem.Tests
{
    public class TypeTests
    {
        [Fact]
        public void A()
        {
            Assert.Equal(Type.Get(ObjectType.Object), Type.Get(ObjectType.Int32).BaseType);
        }

        [Fact]
        public void Names()
        {
            Assert.Equal("Object", Type.Get(ObjectType.Object).Name);
        }
    }
}