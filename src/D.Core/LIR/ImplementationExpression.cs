using System;

namespace D
{
    using Expressions;

    public class ImplementationExpression : IObject
    {
        public ImplementationExpression(Protocal protocal, Type type, VariableDeclaration[] variables, FunctionExpression[] members)
        {
            Protocal = protocal;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Variables = variables;
            Methods = members ?? throw new ArgumentNullException(nameof(members));
        }

        public Type Type { get; }

        public Protocal Protocal { get; }

        public VariableDeclaration[] Variables { get; }

        public FunctionExpression[] Methods { get; }
        
        Kind IObject.Kind => Kind.ImplementationExpression;
    }
}