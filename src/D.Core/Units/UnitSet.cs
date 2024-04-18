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

    private readonly Dictionary<string, UnitInfo> _symbolMap = [];
    private readonly Dictionary<long, UnitInfo> _idMap = [];

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
        foreach (var item in collection._symbolMap)
        {
            Add(item.Key, item.Value);
        }
    }
}