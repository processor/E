using D.Symbols;

namespace D.Syntax
{
    public sealed class AnnotationSyntax : ISyntaxNode
    {
        public AnnotationSyntax(Symbol name, ArgumentSyntax[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public Symbol Name { get; }

        public ArgumentSyntax[] Arguments { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.AnnotationExpression;
    }
}