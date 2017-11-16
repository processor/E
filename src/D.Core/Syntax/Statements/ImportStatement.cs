namespace D.Syntax
{
    public class UsingStatement : ISyntaxNode
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