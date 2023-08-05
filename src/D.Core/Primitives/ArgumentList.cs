using System.Collections;
using System.Collections.Generic;

namespace E;

public sealed class ArgumentList(Argument[] items) : IArguments
{
    private readonly Argument[] _items = items;

    public object? this[int i] => _items[i].Value;

    public object? this[string name]
    {
        get
        {
            foreach (var arg in _items)
            {
                if (arg.Name is not null && arg.Name == name)
                {
                    return arg.Value;
                }
            }

            throw new KeyNotFoundException(name ?? "null");
        }
    }

    public int Count => _items.Length;

    #region IEnumerable

    public IEnumerator<Argument> GetEnumerator() => ((IList<Argument>)_items).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

    #endregion
}
