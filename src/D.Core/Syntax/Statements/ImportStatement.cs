using E.Symbols;

namespace E.Syntax
{
    public sealed class UsingStatement : ISyntaxNode
    {
        public UsingStatement(Symbol[] domains)
        {
            Domains = domains;
        }

        public Symbol[] Domains { get; }

        public Symbol this[int i] => Domains[i];

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.UsingStatement;
    }
}