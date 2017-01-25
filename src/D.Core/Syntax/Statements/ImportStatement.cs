namespace D.Syntax
{
    public class UsingStatement : ISyntax
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