using System;

namespace D.Expressions
{
    public class TypeInitializer : IExpression
    {
        public TypeInitializer(TypeSymbol type, Argument[] arguments)
        {
            Type      = type      ?? throw new ArgumentNullException(nameof(type));
            Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
        }

        public TypeSymbol Type { get; }

        public Argument[] Arguments { get; }

        public int Count => Arguments.Length;

        Kind IObject.Kind => Kind.TypeInitializer; 
    }
}

// Tuple Based Syntax
// (x: 1, y: 2)
// (x, y)

// Rust uses a different syntax... and requires that all fields be initized at once
// Point { x: 1, y: 2 }