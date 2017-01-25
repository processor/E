using System;

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
        public TypeDeclarationSyntax(
            Symbol name,
            ParameterSyntax[] genericParameters,
            Symbol baseType,
            PropertyDeclarationSyntax[] members,
            NamedMetadataSyntax[] attributes,
            TypeFlags flags = TypeFlags.None)
            : base(baseType, flags, members)
        {
            Name = name;
            Attributes = attributes;
            GenericParameters = genericParameters;
        }

        // e.g.
        // Crash 
        // Vehicle 'Crash   term
        public Symbol Name { get; }

        public NamedMetadataSyntax[] Attributes { get; }

        public ParameterSyntax[] GenericParameters { get; }
    }


    public class NamedMetadataSyntax : ISyntax
    {
        public NamedMetadataSyntax(Symbol name, ISyntax value)
        {
            #region Preconditions

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            #endregion

            Name = name;
            Value = value;
        }

        public Symbol Name { get; }

        public ISyntax Value { get; }

        Kind IObject.Kind => Kind.NamedMetadata;
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

    public class PropertyDeclarationSyntax : ISyntax
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

        Kind IObject.Kind => Kind.Property;
    }

    // MAP = * -> *

  
}


/*
type Person = {
  name: String where length > 0
}
*/
