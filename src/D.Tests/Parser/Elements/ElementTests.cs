using E.Syntax;

namespace E.Parsing.Tests;

public class ElementTests : TestBase
{
    [Fact]
    public void Simple()
    {
        var element = Parse<ElementSyntax>("<div>hello</div>");

        Assert.Equal("div", element.Name);

        Assert.Single(element.Children);
    }

    [Fact]
    public void NestedExpression()
    {
        var element = Parse<ElementSyntax>(
            """
            <div>for 1 to 10, we return a block { for i in 1...10 { yield <Block (number: i) /> } }</div>
            """);

        Assert.Equal("div", element.Name);

        var text = element.Children[0] as TextNodeSyntax;

        var block = element.Children[1] as BlockSyntax;

        Assert.Equal("for 1 to 10, we return a block ", text.Content);

        // throw new System.Exception(block.ToString());
    }

    [Fact]
    public void NestedElement()
    {
        var element = Parse<ElementSyntax>(
            """
            <Carbon:Gallery>
              <Carbon:Image (source: "1.heif") />
              <Carbon:Image (source: "2.heif") />
              <Carbon:Image (source: "3.heif") />
              <Carbon:Image (source: "4.heif") />
              <Carbon:Image (source: "5.heif") />

              <Block>
                <div>A few words <span>1</span><span>2</span></div>
              </Block>
            </Carbon:Gallery>
            """);

        Assert.Equal("Carbon", element.Namespace);
        Assert.Equal("Gallery", element.Name);

        Assert.Equal(6, element.Children.Length);

        var blockEl = element[5] as ElementSyntax;

        var divEl = blockEl[0] as ElementSyntax;

        var textNode = divEl[0] as TextNodeSyntax;

        var span1 = (ElementSyntax)divEl[1];
        var span2 = (ElementSyntax)divEl[2];

        Assert.Equal("A few words ", textNode.Content);
    }

    [Fact]
    public void SelfClosing()
    {
        var element = Parse<ElementSyntax>("<Image () />");

        Assert.Equal("Image", element.Name);
    }

    [Fact]
    public void NamespacedSelfClosingElement()
    {
        var element = Parse<ElementSyntax>("<Carbon:Image () />");

        Assert.Equal("Carbon", element.Namespace);
        Assert.Equal("Image", element.Name);
    }

    [Fact]
    public void ElementWithArgs()
    {
        var element = Parse<ElementSyntax>("""<div (name: "hello")>hello</div>""");

        Assert.Equal("div", element.Name);
        Assert.Equal("name", element.Arguments[0].Name);
        Assert.Equal("hello", element.Arguments[0].Value.ToString());
    }
}
