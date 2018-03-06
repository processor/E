namespace D.Compilation
{
    using Expressions;

    public partial class CSharpEmitter
    {
        public override IExpression VisitConstantPattern(ConstantPattern expression)
        {
            VisitConstant(expression.Constant);

            return expression;
        }
        
        public override IExpression VisitTypePattern(TypePattern expression)
        {
            Emit(expression.TypeExpression.ToString());
            Emit(' ');
            Emit(expression.VariableName);

            return expression;
        }
    }
}