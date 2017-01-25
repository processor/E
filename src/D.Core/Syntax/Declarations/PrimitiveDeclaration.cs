namespace D.Syntax
{
    public class PrimitiveDeclarationSyntax : ISyntax
    {
        public PrimitiveDeclarationSyntax(Symbol name, int? size = null)
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