using D.Symbols;

namespace D.Expressions
{
    public sealed class UsingStatement : IExpression
    {
        public UsingStatement(Symbol[] domains)
        {
            Domains = domains;
        }

        public Symbol[] Domains { get; }

        public Symbol this[int i] => Domains[i];

        ObjectType IObject.Kind => ObjectType.UsingStatement;
    }
}