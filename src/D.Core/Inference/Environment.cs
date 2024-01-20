// Based on code by Cyril Jandia http://www.cjandia.com/ 
// LICENCE: https://github.com/ysharplanguage/System.Language/blob/master/LICENSE.md

using System.Collections.Generic;

namespace E.Inference;

public sealed class Environment(Environment? parent = null) : Dictionary<string, IType>
{
    private readonly Environment? _parent = parent;

    private IType Get(string id)
    {
        if (TryGetValue(id, out IType? value))
        {
            return value;
        }
        else if (_parent is not null && _parent.TryGetValue(id, out value))
        {
            return value;
        }
        else
        {
            throw new KeyNotFoundException($"{id} was not found");
        }
    }

    private void Set(string id, IType type)
    {
        base[id] = type;
    }

    public Environment Nested() => new (this);

    public new IType this[string id]
    {
        get => Get(id);
        set => Set(id, value);
    }
}