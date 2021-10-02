namespace E.Expressions;

public sealed class VariableDeclaration : IExpression
{
    public VariableDeclaration(
        string name, 
        Type type, 
        ObjectFlags flags, 
        IExpression? value = null)
    {
        Name = name;
        Type = type;
        Flags = flags;
        Value = value;
    }

    public string Name { get; }

    public Type Type { get; }

    public ObjectFlags Flags { get; }

    public IExpression? Value { get; }

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