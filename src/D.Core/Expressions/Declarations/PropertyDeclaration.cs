namespace E.Expressions
{
    // let a: Integer = 5
    // let a of type Integer equal 5
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
}

/*
let a: Integer = 1;
let a: Integer > 1 = 5;
let mutable a = 1;
var a = 1
*/