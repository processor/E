namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override void VisitConstantPattern(ConstantPattern expression)
        {
            VisitConstant(expression.Constant);
        }
        
        public override void VisitTypePattern(TypePattern expression)
        {
            Emit(expression.TypeExpression.ToString());
            Emit(" ");
            Emit(expression.VariableName);
        }
    }
}
