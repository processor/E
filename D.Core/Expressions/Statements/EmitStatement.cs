namespace D.Expressions
{
    public class EmitStatement : IExpression
    {
        public EmitStatement(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression { get; }

        Kind IObject.Kind => Kind.EmitStatement;
    }
}