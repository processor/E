namespace E.Units.Tests;

public class VolumeTests
{
    [Theory]
    [InlineData("1 ml",    "1 ml")]
    [InlineData("1000 ml", "1 L")]
    [InlineData("1 ML",    "1_000_000 L")]
    // [InlineData("1 L",     "0.001 m³")]
    public void CanConvert(string s, string t)
    {
        var source = Quantity.Parse(s);
        var target = Quantity.Parse(t);

        Assert.Equal(target.Value, source.To(target.Unit));
    }

    [Fact]
    public void CupTests()
    {
        Assert.Equal(0.000236588m, USVolumeUnits.Cup.GetBaseUnitConversionFactor<decimal>());
    }

    [Fact]
    public void USUnitsOfVolumeTests()
    {
        Assert.Equal("c", USVolumeUnits.Cup.ToString());

        var _1cup = new Quantity<double>(1, USVolumeUnits.Cup);


        Assert.Equal(0.00023658800000000001, _1cup.To(VolumeUnits.CubicMetre));

        var _1cupDecimal = new Quantity<decimal>(1, USVolumeUnits.Cup);

        Assert.Equal(0.000236588m, _1cupDecimal.To(VolumeUnits.CubicMetre));

        Assert.Equal(46.49432769202157336804909801m, new Quantity<decimal>(11, VolumeUnits.Litre).To(USVolumeUnits.Cup));
    }

    // 227ms
    [Fact]
    public void Bench()
    {
        var cup = USVolumeUnits.Cup;

        var a = cup.Converters[0].Compile<double>();

        for (int i = 0; i < 1_000_000; i++)
        {
            Assert.Equal(0.000236588, a(1));
        }
    }
}