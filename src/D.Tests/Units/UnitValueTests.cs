﻿namespace E.Units.Tests;

public class QuantityTests
{
    [Fact]
    public void Percent()
    {
        var percent = Quantity.Percent(50);

        Assert.Equal(50, percent.Value);

        Assert.Equal(UnitInfo.Percent, percent.Unit);

        Assert.Equal(0.01m, ((INumberObject)percent.Unit.DefinitionUnit).As<decimal>());

        Assert.Equal(0.5, (percent as INumberObject).As<double>());
    }

    [Fact]
    public void Q()
    {
        var value = Quantity.Parse("3 m³");

        Assert.Equal("3m³", value.ToString());
        Assert.Equal("3m³", $"{value}");

        Assert.Equal("m", value.Unit.Name);
        Assert.Equal(3, value.Unit.Exponent);
        Assert.Equal(3, value.Value);
    }

    [Fact]
    public void Number()
    {
        var value = Quantity.Parse("3");

        Assert.Equal(3d, value.Value);
        Assert.Equal(UnitInfo.None, value.Unit);
        Assert.Equal("3", value.ToString());
    }
}