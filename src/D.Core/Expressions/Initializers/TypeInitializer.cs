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