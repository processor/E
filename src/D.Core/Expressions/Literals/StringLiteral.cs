using System;

namespace E.Expressions;

public readonly struct StringLiteral(string value) : IExpression
{
    public string Value { get; } = value ?? throw new ArgumentNullException(nameof(value));

    public static implicit operator StringLiteral(string text) => new(text);

    public static implicit operator string(StringLiteral text) => text.Value;

    ObjectType IObject.Kind => ObjectType.String;

    public override string ToString() => Value;
}