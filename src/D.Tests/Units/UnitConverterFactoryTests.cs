namespace E.Units;

public class UnitConverterFactoryTests
{
    [Fact]
    public void A()
    {
        var c = new UnitConverterFactory<double>();

        c.Add(UnitType.US_FluidOunce, UnitType.CubicMetre, static (double source) => source * 0.00002957353);

        Assert.Equal(0.00002957353, c.Get(UnitType.US_FluidOunce, UnitType.CubicMetre)(1));
    }

    [Fact]
    public void CanRegisterCustomConversions()
    {
        var factory = new UnitConverterFactory<double>();

        factory.Add(UnitType.Second, new(11, UnitInfo.Radian));

        var _2seconds = new Quantity<double>(2, TimeUnits.Second);
        var _1minute  = new Quantity<double>(1, TimeUnits.Minute);
        var _1day     = new Quantity<double>(1, TimeUnits.Day);

        Assert.Equal(60d,                    _1minute.To(TimeUnits.Second, factory));
        Assert.Equal(0.016666666666666666,   _1minute.To(TimeUnits.Hour,   factory));
        Assert.Equal(0.00069444444444444447, _1minute.To(TimeUnits.Day,    factory));

        Assert.Equal(86400, _1day.To(TimeUnits.Second, factory));
        Assert.Equal(22,    _2seconds.To(UnitInfo.Radian, factory)); // not true
    }

    [Fact]
    public void CompiledDoubleFuncBenchmark()
    {
        var factory = new UnitConverterFactory<double>();

        var _1minute = new Quantity<double>(1, TimeUnits.Minute);

        Assert.Equal(0.016666666666666666, _1minute.To(TimeUnits.Hour, factory));
    }

    [Fact]
    public void CompiledDecimalFuncBenchmark()
    {
        var factory = new UnitConverterFactory<decimal>();

        var _1minute = new Quantity<decimal>(1, TimeUnits.Minute);

        Assert.Equal(0.0166666666666666666666666667m, _1minute.To(TimeUnits.Hour, factory));
    }
}