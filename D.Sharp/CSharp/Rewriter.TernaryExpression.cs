namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override void VisitTernary(TernaryExpression expression)
        {
            Visit(expression.Condition);

            Emit(" ? ");

            Visit(expression.Left);

            Emit(" : ");

            Visit(expression.Right);
        }
    }
}