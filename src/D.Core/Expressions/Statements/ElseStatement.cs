namespace D.Expressions
{
    public class ElseStatement : IExpression
    {
        public ElseStatement(BlockExpression body)
        {
            Body = body;
        }

        public BlockExpression Body { get; }

        Kind IObject.Kind => Kind.ElseStatement;
    }
}