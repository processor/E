using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace E.Parsing;

public ref struct ListBuilder<T>
{
    private int count = 0;

    private T? _0 = default;
    private T? _1 = default;
    private T? _2 = default;
    private T? _3 = default;
    private List<T>? _extra = null;

    public ListBuilder() { }

    public readonly int Count => count;

    public void Add(T node)
    {
        switch (count)
        {
            case 0: _0 = node; break;
            case 1: _1 = node; break;
            case 2: _2 = node; break;
            case 3: _3 = node; break;
            default: (_extra ??= new()).Add(node); break;
        }

        count++;
    }

    public void AddRange(IEnumerable<T> items) 
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public readonly T[] ToArray()
    {
        var result = new T[count];

        switch (count)
        {
            case 0:
                return [];
            case 1:
                result[0] = _0!;
                break;
            case 2:
                result[0] = _0!;
                result[1] = _1!;
                break;
            case 3:
                result[0] = _0!;
                result[1] = _1!;
                result[2] = _2!;
                break;
            case 4:
                result[0] = _0!;
                result[1] = _1!;
                result[2] = _2!;
                result[3] = _3!;
                break;
            default:
                result[0] = _0!;
                result[1] = _1!;
                result[2] = _2!;
                result[3] = _3!;

                CollectionsMarshal.AsSpan(_extra).CopyTo(result.AsSpan(4));
                break;
        }

        return result;
    }
}
