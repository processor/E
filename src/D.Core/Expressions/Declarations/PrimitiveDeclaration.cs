namespace D.Expressions
{
    public class PrimitiveDeclaration : IExpression
    {
        public PrimitiveDeclaration(Symbol name, int? size = null)
        {
            Name = name;
            Size = size;
        }

        public Symbol Name { get; }

        public int? Size { get; }

        Kind IObject.Kind => Kind.PrimitiveDeclaration;
    }


    // px unit { }
}