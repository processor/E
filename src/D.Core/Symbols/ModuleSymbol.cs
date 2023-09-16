using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace E.Symbols;

public sealed class ModuleSymbol(
    string name,
    ModuleSymbol? parent = null) : Symbol(name, [])
{
    private readonly Dictionary<string, Symbol> lookup = new();

    public ModuleSymbol? Parent { get; } = parent;

    public IEnumerable<Symbol> Children => lookup.Values;

    public override void Add(Symbol child)
    {
        lookup[child.Name] = child;
    }

    public override bool TryGetValue(string name, [NotNullWhen(true)] out Symbol? value)
    {
        return lookup.TryGetValue(name, out value);
    }
}