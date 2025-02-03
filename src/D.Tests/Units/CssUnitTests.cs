using System.Text.Json.Serialization;

using Carbon.Json;

namespace E.Units.Tests;

public class CssUnitTests
{
    public sealed class Element
    {
        [JsonPropertyName("width")]
        public Quantity<double> Width { get; set; }

        [JsonPropertyName("height")]
        public Quantity<double> Height { get; set; }

        [JsonPropertyName("flex")]
        public Quantity<double> Flex { get; set; }
    }

    [Fact]
    public void CanSerialize()
    {
        // margin = new (20px, 20px)
        // margin = new (100px)

        var a = new Element {
            Width = Quantity.Parse("1920px"),
            Height = Quantity.Px(1080),
            Flex = Quantity.Percent(100)
        };

        // Element(width: 100px, margin: (100px, 80px))

        var json = JsonObject.FromObject(a);

        Assert.Equal(
            """
            {
              "width": "1920px",
              "height": "1080px",
              "flex": "100%"
            }
            """, json.ToString(), ignoreLineEndingDifferences: true);

        var el = json.Deserialize<Element>();

        Assert.Equal("1920px", el.Width.ToString());
        Assert.Equal("1080px", el.Height.ToString());
        Assert.Equal("100%", el.Flex.ToString());
    }

    [Fact]
    public void Parse1()
    {
        var val = Quantity.Parse("11.5px");

        Assert.Equal((11.5, CssUnits.Px), (val.Value, val.Unit));
    }

    [Fact]
    public void Parse2()
    {
        var val = Quantity.Parse("3turn");

        Assert.Equal(3, val.Value);
        Assert.Equal(UnitInfo.Turn, val.Unit);
    }

    [Fact]
    public void Parse3()
    {
        var val = Quantity.Parse("-0.5turn");

        Assert.Equal((-0.5d, UnitInfo.Turn), (val.Value, val.Unit));
    }

    [Theory]
    [InlineData("deg")]
    [InlineData("grad")]
    [InlineData("rad")]
    [InlineData("turn")]
    public void Angles(string symbol)
    {
        Assert.True(UnitInfo.TryParse(symbol, out UnitInfo type));

        Assert.Equal(symbol, type.Name);
        Assert.Equal(Dimension.Angle, type.Dimension);
    }

    [Theory]
    [InlineData(1,     "s")]
    [InlineData(0.001, "ms")]
    public void Durations(double scale, string text)
    {
        Assert.True(UnitInfo.TryParse(text, out UnitInfo type));

        Assert.Equal(text, type.ToString());
        Assert.Equal(Dimension.Time, type.Dimension);
        Assert.Equal(scale, type.GetBaseUnitConversionFactor<double>());
    }

    [Theory]

    // Absolute
    [InlineData("cm")]
    [InlineData("mm")]
    [InlineData("in")]
    [InlineData("pt")]
    [InlineData("pc")]
    [InlineData("px")]

    // Relative
    [InlineData("em")]
    [InlineData("ex")]
    [InlineData("ch")]
    [InlineData("rem")]
    [InlineData("vw")]
    [InlineData("vh")]
    public void Lengths(string text)
    {
        Assert.True(UnitInfo.TryParse(text, out UnitInfo type));

        Assert.Equal(Dimension.Length, type.Dimension);
    }

    [Theory]
    [InlineData(1, "Hz")]
    [InlineData(1000, "kHz")]
    public void Frequency(double scale, string text)
    {
        Assert.True(UnitInfo.TryParse(text, out UnitInfo type));

        Assert.Equal(scale, type.GetBaseUnitConversionFactor<double>());
        Assert.Equal(Dimension.Frequency, type.Dimension);
    }

    [Theory]
    [InlineData("dpi")]
    [InlineData("dpcm")]
    [InlineData("dppx")]
    public void Resolution(string text)
    {
        Assert.True(UnitInfo.TryParse(text, out UnitInfo type));

        Assert.Equal(1, type.GetBaseUnitConversionFactor<double>());
        Assert.Equal(Dimension.Resolution, type.Dimension);
    }
}
