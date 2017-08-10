using System;

namespace D
{
    using Expressions;

    public class ImplementationExpression : IObject
    {
        public ImplementationExpression(ProtocolExpression protocol, Type type, VariableDeclaration[] variables, FunctionExpression[] members)
        {
            Protocol  = protocol;
            Type      = type ?? throw new ArgumentNullException(nameof(type));
            Variables = variables;
            Methods   = members ?? throw new ArgumentNullException(nameof(members));
        }

        // Struct | Class

        public Type Type { get; }

        public ProtocolExpression Protocol { get; }

        public VariableDeclaration[] Variables { get; }

        public FunctionExpression[] Methods { get; }
        
        Kind IObject.Kind => Kind.ImplementationExpression;
    }
}