namespace E.Tests;

public class SuperscriptTests
{
    [Fact]
    public void Format()
    {
        Assert.Equal("¹",         new Superscript(1).ToString());
        Assert.Equal("¹¹",        new Superscript(11).ToString());
        Assert.Equal("¹¹¹",       new Superscript(111).ToString());
        Assert.Equal("¹⁰⁰⁰",      new Superscript(1000).ToString());
        Assert.Equal("¹²³⁴⁵⁶⁷⁸⁹", new Superscript(123456789).ToString());
    }

    [Fact]
    public void Parse()
    {
        Assert.Equal(123456789, Superscript.Parse("¹²³⁴⁵⁶⁷⁸⁹"));
    }
}