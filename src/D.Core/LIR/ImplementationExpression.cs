namespace D
{
    using Expressions;

    public sealed class ImplementationExpression : IExpression
    {
        public ImplementationExpression(
            ProtocolExpression protocol, 
            Type type, 
            VariableDeclaration[] variables,
            FunctionExpression[] members)
        {
            Protocol  = protocol;
            Type = type;
            Variables = variables;
            Methods = members;
        }

        // Struct | Class

        public Type Type { get; }

        public ProtocolExpression Protocol { get; }

        public VariableDeclaration[] Variables { get; }

        public FunctionExpression[] Methods { get; }
        
        ObjectType IObject.Kind => ObjectType.Implementation;
    }
}