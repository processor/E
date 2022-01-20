using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using E.Modules;
using E.Symbols;

namespace E;

public class Node
{
    private readonly ConcurrentDictionary<string, object> children = new();

    private readonly OperatorCollection operators = new();

    private readonly int depth = 0;

    public Node(string? name = null, Node? parent = null)
    {
        Name = name;
        Parent = parent;

        if (parent is not null)
        {
            this.depth = parent.depth + 1;
        }

        operators.Add(Operator.DefaultList);

        AddModule(new BaseModule());
    }

    public Node(params Module[] modules)
        : this()
    {
        foreach (var module in modules)
        {
            AddModule(module);
        }
    }

    public void AddModule(Module module)
    {
        foreach (var pair in module.Exports)
        {
            children.TryAdd(pair.Key, pair.Value);

            if (pair.Value is Operator @operator)
            {
                operators.Add(@operator);
            }
        }
    }

    public string? Name { get; }

    public Node? Parent { get; }

    public OperatorCollection Operators => operators;

    public void Add(string name, object value)
    {
        if (!children.TryAdd(name, value))
        {
            // throw new Exception($"{name} already added");
        }
    }

    public void Set<T>(string name, T value)
        where T : notnull
    {
        children[name] = value;
    }

    public bool TryGetValue<T>(string key, [NotNullWhen(true)] out T? value)
        where T : notnull
    {
        if (!TryGet(key, out object? r))
        {
            value = default;

            return false;
        }

        value = (T)r!;

        return true;
    }

    public bool TryGet(string name, [NotNullWhen(true)] out object? kind)
    {
        if (children.TryGetValue(name, out kind))
        {
            return true;
        }
        else if (Parent is not null && Parent.TryGet(name, out kind))
        {
            return true;
        }

        return false;
    }

    private bool TryGetType(Symbol symbol, [NotNullWhen(true)] out Type? type)
    {
        if (TryGet(symbol.Name, out object? t))
        {
            type = (Type)t;

            return true;
        }

        type = null;

        return false;
    }

    public Type GetType(Symbol symbol)
    {
        if (TryGetType(symbol, out Type? type))
        {
            return type;
        }

        if (symbol.Arguments is { Count: > 0 })
        {
            var args = new Type[symbol.Arguments.Count];

            for (var i = 0; i < args.Length; i++)
            {
                args[i] = GetType(symbol.Arguments[i]);
            }

            return new Type(symbol.Module, symbol.Name, args);
        }
        else
        {
            return new Type(symbol.Module, symbol.Name);
        }
    }

    public T Get<T>(string name)
        where T : notnull
    {
        if (!TryGetValue(name, out T? value))
        {
            throw new KeyNotFoundException($"Node does not contain {name} of {typeof(T).Name}");
        }

        return value;
    }

    public T Get<T>(Symbol symbol)
        where T : notnull
    {
        if (!TryGetValue(symbol, out T? value))
        {
            if (typeof(T) == typeof(Type))
            {
                return (T)(object)new Node().GetType(symbol);
            }

            throw new Exception($"context does not contain {typeof(T).Name} '{symbol.Name}'");
        }

        return value;
    }

    public T Get<T>(Symbol symbol, Argument[] args)
    {
        // Find by match...

        throw new NotImplementedException();
    }

    public Node Nested(string? name = null)
    {
        return new Node(name, this);
    }
}
