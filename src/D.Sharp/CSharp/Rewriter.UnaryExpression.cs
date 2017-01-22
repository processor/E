namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override void VisitUnary(UnaryExpression be)
        {
            Emit(be.Operator.Name);

            Visit(be.Argument);
        }
    }
}
