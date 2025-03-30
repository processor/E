namespace E.Tests.Numerics;

public class RationalTests
{
    [Fact]
    public void CanConvert()
    {
        INumberObject a = new Rational<decimal>(100, 200);

        Assert.Equal(0.5m, a.As<decimal>());
        Assert.Equal(0.5d, a.As<double>());
    }
}