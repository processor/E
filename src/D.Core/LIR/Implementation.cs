using System;

namespace D
{
    using Expressions;

    public class Implementation : IObject
    {
        public Implementation(Protocal protocal, Type type, VariableDeclaration[] variables, Function[] members)
        {
            Protocal = protocal;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Variables = variables;
            Methods = members ?? throw new ArgumentNullException(nameof(members));
        }

        public Type Type { get; }

        public Protocal Protocal { get; }

        public VariableDeclaration[] Variables { get; }

        public Function[] Methods { get; }
        
        Kind IObject.Kind => Kind.Implementation;
    }
}