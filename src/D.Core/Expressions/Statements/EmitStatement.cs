namespace D.Expressions
{
    public sealed class EmitStatement : IExpression
    {
        public EmitStatement(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression { get; }

        ObjectType IObject.Kind => ObjectType.EmitStatement;
    }
}