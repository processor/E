using E.Symbols;

namespace E.Syntax
{
    // type | record | event

    /*
    A type { 
      a: String
      b: Number
    }     
    */
    public class TypeDefinationBase : ISyntaxNode
    {
        public TypeDefinationBase(Symbol baseType, ISyntaxNode[] members, TypeFlags flags)
        {
            BaseType = baseType;
            Members = members;
            Flags  = flags;
        }

        // : A
        public Symbol BaseType { get; }

        public TypeFlags Flags { get; }

        public ISyntaxNode[] Members { get; }

        public bool IsRecord => Flags.HasFlag(TypeFlags.Record);

        public bool IsEvent => Flags.HasFlag(TypeFlags.Event);

        public bool IsRole => Flags.HasFlag(TypeFlags.Role);

        public bool IsActor => Flags.HasFlag(TypeFlags.Actor);

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.TypeDeclaration;
    }

    public sealed class TypeDeclarationSyntax : TypeDefinationBase
    {
        public TypeDeclarationSyntax(
            Symbol name,
            ParameterSyntax[] genericParameters,
            Symbol baseType,
            ArgumentSyntax[] arguments,
            AnnotationSyntax[] annotations,
            ISyntaxNode[] members,
            TypeFlags flags = TypeFlags.None)
            : base(baseType, members, flags)
        {
            Name              = name;
            Annotations       = annotations;
            Arguments         = arguments;
            GenericParameters = genericParameters;
        }

        // e.g.
        // Crash 
        // Vehicle 'Crash 
        public Symbol Name { get; }

        public ArgumentSyntax[] Arguments { get; }

        public AnnotationSyntax[] Annotations { get; }

        public ParameterSyntax[] GenericParameters { get; }
    }

    // Las `Vegas, New `York : State class { }

    public sealed class CompoundTypeDeclarationSyntax : TypeDefinationBase
    {
        public CompoundTypeDeclarationSyntax(Symbol[] names, TypeFlags flags, Symbol baseType, ISyntaxNode[] members)
             : base(baseType, members, flags)
        {
            Names = names;
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
