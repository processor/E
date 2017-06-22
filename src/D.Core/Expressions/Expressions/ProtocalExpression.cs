using System;

namespace D.Expressions
{
    public class ProtocolExpression : INamedObject
    {  
        public ProtocolExpression(Symbol name, FunctionExpression[] members)
        {
            Name    = name    ?? throw new ArgumentNullException(nameof(name));
            Members = members ?? throw new ArgumentNullException(nameof(members));
        }

        public Symbol Name { get; set; }

        public FunctionExpression[] Members { get; }

        // public IMessageDeclaration[] Channel { get; set; }

        Kind IObject.Kind => Kind.Protocol;

        string INamedObject.Name => Name;
    }
}