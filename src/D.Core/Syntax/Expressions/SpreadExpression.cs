namespace D.Syntax
{
    public class SpreadExpression : SyntaxNode
    {
        public SpreadExpression(SyntaxNode symbol)
        {
            Expression = symbol;
        }

        public SyntaxNode Expression { get; }

        Kind IObject.Kind => Kind.SpreadStatement;
    }
}