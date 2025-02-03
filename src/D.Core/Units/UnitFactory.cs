using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using E.Collections;

namespace E.Units;

public class UnitFactory
{
    public static readonly UnitFactory Default = new(
       new GeneralUnitSet(),
       new ThermodynamicUnitSet(),
       new ElectromagismUnitSet(),
       new MechanicalUnitSet(),
       new CssUnitSet()
    );

    public UnitFactory(params ReadOnlySpan<UnitSet> sets)
    {
        foreach (var set in sets)
        {
            AddRange(set);
        }
    }

    private readonly Dictionary<string, UnitInfo> _symbolMap = [];
    private readonly Dictionary<long, UnitInfo> _idMap = [];
    private readonly Trie<UnitInfo> _trie = new();

    public bool Contains(ReadOnlySpan<char> name) => _trie.ContainsKey(name);

    public bool TryGet(ReadOnlySpan<char> name, [NotNullWhen(true)] out UnitInfo? type)
    {
        return _trie.TryGetValue(name, out type);
    }

    public UnitInfo? Find(long id)
    {
        return _idMap.TryGetValue(id, out var type) ? type : null;
    }

    public void Add(string symbol, UnitInfo type)
    {
        _symbolMap[symbol] = type;

        _trie.TryAdd(symbol, type);

        if (type.Id > 0)
        {
            _idMap[type.Id] = type;
        }
    }

    public void Add(UnitInfo type)
    {
        _symbolMap[type.Name] = type;

        if (type.Id > 0)
        {
            _idMap[type.Id] = type;
        }
    }

    public void AddRange(UnitSet collection)
    {
        foreach (var item in collection)
        {
            Add(item.Key, item.Value);
        }
    }
}
