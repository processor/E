using Xunit;

namespace D.Units.Tests
{
    public class UnitTypeTests
    {
        [Fact]
        public void X()
        {
            Unit<int> unit;

            Unit<int>.TryParse("g", out unit);

            Assert.Equal("g", unit.Type.Name);
            Assert.Equal(1, unit.Type.BaseFactor);
            Assert.Equal(1, unit.Prefix.Value);
            Assert.Equal(1, unit.Quantity);
            Assert.Equal(1, unit.Power);
        }

        [Theory]
        [InlineData("mg", .001d, 1000d)]
        [InlineData("g",   1d, 1d)]
        [InlineData("kg",  1000d, .001d)]
        [InlineData("lb",  453.592d, 0.00220462d)]
        public void MassConversions(string s, double v1, double v2)
        {
            var unit = Unit.Parse(s);

            var baseUnit = Unit.Create(1, UnitType.Ampere);

            Assert.Equal(v1, unit.To(baseUnit));
            // Assert.Equal(v2, unit.From(baseUnit), 5);
        }


        [Fact]
        public void S()
        {
            Assert.Equal(10/60d, Unit.Create(10, UnitType.Second).To(UnitType.Minute));
            Assert.Equal(1/60d,  Unit.Create(1,  UnitType.Minute).To(UnitType.Hour));
            Assert.Equal(60d,    Unit.Create(1,  UnitType.Minute).To(UnitType.Second));
            Assert.Equal(120d,   Unit.Create(2,  UnitType.Hour).To(UnitType.Minute));
        }

        [Fact]
        public void A()
        {
            var kg = new Unit<int>(SIPrefix.k, UnitType.Gram);

            var g_1000 = Unit.Create(1000, UnitType.Gram);
            var g_500 =  Unit.Create(500, UnitType.Gram);
            var g_100 =  Unit.Parse("g").With(100);

            Assert.Equal(1,   g_1000.To(kg));
            Assert.Equal(0.5, g_500.To(kg));
            Assert.Equal(0.1, g_100.To(kg));
        }



        [Fact]
        public void Parse()
        {
            var unit = Unit.Parse("kg");

            Assert.Equal(1000d, unit.Prefix.Value);
            Assert.Equal("1kg", unit.ToString());
            Assert.Equal(1, unit.Quantity);
            Assert.Equal(1, unit.Power);
        }

        [InlineData("deg")]
        [InlineData("px")]
        [InlineData("%")]
        public void Randoms(string text)
        {
            UnitType type;

            var result = UnitType.TryParse(text, out type);

            Assert.True(result);
            Assert.Equal(text, type.Name);
        }


        [Theory]

        /*
        // Time
        [InlineData(SIPrefix.Yocto,  UnitId.Time, "ys")]
        [InlineData(SIPrefix.Zepto,  UnitId.Time, "zs")]
        [InlineData(SIPrefix.Atto,   UnitId.Time, "as")]
        [InlineData(SIPrefix.Femto,  UnitId.Time, "fs")]
        [InlineData(SIPrefix.Pico,   UnitId.Time, "ps")]
        [InlineData(SIPrefix.Nano,   UnitId.Time, "ns")]
        [InlineData(SIPrefix.Micro,  UnitId.Time, "μs")]
        [InlineData(SIPrefix.Milli,  UnitId.Time, "ms")]
        [InlineData(SIPrefix.Centi,  UnitId.Time, "cs")]
        [InlineData(SIPrefix.Deci,   UnitId.Time, "ds")]
        [InlineData(SIPrefix.Deca,   UnitId.Time, "das")]
        [InlineData(SIPrefix.Hecto,  UnitId.Time, "hs")]
        [InlineData(SIPrefix.Kilo,   UnitId.Time, "ks")]
        [InlineData(SIPrefix.Mega,   UnitId.Time, "Ms")]
        [InlineData(SIPrefix.Giga,   UnitId.Time, "Gs")]
        [InlineData(SIPrefix.Tera,   UnitId.Time, "Ts")]
        [InlineData(SIPrefix.Peta,   UnitId.Time, "Ps")]
        [InlineData(SIPrefix.Exa,    UnitId.Time, "Es")]
        [InlineData(SIPrefix.Zetta,  UnitId.Time, "Zs")]
        [InlineData(SIPrefix.Yotta,  UnitId.Time, "Ys")]
       
        public void ScalesShort(SIScale scale, UnitId type, string text)
        {
            var kind = UnitType.Parse(text);

            Assert.Equal(type, kind.BaseUnit);
            Assert.Equal(scale, kind.Scale);
        }
        */

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
            UnitType type;

            UnitType.TryParse(text, out type);

            Assert.Equal(id, type.BaseUnit);
            Assert.Equal(1, type.BaseFactor);
        }


        [Theory]
        [InlineData(UnitId.Time, "s", "second")]
        [InlineData(UnitId.ThermodynamicTemperature, "K", "kelvin")]
        [InlineData(UnitId.ElectricCurrent, "A", "ampere")]
        [InlineData(UnitId.Length, "m", "meter")]
        // [InlineData(UnitId.Mass, "kg", "kilogram")]
        [InlineData(UnitId.AmountOfSubstance, "mol", null)]
        [InlineData(UnitId.LuminousIntensity, "cd", "candela")]
        public void BaseTypes(UnitId id, string text, string text2)
        {
            UnitType type;

            UnitType.TryParse(text, out type);

            Assert.Equal(id, type.BaseUnit);
            Assert.Equal(true, type.IsBaseUnit);

            /*
            if (text2 != null)
            {
                var kind2 = UnitType.Parse(text2);

                Assert.Same(kind, kind2);
            }
            */

        }

        /*
        // Volumes
        [Theory]
        // [InlineData(BaseUnitId.Length, BaseUnitId.Time, "m/s²")]
        // [InlineData(BaseUnitId.Length, BaseUnitId.Time, "m/s³")]

        // [InlineData(BaseUnitId.Angle, BaseUnitId.Time, "rad/s")] // angular velocity
        // [InlineData(BaseUnitId.Angle, BaseUnitId.Time, "rad/s²")] // angular accelaration

        // 	m³/s        // volumnetric flow

        public void MultidimensionalTypes(BaseUnitId type, BaseUnitId dimesion, string text)
        {
            var kind = UnitType.Parse(text);

            Assert.Equal(type, kind.Type);
            Assert.Equal(dimesion, kind.Dimension.Type);
        }
        */

        /*
        [InlineData(SIScale.Kilo, UnitId.Force, "kN")]
        [InlineData(SIScale._, UnitId.ElectricResistance, "ohm")]
        [InlineData(SIScale.Kilo, UnitId.ElectricResistance, "kiloohm")]

        public void NewTypes(SIScale scale, UnitId type, string text)
        {
            var kind = UnitType.Parse(text);

            Assert.Equal(type, kind.BaseUnit);
            Assert.Equal(scale, kind.Scale);
        }
        */
    }
}

