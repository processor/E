using Xunit;

namespace D.Units.Tests
{
    public class DerivedUnitTests
    {
        // [Theory]
        [InlineData(UnitId.Frequency, "Hz")]
        [InlineData(UnitId.ElectricResistance, "Ω")]
        [InlineData(UnitId.Force, "N")]
        [InlineData(UnitId.Power, "W")]
        [InlineData(UnitId.Pressure, "Pa")]
        [InlineData(UnitId.Energy, "J")]
        [InlineData(UnitId.ElectricCharge, "C")]
        [InlineData(UnitId.ElectricPotentialDifference, "V")]
        [InlineData(UnitId.Capacitance, "F")]
        [InlineData(UnitId.ElectricConductance, "S")]
        public void DerivedTypes(UnitId id, string text)
        {
            UnitType.TryParse(text, out UnitType type);

            Assert.Equal(id, type.BaseUnit);
            Assert.Equal(1, type.BaseFactor);
        }
    }
}

