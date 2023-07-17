namespace E.Expressions;

public sealed class VariableDeclaration(
    string name,
    Type type,
    ObjectFlags flags,
    IExpression? value = null) : IExpression
{
    public string Name { get; } = name;

    public Type Type { get; } = type;

    public ObjectFlags Flags { get; } = flags;

    public IExpression? Value { get; } = value;

    public bool IsMutable => Flags.HasFlag(ObjectFlags.Mutable);

    ObjectType IObject.Kind => ObjectType.PropertyDeclaration;
}


/*
let a: Integer = 11;
let a: Integer > 1 = 5;
let mutable a = 1;
var a = 1

-- other langs
let a of type Integer equal 5
*/