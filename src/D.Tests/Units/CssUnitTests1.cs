using Xunit;

namespace D.Units.Tests
{
    public class CssUnitTests
    {
        [Theory]
        [InlineData(Dimension.Length, "px")]
        public void DerivedTypes(Dimension id, string text)
        {
            UnitInfo.TryParse(text, out UnitInfo? type);

            Assert.Equal(id, type.Dimension);
            Assert.Equal(1, type.DefinitionValue);
        }
    }
}

