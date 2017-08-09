using System;

namespace D.Expressions
{
    public class ObjectInitializer : IExpression
    {
        public ObjectInitializer(TypeSymbol type, Argument[] arguments)
        {
            Type      = type      ?? throw new ArgumentNullException(nameof(type));
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public TypeSymbol Type { get; }

        public Argument[] Arguments { get; }

        public int Count => Arguments.Length;

        Kind IObject.Kind => Kind.ObjectInitializer; 
    }

    // { a: 1, b: 2 }
    // { a, b, c }

    // // Point { x: 1, y: 2 }
    // Rust Notes: There is exactly one way to create an instance of a user-defined type: name it, and initialize all its fields at once:
}