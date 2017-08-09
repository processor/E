using System;

namespace D.Syntax
{
    // type | record | event

    /*
    A type { 
      a: String
      b: Number
    }     
    */
    public class TypeDefinationBase : SyntaxNode
    {
        public TypeDefinationBase(Symbol baseType, PropertyDeclarationSyntax[] members, TypeFlags flags)
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

        Kind IObject.Kind => Kind.TypeDeclaration;
    }

    public class TypeDeclarationSyntax : TypeDefinationBase
    {
        public TypeDeclarationSyntax(
            Symbol name,
            ParameterSyntax[] genericParameters,
            Symbol baseType,
            AnnotationExpressionSyntax[] annotations,
            PropertyDeclarationSyntax[] members,
            TypeFlags flags = TypeFlags.None)
            : base(baseType, members, flags)
        {
            Name = name;
            Annotations = annotations;
            GenericParameters = genericParameters;
        }

        // e.g.
        // Crash 
        // Vehicle 'Crash   term
        public Symbol Name { get; }

        public AnnotationExpressionSyntax[] Annotations { get; }

        public ParameterSyntax[] GenericParameters { get; }
    }

    public class CompoundTypeDeclarationSyntax : TypeDefinationBase
    {
        public CompoundTypeDeclarationSyntax(Symbol[] names, TypeFlags flags, Symbol baseType, PropertyDeclarationSyntax[] properties)
             : base(baseType, properties, flags)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
        }

        public Symbol[] Names { get; }
    }


    public class PropertyDeclarationSyntax : SyntaxNode
    {
        public PropertyDeclarationSyntax(string name, TypeSymbol type, ObjectFlags flags)
        {
            Name  = name ?? throw new ArgumentNullException(nameof(name));
            Type  = type;
            Flags = flags;
        }

        public string Name { get; }

        // mutable
        public ObjectFlags Flags { get; }

        // String
        // String | Number
        // A & B
        public TypeSymbol Type { get; }

        Kind IObject.Kind => Kind.Property;
    }

    // MAP = * -> *
}

/*
Person type {
  name: String where length > 0
}
*/
