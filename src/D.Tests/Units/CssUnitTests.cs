using System.Runtime.Serialization;
using Carbon.Json;
using Xunit;

namespace D.Units.Tests
{
    public class CssUnitTests
    {
        public class Element
        {
            [DataMember(Name = "width")]
            public UnitValue<double> Width { get; set; }

            [DataMember(Name = "height")]
            public UnitValue<double> Height { get; set; }
        }

        [Fact]
        public void Serialize()
        {
            // margin = new (20px, 20px)
            // margin = new (100px)

            var a = new Element {
                Width  = UnitValue.Parse("1920px"),
                Height = UnitValue.Parse("1080px")
            };

            // Element(width: 100px, margin: (100px, 80px))

            var json = JsonObject.FromObject(a);

            Assert.Equal(@"{
  ""width"": ""1920px"",
  ""height"": ""1080px""
}", json.ToString());

            var el = json.As<Element>();
            
            Assert.Equal("1920px", el.Width.ToString());
            Assert.Equal("1080px", el.Height.ToString());
        }

        [Fact]
        public void Parse1()
        {
            var val = UnitValue.Parse("11.5px");
            
            Assert.Equal(11.5,           val.Value);
            Assert.Equal(CssUnits.Px, val.Unit);
        }

        [Fact]
        public void Parse2()
        {
            var val = UnitValue.Parse("3turn");

            Assert.Equal(3, val.Value);
            Assert.Equal(UnitInfo.Turn, val.Unit);
        }

        [Fact]
        public void Parse3()
        {
            var val = UnitValue.Parse("-0.5turn");

            Assert.Equal(-0.5,          val.Value);
            Assert.Equal(UnitInfo.Turn, val.Unit);
        }

        [Theory]
        [InlineData(Dimension.PlaneAngle, "deg")]
        [InlineData(Dimension.PlaneAngle, "grad")]
        [InlineData(Dimension.PlaneAngle, "rad")]
        [InlineData(Dimension.PlaneAngle, "turn")]
        public void Angles(Dimension id, string text)
        {
            UnitInfo.TryParse(text, out UnitInfo type);

            Assert.Equal(id, type.Dimension);
        }

        [Theory]
        [InlineData(Dimension.Time, 1,     "s")]
        [InlineData(Dimension.Time, 0.001, "ms")]
        public void Durations(Dimension id, double scale, string text)
        {
            UnitInfo.TryParse(text, out UnitInfo type);

            Assert.Equal(id,    type.Dimension);
            Assert.Equal(scale, type.Prefix.Value);
        }

        [Theory]

        // Absolute
        [InlineData(Dimension.Length, "cm")]
        [InlineData(Dimension.Length, "mm")]
        [InlineData(Dimension.Length, "in")]
        [InlineData(Dimension.Length, "pt")]
        [InlineData(Dimension.Length, "pc")]
        [InlineData(Dimension.Length, "px")]

        // Relative
        [InlineData(Dimension.Length, "em")] 
        [InlineData(Dimension.Length, "ex")] 
        [InlineData(Dimension.Length, "ch")]
        [InlineData(Dimension.Length, "rem")]
        [InlineData(Dimension.Length, "vw")] 
        [InlineData(Dimension.Length, "vh")] 
        
        public void Lengths(Dimension id, string text)
        {
            UnitInfo.TryParse(text, out UnitInfo type);

            Assert.Equal(id, type.Dimension);
        }
        
        [Theory]
        [InlineData(Dimension.Frequency, 1,    "Hz")]
        [InlineData(Dimension.Frequency, 1000, "kHz")]
        public void Frequency(Dimension id, double scale, string text)
        {
            UnitInfo.TryParse(text, out UnitInfo type);

            Assert.Equal(scale, type.Prefix.Value);
            Assert.Equal(id, type.Dimension);
        }

        [Theory]
        [InlineData(Dimension.Resolution, 1, "dpi")]
        [InlineData(Dimension.Resolution, 1, "dpcm")]
        [InlineData(Dimension.Resolution, 1, "dppx")]
        public void Resolution(Dimension id, int scale, string text)
        {
            UnitInfo.TryParse(text, out UnitInfo type);

            Assert.Equal(scale, type.Prefix.Value);
            Assert.Equal(id, type.Dimension);
        }
    }
}