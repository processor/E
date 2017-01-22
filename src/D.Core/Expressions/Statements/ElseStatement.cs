namespace D.Expressions
{
    public class ElseStatement : IExpression
    {
        public ElseStatement(BlockStatement body)
        {
            Body = body;
        }

        public BlockStatement Body { get; }

        Kind IObject.Kind => Kind.ElseStatement;
    }
}