namespace D.Compilation
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override IExpression VisitIndexAccess(IndexAccessExpression expression)
        {
            Visit(expression.Left);
            Emit("[");
            Visit(expression.Arguments[0]);
            Emit("]");

            return expression;
        }

        public override IExpression VisitMemberAccess(MemberAccessExpression expression)
        {
            Visit(expression.Left);
            Emit(".");
            Emit(ToPascalCase(expression.MemberName));

            return expression;
        }
    }
}