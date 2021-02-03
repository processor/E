using E.Mathematics;

using Xunit;

namespace E.Tests
{
    public class CustomOperatorTests
    {
        [Fact]
        public void Multiply()
        {
            var env = new Node();

            env.Operators.Add(Operator.Infix(ObjectType.MultiplyExpression, "×", precedence: 14));

            env.Add("×", new ArithmethicFunction("multiply", Arithmetic.Multiply));

            Assert.Equal("0",     Script.Evaluate("0 × 10",    env).ToString());
            Assert.Equal("1",     Script.Evaluate("1 × 1",     env).ToString());
            Assert.Equal("100m²", Script.Evaluate("10m × 10m", env).ToString());
        }
    }
}