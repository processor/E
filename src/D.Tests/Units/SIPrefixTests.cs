namespace E.Units;

public class SIPrefixTests
{
    [Fact]
    public void KPrefixTest()
    {
        Assert.Equal("k", MetricPrefix.k.Name);
        Assert.Equal(1000d, MetricPrefix.k.Value);
    }

    [Fact]
    public void ScalesAreCorrect()
    {
        Assert.Equal(1000000000000000000000000d, MetricPrefix.Y.Value);
        Assert.Equal(1000000000000000000000d, MetricPrefix.Z.Value);
        Assert.Equal(1000000000000000000d, MetricPrefix.E.Value);
        Assert.Equal(1000000000000000d, MetricPrefix.P.Value);
        Assert.Equal(1000000000000d, MetricPrefix.T.Value);
        Assert.Equal(1000000000d, MetricPrefix.G.Value);
        Assert.Equal(1000000d, MetricPrefix.M.Value);
        Assert.Equal(1000d, MetricPrefix.k.Value);
        Assert.Equal(0.001d, MetricPrefix.m.Value);
        Assert.Equal(0.000001d, MetricPrefix.µ.Value);
        Assert.Equal(0.000000001d, MetricPrefix.n.Value);
        Assert.Equal(0.000000000001d, MetricPrefix.p.Value);
        Assert.Equal(0.000000000000001d, MetricPrefix.f.Value);
        Assert.Equal(0.000000000000000001d, MetricPrefix.a.Value);
        Assert.Equal(0.000000000000000000001d, MetricPrefix.z.Value);
        Assert.Equal(0.000000000000000000000001d, MetricPrefix.y.Value);
    }
}
