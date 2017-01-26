namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override IExpression VisitConstantPattern(ConstantPattern expression)
        {
            VisitConstant(expression.Constant);

            return expression;
        }
        
        public override IExpression VisitTypePattern(TypePattern expression)
        {
            Emit(expression.TypeExpression.ToString());
            Emit(" ");
            Emit(expression.VariableName);

            return expression;
        }
    }
}
