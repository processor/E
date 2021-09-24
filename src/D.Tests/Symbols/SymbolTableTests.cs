using E.Symbols;

namespace E.Parsing.Tests;

public class SymbolTableTests
{
    [Fact]
    public void A()
    {
        var table = new ModuleSymbol("root");

        table.Add(TypeSymbol.Int32);

        var result = table.TryGetValue("Int32", out var symbol);

        Assert.Same(TypeSymbol.Int32, symbol);
    }

    [Fact]
    public void B()
    {
        var table = new ModuleSymbol("root");

        table.Add(new ModuleSymbol("Geometry"));
        table.Add(new ModuleSymbol("Imaging"));
        table.Add(new ModuleSymbol("JSON"));

        table.TryGetValue("Geometry", out var geometry);

        geometry.Add(new TypeSymbol("Circle"));

        table.TryGetValue("JSON", out var json);

        json.Add(new MethodSymbol("parse"));
        json.Add(new MethodSymbol("stringify"));
    }
}