using Xunit;

namespace D.Units.Tests
{

    public class BaseUnitTypeTests
    {
        [Fact]
        public void Equality()
        {
            var a = new UnitType("a");
            var b = new UnitType("b");
            
            Assert.False(a.Equals(b));
            Assert.True(a.Equals(new UnitType("a")));

        }

        [Fact]
        public void Q()
        {
            var value = UnitValue.Parse("3 m³");

            Assert.Equal("3m³", value.ToString());
        }

        [Fact]
        public void CubeMeters()
        {
            var unit = new UnitType("m", 3);

            Assert.Equal("m³", unit.ToString());
        }

        [Fact]
        public void X()
        {
            UnitType.TryParse("g", out UnitType type);

            Assert.Equal("g", type.Name);
            Assert.Equal(1,   type.BaseFactor);
            Assert.Equal(1,   type.Prefix.Value);
            Assert.Equal(1,   type.Exponent);
        }

        [Theory]
        [InlineData("mg", .001d,    1000d)]
        [InlineData("g",   1d,      1d)]
        [InlineData("kg",  1000d,   .001d)]
        [InlineData("lb",  453.592d, 0.00220462d)]
        public void MassConversions(string s, double v1, double v2)
        {
            var unit = UnitValue.Parse(s).With(1);

            var baseUnit = UnitValue.Create(1, UnitType.Ampere);

            Assert.Equal(v1, unit.To(baseUnit));
            // Assert.Equal(v2, unit.From(baseUnit), 5);
        }


        [Fact]
        public void S()
        {
            Assert.Equal(10/60d, UnitValue.Create(10, UnitType.Second).To(UnitType.Minute));
            Assert.Equal(1/60d,  UnitValue.Create(1,  UnitType.Minute).To(UnitType.Hour));
            Assert.Equal(60d,    UnitValue.Create(1,  UnitType.Minute).To(UnitType.Second));
            Assert.Equal(120d,   UnitValue.Create(2,  UnitType.Hour).To(UnitType.Minute));
        }

        [Fact]
        public void A()
        {
            var kg = UnitType.Gram.WithPrefix(SIPrefix.k); // kg

            var g_1000 = UnitValue.Create(1000, UnitType.Gram);
            var g_500 =  UnitValue.Create(500, UnitType.Gram);
            var g_100 =  UnitValue.Parse("g").With(100);

            Assert.Equal(1,   g_1000.To(kg));
            Assert.Equal(0.5, g_500.To(kg));
            Assert.Equal(0.1, g_100.To(kg));
        }

        [Fact]
        public void Parse()
        {
            var unit = UnitValue.Parse("kg").With(1);

            Assert.Equal(1000d, unit.Type.Prefix.Value);
            Assert.Equal("1kg", unit.ToString());
            Assert.Equal(1, unit.Quantity);
            Assert.Equal(1, unit.Type.Exponent);
        }

        [InlineData("deg")]
        [InlineData("px")]
        [InlineData("%")]
        public void OtherTests(string text)
        {
            var result = UnitType.TryParse(text, out UnitType type);

            Assert.True(result);
            Assert.Equal(text, type.Name);
        }


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
            UnitType.TryParse(text, out UnitType type);

            Assert.Equal(id, type.BaseUnit);
            Assert.True(type.IsBaseUnit);

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

