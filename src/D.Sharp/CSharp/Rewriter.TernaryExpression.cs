namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
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