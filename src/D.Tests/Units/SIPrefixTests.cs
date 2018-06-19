using Xunit;

namespace D.Units
{
    public class SIPrefixTests
    {
        [Fact]
        public void KPrefixTest()
        {
            Assert.Equal("k", SIPrefix.k.Name);
            Assert.Equal(1000d, SIPrefix.k.Value);
        }

        [Fact]
        public void ScalesAreCorrect()
        {
            Assert.Equal(1000000000000000000000000d, SIPrefix.Y.Value);
            Assert.Equal(1000000000000000000000d, SIPrefix.Z.Value);
            Assert.Equal(1000000000000000000d, SIPrefix.E.Value);
            Assert.Equal(1000000000000000d, SIPrefix.P.Value);
            Assert.Equal(1000000000000d, SIPrefix.T.Value);
            Assert.Equal(1000000000d, SIPrefix.G.Value);
            Assert.Equal(1000000d, SIPrefix.M.Value);
            Assert.Equal(1000d, SIPrefix.k.Value);
            Assert.Equal(0.001d, SIPrefix.m.Value);
            Assert.Equal(0.000001d, SIPrefix.µ.Value);
            Assert.Equal(0.000000001d, SIPrefix.n.Value);
            Assert.Equal(0.000000000001d, SIPrefix.p.Value);
            Assert.Equal(0.000000000000001d, SIPrefix.f.Value);
            Assert.Equal(0.000000000000000001d, SIPrefix.a.Value);
            Assert.Equal(0.000000000000000000001d, SIPrefix.z.Value);
            Assert.Equal(0.000000000000000000000001d, SIPrefix.y.Value);
        }
    }
}