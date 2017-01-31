using System;

namespace D.Expressions
{
    public class TypeDeclarationBase : IExpression
    {
        public TypeDeclarationBase(Symbol baseType, Property[] members, TypeFlags flags = TypeFlags.None)
        {
            BaseType = baseType;
            Members = members ?? throw new ArgumentNullException(nameof(members));
            Flags = flags;
        }

        // : A
        public Symbol BaseType { get; }

        public TypeFlags Flags { get; }

        public Property[] Members { get; }

        public bool IsRecord 
            => Flags.HasFlag(TypeFlags.Record);

        public bool IsEvent 
            => Flags.HasFlag(TypeFlags.Event);

        Kind IObject.Kind => Kind.TypeDeclaration;
    }

    public class TypeDeclaration : TypeDeclarationBase
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

    public class CompoundTypeDeclaration : TypeDeclarationBase
    {
        public CompoundTypeDeclaration(Symbol[] names, TypeFlags flags, Symbol baseType, Property[] properties)
             : base(baseType, properties, flags)
        {
            Names = names;
        }

        public Symbol[] Names { get; }
    }    

    // MAP = * -> *
}


/*
type Person = {
  name: String where length > 0
}
*/
