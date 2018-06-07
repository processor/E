using Xunit;

namespace D.Units.Tests
{
    public class CssUnitTests
    {
        [Theory]
        [InlineData(UnitId.Angle, "deg")]
        [InlineData(UnitId.Angle, "grad")]
        [InlineData(UnitId.Angle, "rad")]
        [InlineData(UnitId.Angle, "turn")]
        public void Angles(UnitId id, string text)
        {
            UnitType.TryParse(text, out UnitType type);

            Assert.Equal(id, type.BaseUnit);
        }

        [Theory]
        [InlineData(UnitId.Time, 1,     "s")]
        [InlineData(UnitId.Time, 0.001, "ms")]
        public void Durations(UnitId id, double scale, string text)
        {
            UnitType.TryParse(text, out UnitType type);

            Assert.Equal(id,    type.BaseUnit);
            Assert.Equal(scale, type.Prefix.Value);
        }

        [Theory]

        // Absolute
        [InlineData(UnitId.Length, "cm")]
        [InlineData(UnitId.Length, "mm")]
        [InlineData(UnitId.Length, "in")]
        [InlineData(UnitId.Length, "pt")]
        [InlineData(UnitId.Length, "pc")]
        [InlineData(UnitId.Length, "px")]

        // Relative
        [InlineData(UnitId.Length, "em")] 
        [InlineData(UnitId.Length, "ex")] 
        [InlineData(UnitId.Length, "ch")]
        [InlineData(UnitId.Length, "rem")]
        [InlineData(UnitId.Length, "vw")] 
        [InlineData(UnitId.Length, "vh")] 
        
        public void Lengths(UnitId id, string text)
        {
            UnitType.TryParse(text, out UnitType type);

            Assert.Equal(id, type.BaseUnit);
        }



        [Theory]
        [InlineData(UnitId.Frequency, 1,    "Hz")]
        [InlineData(UnitId.Frequency, 1000, "kHz")]
        public void Frequency(UnitId id, double scale, string text)
        {
            UnitType.TryParse(text, out UnitType type);

            Assert.Equal(scale, type.Prefix.Value);
            Assert.Equal(id, type.BaseUnit);
        }


        [Theory]
        [InlineData(UnitId.Resolution, 1, "dpi")]
        [InlineData(UnitId.Resolution, 1, "dpcm")]
        [InlineData(UnitId.Resolution, 1, "dppx")]
        public void Resolution(UnitId id, int scale, string text)
        {
            UnitType.TryParse(text, out UnitType type);

            Assert.Equal(scale, type.Prefix.Value);
            Assert.Equal(id, type.BaseUnit);
        }
    }
}