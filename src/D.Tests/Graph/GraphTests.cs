using System.Text.Json;
using System.Text.Json.Nodes;

using E.Expressions;
using E.Parsing.Tests;
using E.Symbols;

namespace E.Graph.Tests;

public class GraphTests : TestBase
{
    [Fact]
    public void Nested()
    {
        var env = new Node();

        env.Add("JSON", new Node());

        var json = env.Get<Node>("JSON");

        Func<object, JsonNode> func = (object instance) => JsonSerializer.SerializeToNode(instance);

        json.Add("encode", func);

        var result = env.Get<Node>("JSON").Get<Func<object, JsonNode>>("encode").Invoke(new[] { 1 });

        Assert.Equal(
            """
            [
              1
            ]
            """, result.ToString());
    }

    [Fact]
    public void Root()
    {
        var context = new Node();

        context.Add("a", new StringLiteral("a"));
        context.Add("b", new Integer(1));

        Assert.Equal("Object", context.Get<Type>(TypeSymbol.Object).Name);

        Assert.Equal("a", context.Get<StringLiteral>(Symbol.Variable("a")));
        Assert.Equal(1, context.Get<Integer>(Symbol.Variable("b")));
    }

    [Fact]
    public void Child()
    {
        var env = new Node();

        env.Add("name", new StringLiteral("name"));

        var child = env.Nested("child");

        Assert.Equal("name", child.Get<StringLiteral>(Symbol.Variable("name")));
        Assert.Equal("Object", child.Get<Type>(TypeSymbol.Object).Name);
    }
}