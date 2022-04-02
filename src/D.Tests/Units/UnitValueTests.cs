namespace E.Units.Tests;

public class UnitValueTests
{
    [Fact]
    public void Percent()
    {
        var percent = UnitValue.Percent(50);

        Assert.Equal(50, percent.Value);

        Assert.Equal(UnitInfo.Percent, percent.Unit);

        Assert.Equal(0.01, ((Number)percent.Unit.DefinitionUnit).Value);

        Assert.Equal(0.5, (percent as INumber).Real);
    }

    [Fact]
    public void Q()
    {
        var value = UnitValue.Parse("3 m³");

        Assert.Equal("3m³", value.ToString());
        Assert.Equal("3m³", $"{value}");

        Assert.Equal("m", value.Unit.Name);
        Assert.Equal(3, value.Unit.Power);
        Assert.Equal(3, value.Value);
    }

    [Fact]
    public void Number()
    {
        var value = UnitValue.Parse("3");

        Assert.Equal(3d, value.Value);
        Assert.Equal(UnitInfo.None, value.Unit);
        Assert.Equal("3", value.ToString());
    }
}