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
            
            Assert.Equal(11.5,           val.Quantity);
            Assert.Equal(CssUnitType.Px, val.Type);
        }

        [Fact]
        public void Parse2()
        {
            var val = UnitValue.Parse("3turn");

            Assert.Equal(3, val.Quantity);
            Assert.Equal(UnitType.Turn, val.Type);
        }

        [Fact]
        public void Parse3()
        {
            var val = UnitValue.Parse("-0.5turn");

            Assert.Equal(-0.5,          val.Quantity);
            Assert.Equal(UnitType.Turn, val.Type);
        }

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