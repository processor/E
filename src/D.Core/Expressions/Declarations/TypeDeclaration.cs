namespace D.Expressions
{
    public class TypeDeclarationBase : IExpression
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

    public sealed class TypeDeclaration : TypeDeclarationBase
    {
        public TypeDeclaration(Symbol name, Parameter[] genericParameters, Symbol baseType, Property[] members, TypeFlags flags)
            : base (baseType, members, flags)
        {
            Name = name;
            GenericParameters = genericParameters;
        }

        // e.g.
        // Crash 
        // Vehicle 'Crash   term
        public Symbol Name { get; }

        public Parameter[] GenericParameters { get; }
    }

    // MAP = * -> *
}


/*
type Person = {
  name: String where length > 0
}
*/
