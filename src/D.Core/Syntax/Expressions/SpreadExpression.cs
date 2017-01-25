namespace D.Syntax
{
    public class SpreadExpression : ISyntax
    {
        public SpreadExpression(ISyntax symbol)
        {
            Expression = symbol;
        }

        public ISyntax Expression { get; }

        Kind IObject.Kind => Kind.SpreadStatement;
    }
}