namespace D.Expressions
{
    public class UsingStatement : IExpression
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