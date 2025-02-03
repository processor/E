namespace E.Units.Tests;

public class BaseUnitTypeTests
{
    [Fact]
    public void Equality()
    {
        var a = UnitInfo.Create("a");
        var b = UnitInfo.Create("b");

        Assert.False(a.Equals(b));
        Assert.True(a.Equals(a));
    }

    [Fact]
    public void CubicMeters()
    {
        var unit = new UnitInfo(UnitType.Meter, Dimension.Length, "m", exponent: 3);

        Assert.Equal("m³", unit.ToString());
        Assert.Equal("m³", $"{unit}");
    }

    [Fact]
    public void CanFindGramUnit()
    {
        Assert.True(UnitInfo.TryParse("g", out UnitInfo gUnit));

        Assert.Equal(41803, gUnit.Id);
        Assert.Equal("g", gUnit.Name);
        Assert.Equal(1, gUnit.GetBaseUnitConversionFactor<double>());
        Assert.Equal(1, gUnit.Exponent);

        Assert.Equal("g", gUnit.ToString());
        Assert.Equal("g", $"{gUnit}");

        Assert.Same(UnitFactory.Default.Find(41803), gUnit);
    }

    [Theory]
    [InlineData("mg", .001d, 1000d)]
    [InlineData("g",  1d, 1d)]
    [InlineData("kg", 1000d, .001d)]
    [InlineData("lb", 453.592d, 0.00220462d)]
    public void MassConversions(string s, double v1, double v2)
    {
        var unit = Quantity.Parse(s).With(1);

        Assert.Equal(v1, unit.To(UnitInfo.Gram));

        // Assert.Equal(v2, unit.From(baseUnit), 5);
    }

    [Theory]
    [InlineData("1 km",      "1 km")]
    [InlineData("1000 ft",   "0.3048 km")]
    [InlineData("0.3048 km", "1000 ft")]
    public void LengthConversions(string s, string t)
    {
        var source = Quantity<decimal>.Parse(s);
        var target = Quantity<decimal>.Parse(t);

        Assert.Equal(target.Value, source.To(target.Unit));
    }

    [Fact]
    public void S()
    {
        Assert.Equal(10 / 60d, Quantity.Create(10d, TimeUnits.Second).To(TimeUnits.Minute));
        Assert.Equal(1 / 60d,  Quantity.Create(1d,  TimeUnits.Minute).To(TimeUnits.Hour));
        Assert.Equal(60d,      Quantity.Create(1d,  TimeUnits.Minute).To(TimeUnits.Second));
        Assert.Equal(120d,     Quantity.Create(2d,  TimeUnits.Hour).To(TimeUnits.Minute));
    }

    [Fact]
    public void A()
    {
        Assert.True(UnitInfo.TryParse("kg", out var kg));

        var g_1000 = Quantity.Create(1000d, UnitInfo.Gram);
        var g_500  = Quantity.Create(500d, UnitInfo.Gram);
        var g_100  = Quantity.Parse("g").With(100);

        Assert.Equal(1,   g_1000.To(kg));
        Assert.Equal(0.5, g_500.To(kg));
        Assert.Equal(0.1, g_100.To(kg));
    }

    [Fact]
    public void CanCreateScaledMetricType()
    {
        Assert.True(UnitInfo.TryParse("Mg", out var mg));

        Assert.True(UnitInfo.TryParse("Mg", out var mg2));

        Assert.Same(mg, mg2);

        var _1Mg = new Quantity<decimal>(1, mg);

        Assert.Equal("1Mg", _1Mg.ToString());

        Assert.Equal(1_000_000m, _1Mg.To(UnitInfo.Gram));
    }

    [Fact]
    public void Parse()
    {
        var unit = Quantity.Parse("kg").With(1);

        Assert.Equal(1000d, unit.Unit.BaseConverter.Value.Compile<double>()(1));
        Assert.Equal("1kg", unit.ToString());
        Assert.Equal(1, unit.Value);
        Assert.Equal(1, unit.Unit.Exponent);
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


    public class TimeScalesTestData : TheoryData<MetricPrefix, Dimension, string>
    {
        public TimeScalesTestData()
        {
            Add(MetricPrefix.y, Dimension.Time, "ys");
            Add(MetricPrefix.z, Dimension.Time, "zs");
            Add(MetricPrefix.a, Dimension.Time, "as");
            Add(MetricPrefix.f, Dimension.Time, "fs");
            Add(MetricPrefix.p, Dimension.Time, "ps");
            Add(MetricPrefix.n, Dimension.Time, "ns");
            // Add(SIPrefix.µ, Dimension.Time, "μs");
            Add(MetricPrefix.m, Dimension.Time, "ms");
            Add(MetricPrefix.c, Dimension.Time, "cs");
            Add(MetricPrefix.d, Dimension.Time, "ds");
            Add(MetricPrefix.da, Dimension.Time, "das");
            Add(MetricPrefix.h, Dimension.Time, "hs");
            Add(MetricPrefix.k, Dimension.Time, "ks");
            Add(MetricPrefix.M, Dimension.Time, "Ms");
            Add(MetricPrefix.G, Dimension.Time, "Gs");
            Add(MetricPrefix.T, Dimension.Time, "Ts");
            Add(MetricPrefix.P, Dimension.Time, "Ps");
            Add(MetricPrefix.E, Dimension.Time, "Es");
            Add(MetricPrefix.Z, Dimension.Time, "Zs");
            Add(MetricPrefix.Y, Dimension.Time, "Ys");
        }
    }

    [Theory]
    [ClassData(typeof(TimeScalesTestData))]
    public void TimeScales(MetricPrefix scale, Dimension type, string text)
    {
        Assert.True(UnitInfo.TryParse(text, out var unit));

        Assert.Equal(type,        unit.Dimension);
        Assert.Equal(scale.Value, unit.GetBaseUnitConversionFactor<double>());
    }
    

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
        UnitFactory.Default.AddRange(new ElectromagnetismUnitSet());

        Assert.True(UnitInfo.TryParse(text, out UnitInfo type));

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

