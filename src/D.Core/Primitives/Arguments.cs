using System;

namespace E;

public static class Arguments
{
    public static readonly ArgumentList None = new ArgumentList(Array.Empty<Argument>());

    public static IArguments Create(Argument[] items) => items.Length switch
    {
        0 => None,
        1 => items[0],
        _ => new ArgumentList(items)
    };

    public static IArguments Create(object value)
    {
        return new Argument(null, value);
    }

    public static ArgumentList Create(params object[] values)
    {
        var args = new Argument[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            args[i] = new Argument(null, values[i]);
        }

        return new ArgumentList(args);
    }
}
