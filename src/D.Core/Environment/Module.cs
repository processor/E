using System.Collections.Generic;
using E.Expressions;

namespace E;

public class Module(string? name = null, Module? parent = null) : IExpression
{
    public readonly Dictionary<string, object> exports = new();

    private readonly List<IExpression> statements = new();

    public string? Name { get; } = name;

    public Module? Parent { get; } = parent;

    // Imported
    // Exported

    public void Add(IExpression value)
    {
        // TODO: Check visibility
        statements.Add(value);
    }

    public void AddExport(INamedObject value)
    {
        exports[value.Name] = value;
    }

    public void AddExport(string name, object value)
    {
        exports[name] = value;
    }

    public ObjectType Kind => ObjectType.Module;

    // import * from Geometry

    // public IDictionary<string, TypeSymbol> Imports => imports;

    public IDictionary<string, object> Exports => exports;

    public IReadOnlyList<IExpression> Statements => statements;
}