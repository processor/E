using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using E.Collections;

namespace E.Units;

public class UnitSet
{
    public static readonly UnitSet Default = new(
        new GeneralUnitSet(),
        new ThermodynamicUnitSet(),
        new ElectromagismUnitSet(),
        new MechanicalUnitSet(),
        new CssUnitSet()
    );

    private readonly Dictionary<string, UnitInfo> _items = [];

    private readonly Trie<UnitInfo> _trie = new ();

    public UnitSet() { }

    public UnitSet(params UnitSet[] collections)
    {
        foreach (var set in collections)
        {
            AddRange(set);
        }
    }

    public bool Contains(ReadOnlySpan<char> name) => _trie.ContainsKey(name);

    public bool TryGet(ReadOnlySpan<char> name, [NotNullWhen(true)] out UnitInfo? type)
    {
       return _trie.TryGetValue(name, out type);
    }

    public void Add(string name, UnitInfo type)
    {
        _items[name] = type;

        _trie.TryAdd(name, type);
    }

    public void Add(UnitInfo type)
    {
        _items[type.Name] = type;
    }

    public void AddRange(UnitSet collection)
    {
        foreach (var item in collection._items)
        {
            Add(item.Key, item.Value);
        }
    }
}