namespace E.Tests.Numerics;

public class RationalTests
{
    [Fact]
    public void CanConvert()
    {
        INumber a = new Rational(100, 200);

        Assert.Equal(0.5m, a.As<decimal>());
        Assert.Equal(0.5d, a.As<double>());
    }
}