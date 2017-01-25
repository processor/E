namespace D.Syntax
{
    // type | record | event

    public class TypeDefinationBase : ISyntax
    {
        public TypeDefinationBase(Symbol baseType, TypeFlags flags, PropertyDeclarationSyntax[] members)
        {
            BaseType = baseType;
            Members = members;
            Flags = flags;
        }

        // : A
        public Symbol BaseType { get; }

        public TypeFlags Flags { get; }

        public PropertyDeclarationSyntax[] Members { get; }

        public bool IsRecord 
            => Flags.HasFlag(TypeFlags.Record);

        public bool IsEvent 
            => Flags.HasFlag(TypeFlags.Event);

        public bool IsPrimitive
            => Flags.HasFlag(TypeFlags.Primitive);

        Kind IObject.Kind => Kind.TypeDeclaration;
    }

    public class TypeDeclarationSyntax : TypeDefinationBase
    {
        public TypeDeclarationSyntax(Symbol name, ParameterSyntax[] genericParameters, Symbol baseType, PropertyDeclarationSyntax[] members, TypeFlags flags)
            : base (baseType, flags, members)
        {
            Name = name;
            GenericParameters = genericParameters;
        }

        // e.g.
        // Crash 
        // Vehicle 'Crash   term
        public Symbol Name { get; }

        public ParameterSyntax[] GenericParameters { get; }
    }

    public class CompoundTypeDeclarationSyntax : TypeDefinationBase
    {
        public CompoundTypeDeclarationSyntax(Symbol[] names, TypeFlags flags, Symbol baseType, PropertyDeclarationSyntax[] properties)
             : base(baseType, flags, properties)
        {
            Names = names;
        }

        public Symbol[] Names { get; }
    }

    public struct PropertyDeclarationSyntax
    {
        public PropertyDeclarationSyntax(string name, Symbol type, bool mutable = false)
        {
            Name = name;
            Type = type;
            IsMutable = mutable;
        }

        public string Name { get; }

        public bool IsMutable { get; }

        // String
        // String | Number
        // A & B
        public Symbol Type { get; }
    }

    // MAP = * -> *

    public enum TypeFlags
    {
        None = 0,
        Primitive,
        Record = 1 << 3,
        Event = 1 << 4
    }
}


/*
type Person = {
  name: String where length > 0
}
*/
