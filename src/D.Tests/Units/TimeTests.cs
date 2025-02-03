namespace E.Units.Tests;

public class TimeTests
{
    [Fact]
    public void A()
    {
        var _1minute = new Quantity<double>(1, TimeUnits.Minute);

        Assert.Equal(60d,                  _1minute.To(TimeUnits.Second));
        Assert.Equal(0.016666666666666666, _1minute.To(TimeUnits.Hour));
    }
}