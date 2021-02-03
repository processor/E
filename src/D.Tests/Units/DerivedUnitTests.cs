using Xunit;

namespace E.Units.Tests
{
    public class DerivedUnitTests
    {
        [Theory]
        [InlineData(Dimension.Frequency, "Hz")]
        [InlineData(Dimension.ElectricResistance, "Ω")]
        [InlineData(Dimension.Force, "N")]
        [InlineData(Dimension.Power, "W")]
        [InlineData(Dimension.Pressure, "Pa")]
        [InlineData(Dimension.Energy, "J")]
        [InlineData(Dimension.ElectricCharge, "C")]
        [InlineData(Dimension.ElectricPotentialDifference, "V")]
        [InlineData(Dimension.Capacitance, "F")]
        [InlineData(Dimension.ElectricConductance, "S")]
        public void DerivedTypes(Dimension id, string text)
        {
            Assert.True(UnitInfo.TryParse(text, out UnitInfo type));

            Assert.Equal(id, type.Dimension);
            Assert.Equal(1, type.DefinitionValue);
        }
    }
}