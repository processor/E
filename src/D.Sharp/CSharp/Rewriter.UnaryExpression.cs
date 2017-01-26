namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override IExpression VisitUnary(UnaryExpression expression)
        {
            Emit(expression.Operator.Name);

            Visit(expression.Argument);

            return expression;
        }
    }
}
