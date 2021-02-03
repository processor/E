using Xunit;

namespace E.Inference.Tests
{
    public class TypeTests
    {
        [Fact]
        public void Q()
        {
            var flow = new Flow();

            var g = TypeSystem.NewGeneric();

            var num = TypeSystem.NewType("Number", new[] { g }); // Number<T>

            var complex = TypeSystem.NewType(num, "Complex", new[] { g }); // Number<T>

            // Assert.Equal("Complex<`af>", complex.ToString());

            Assert.Equal(num, complex.BaseType);

            // throw new System.Exception(complex.ToString());
        }
    }
}
