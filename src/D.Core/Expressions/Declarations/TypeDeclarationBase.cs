using D.Symbols;

namespace D.Expressions
{
    public abstract class TypeDeclarationBase : IExpression
    {
        public TypeDeclarationBase(
            Symbol baseType, 
            Property[] members, 
            TypeFlags flags = TypeFlags.None)
        {
            BaseType = baseType;
            Members = members;
            Flags = flags;
        }

        // : A
        public Symbol BaseType { get; }

        public TypeFlags Flags { get; }

        public Property[] Members { get; }

        public bool IsRecord => Flags.HasFlag(TypeFlags.Record);

        public bool IsEvent => Flags.HasFlag(TypeFlags.Event);

        ObjectType IObject.Kind => ObjectType.TypeDeclaration;
    }

    // MAP = * -> *
}


/*
type Person = {
  name: String where length > 0
}
*/
