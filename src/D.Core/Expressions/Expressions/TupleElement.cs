﻿using E.Symbols;

namespace E.Expressions;

public sealed class TupleElement(Symbol name, IExpression? value) : IExpression
{
    public Symbol Name { get; } = name;

    // type or constant
    public IExpression? Value { get; } = value;

    public void Deconstruct(out Symbol name, out IExpression? value)
    {
        name = Name;
        value = Value;
    }

    ObjectType IObject.Kind => ObjectType.TupleElement;
}

// EXAMPLES -
// a: 100   | symbol + value
// a        | symbol only