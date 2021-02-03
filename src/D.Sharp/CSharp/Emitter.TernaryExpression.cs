namespace E.Compilation
{
    using Expressions;

    public partial class CSharpEmitter
    {
        public override IExpression VisitTernary(TernaryExpression expression)
        {
            Visit(expression.Condition);

            Emit(" ? ");

            Visit(expression.Left);

            Emit(" : ");

            Visit(expression.Right);

            return expression;
        }
    }
}