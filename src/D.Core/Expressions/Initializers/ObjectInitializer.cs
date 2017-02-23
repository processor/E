using System;

namespace D.Expressions
{
    public class ObjectInitializer : IExpression
    {
        public ObjectInitializer(Symbol type, ObjectMember[] properties)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public Symbol Type { get; }

        public ObjectMember[] Properties { get; }

        public int Count => Properties.Length;

        Kind IObject.Kind => Kind.ObjectInitializer; 
    }

    // { a: 1, b: 2 }
    // { a, b, c }

    public struct ObjectMember
    {
        public ObjectMember(Symbol name, IExpression value)
        {
            Name = name;
            Value = value;
        }

        public Symbol Name { get; }

        public IExpression Value { get; }
    }

    // // Point { x: 1, y: 2 }
    // Rust Notes: There is exactly one way to create an instance of a user-defined type: name it, and initialize all its fields at once:
}