namespace E.Units.Tests;

public class BaseUnitTypeTests
{
    [Fact]
    public void Equality()
    {
        var a = new UnitInfo("a");
        var b = new UnitInfo("b");

        Assert.False(a.Equals(b));
        Assert.True(a.Equals(new UnitInfo("a")));
    }

    [Fact]
    public void CubeMeters()
    {
        var unit = new UnitInfo("m", 3);

        Assert.Equal("m³", unit.ToString());
        Assert.Equal("m³", $"{unit}");
    }

    [Fact]
    public void CanFindGramUnit()
    {
        Assert.True(UnitInfo.TryParse("g", out UnitInfo gUnit));

        Assert.Equal(41803, gUnit.Id);
        Assert.Equal("g", gUnit.Name);
        Assert.Equal(1, gUnit.DefinitionValue);
        Assert.Equal(1, gUnit.Prefix.Value);
        Assert.Equal(1, gUnit.Power);

        Assert.Equal("g", gUnit.ToString());
        Assert.Equal("g", $"{gUnit}");

        Assert.Same(UnitSet.Default.Find(41803), gUnit);
    }

    [Theory]
    [InlineData("mg", .001d, 1000d)]
    [InlineData("g", 1d, 1d)]
    [InlineData("kg", 1000d, .001d)]
    [InlineData("lb", 453.592d, 0.00220462d)]
    public void MassConversions(string s, double v1, double v2)
    {
        var unit = Quantity.Parse(s).With(1);

        Assert.Equal(v1, unit.To(UnitInfo.Gram));

        // Assert.Equal(v2, unit.From(baseUnit), 5);
    }

    [Fact]
    public void S()
    {
        Assert.Equal(10 / 60d, Quantity.Create(10d, UnitInfo.Second).To(UnitInfo.Minute));
        Assert.Equal(1 / 60d,  Quantity.Create(1d,  UnitInfo.Minute).To(UnitInfo.Hour));
        Assert.Equal(60d,      Quantity.Create(1d,  UnitInfo.Minute).To(UnitInfo.Second));
        Assert.Equal(120d,     Quantity.Create(2d,  UnitInfo.Hour).To(UnitInfo.Minute));
    }

    [Fact]
    public void A()
    {
        var kg = UnitInfo.Gram.WithPrefix(SIPrefix.k); // kg

        var g_1000 = Quantity.Create(1000d, UnitInfo.Gram);
        var g_500 = Quantity.Create(500d, UnitInfo.Gram);
        var g_100 = Quantity.Parse("g").With(100);

        Assert.Equal(1, g_1000.To(kg));
        Assert.Equal(0.5, g_500.To(kg));
        Assert.Equal(0.1, g_100.To(kg));
    }

    [Fact]
    public void Parse()
    {
        var unit = Quantity.Parse("kg").With(1);

        Assert.Equal(1000d, unit.Unit.Prefix.Value);
        Assert.Equal("1kg", unit.ToString());
        Assert.Equal(1, unit.Value);
        Assert.Equal(1, unit.Unit.Power);
    }

    [Theory]
    [InlineData("deg")]
    [InlineData("px")]
    [InlineData("%")]
    public void OtherTests(string text)
    {
        var result = UnitInfo.TryParse(text, out UnitInfo type);

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
    [InlineData(Dimension.Time, "s")] // second
    [InlineData(Dimension.ThermodynamicTemperature, "K")] // kelvin
    [InlineData(Dimension.ElectricCurrent, "A")] // ampere
    [InlineData(Dimension.Length, "m")] // meter
  //  [InlineData(Dimension.Mass, "kg", "kilogram")]
    [InlineData(Dimension.AmountOfSubstance, "mol")]
    [InlineData(Dimension.LuminousIntensity, "cd")] // candela
    public void BaseTypes(Dimension id, string text)
    {
        UnitSet.Default.AddRange(new ElectromagnetismUnitSet());

        UnitInfo.TryParse(text, out UnitInfo type);

        Assert.Equal(id, type.Dimension);
        Assert.True(type.IsBaseUnit);
        Assert.True(type.IsMetric); // HasSI

        /*
        if (text2 is not null)
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

    // [InlineData(BaseUnitId.Angle, BaseUnitId.Time, "rad/s")]  // angular velocity
    // [InlineData(BaseUnitId.Angle, BaseUnitId.Time, "rad/s²")] // angular acceleration

    // 	m³/s        // volumetric flow

    public void MultidimensionalTypes(BaseUnitId type, BaseUnitId dimension, string text)
    {
        var kind = UnitType.Parse(text);

        Assert.Equal(type, kind.Type);
        Assert.Equal(dimension, kind.Dimension.Type);
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

