namespace D.Expressions
{
    // type | record | event

    public class TypeDefinationBase : IExpression
    {
        public TypeDefinationBase(Symbol baseType, TypeFlags flags, PropertyDeclaration[] members)
        {
            BaseType = baseType;
            Members = members;
            Flags = flags;
        }

        // : A
        public Symbol BaseType { get; }

        public TypeFlags Flags { get; }

        public PropertyDeclaration[] Members { get; }

        public bool IsRecord 
            => Flags.HasFlag(TypeFlags.Record);

        public bool IsEvent 
            => Flags.HasFlag(TypeFlags.Event);

        public bool IsPrimitive
            => Flags.HasFlag(TypeFlags.Primitive);

        Kind IObject.Kind => Kind.TypeDeclaration;
    }

    public class TypeDeclaration : TypeDefinationBase
    {
        public TypeDeclaration(Symbol name, Parameter[] genericParameters, Symbol baseType, PropertyDeclaration[] members, TypeFlags flags)
            : base (baseType, flags, members)
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

    public class CompoundTypeDeclaration : TypeDefinationBase
    {
        public CompoundTypeDeclaration(Symbol[] names, TypeFlags flags, Symbol baseType, PropertyDeclaration[] properties)
             : base(baseType, flags, properties)
        {
            Names = names;
        }

        public Symbol[] Names { get; }
    }

    public struct PropertyDeclaration
    {
        public PropertyDeclaration(string name, Symbol type, bool mutable = false)
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
