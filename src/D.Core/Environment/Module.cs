using System.Collections.Generic;
using E.Expressions;

namespace E;

public class Module(string? name = null, Module? parent = null) : IExpression
{
    public readonly Dictionary<string, object> _exports = new();

    private readonly List<IExpression> _statements = new();

    public string? Name { get; } = name;

    public Module? Parent { get; } = parent;

    // Imported
    // Exported

    public void Add(IExpression value)
    {
        // TODO: Check visibility
        _statements.Add(value);
    }

    public void AddExport(INamedObject value)
    {
        _exports[value.Name] = value;
    }

    public void AddExport(string name, object value)
    {
        _exports[name] = value;
    }

    public ObjectType Kind => ObjectType.Module;

    // import * from Geometry

    // public IDictionary<string, TypeSymbol> Imports => imports;

    public IDictionary<string, object> Exports => _exports;

    public IReadOnlyList<IExpression> Statements => _statements;
}