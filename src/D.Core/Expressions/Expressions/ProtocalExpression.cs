namespace D
{
    public class ProtocalExpression : IObject
    {  
        public ProtocalExpression(Symbol name, FunctionExpression[] members)
        {
            Name = name;
            Members = members;
        }

        public Symbol Name { get; set; }

        public FunctionExpression[] Members { get; }

        // public IMessageDeclaration[] Channel { get; set; }

        Kind IObject.Kind => Kind.Protocal;
    }
}