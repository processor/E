using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax
{
    public sealed class AnnotationSyntax : ISyntaxNode
    {
        public AnnotationSyntax(Symbol name, IReadOnlyList<ArgumentSyntax> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public Symbol Name { get; }

        public IReadOnlyList<ArgumentSyntax> Arguments { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.AnnotationExpression;
    }
}