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
        public TypeDefinationBase(Symbol baseType, SyntaxNode[] members, TypeFlags flags)
        {
            BaseType = baseType;
            Members = members ?? throw new ArgumentNullException(nameof(members));
            Flags  = flags;
        }

        // : A
        public Symbol BaseType { get; }

        public TypeFlags Flags { get; }

        public SyntaxNode[] Members { get; }

        public bool IsRecord => Flags.HasFlag(TypeFlags.Record);

        public bool IsEvent => Flags.HasFlag(TypeFlags.Event);

        SyntaxKind SyntaxNode.Kind => SyntaxKind.TypeDeclaration;
    }

    public class TypeDeclarationSyntax : TypeDefinationBase
    {
        public TypeDeclarationSyntax(
            Symbol name,
            ParameterSyntax[] genericParameters,
            Symbol baseType,
            AnnotationExpressionSyntax[] annotations,
            SyntaxNode[] members,
            TypeFlags flags = TypeFlags.None)
            : base(baseType, members, flags)
        {
            Name = name;
            Annotations = annotations;
            GenericParameters = genericParameters;
        }

        // e.g.
        // Crash 
        // Vehicle 'Crash  term
        public Symbol Name { get; }

        public AnnotationExpressionSyntax[] Annotations { get; }

        public ParameterSyntax[] GenericParameters { get; }
    }


    // Las `Vegas, New `York : State class { }

    public class CompoundTypeDeclarationSyntax : TypeDefinationBase
    {
        public CompoundTypeDeclarationSyntax(Symbol[] names, TypeFlags flags, Symbol baseType, SyntaxNode[] members)
             : base(baseType, members, flags)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
        }

        public Symbol[] Names { get; }
    }
    

    // MAP = * -> *
}

/*
Person type {
  name: String where length > 0
}
*/
