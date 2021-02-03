using System.Collections.Generic;

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
    public abstract class TypeDefinationBase : ISyntaxNode
    {
        public TypeDefinationBase(Symbol baseType, IReadOnlyList<ISyntaxNode> members, TypeFlags flags)
        {
            BaseType = baseType;
            Members = members;
            Flags  = flags;
        }

        // : A
        public Symbol BaseType { get; }

        public TypeFlags Flags { get; }

        public IReadOnlyList<ISyntaxNode> Members { get; }

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
            IReadOnlyList<ParameterSyntax> genericParameters,
            Symbol baseType,
            IReadOnlyList<ArgumentSyntax> arguments,
            AnnotationSyntax[] annotations,
            IReadOnlyList<ISyntaxNode> members,
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

        public IReadOnlyList<ArgumentSyntax> Arguments { get; }

        public AnnotationSyntax[] Annotations { get; }

        public IReadOnlyList<ParameterSyntax> GenericParameters { get; }
    }

    // Las `Vegas, New `York : State class { }

    public sealed class CompoundTypeDeclarationSyntax : TypeDefinationBase
    {
        public CompoundTypeDeclarationSyntax(Symbol[] names, TypeFlags flags, Symbol baseType, IReadOnlyList<ISyntaxNode> members)
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
