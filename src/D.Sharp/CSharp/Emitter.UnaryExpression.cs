namespace D.Compilation
{
    using Expressions;

    public partial class CSharpEmitter
    {
        public override IExpression VisitUnary(UnaryExpression expression)
        {
            Emit(expression.Operator.Name);

            Visit(expression.Argument);

            return expression;
        }
    }
}
