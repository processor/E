namespace D.Syntax
{
    public class UsingStatement : SyntaxNode
    {
        public UsingStatement(Symbol[] domains)
        {
            Domains = domains;
        }

        public Symbol[] Domains { get; }

        public Symbol this[int i] => Domains[i];

        Kind IObject.Kind => Kind.UsingStatement;
    }
}