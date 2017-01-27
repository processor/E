namespace D.Syntax
{
    public class ElseStatementSyntax : ISyntax
    {
        public ElseStatementSyntax(BlockExpressionSyntax body)
        {
            Body = body;
        }

        public BlockExpressionSyntax Body { get; }

        Kind IObject.Kind => Kind.ElseStatement;
    }
}