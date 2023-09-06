namespace E.Compilation;

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
        Visit(expression.TypeExpression);
        Emit(' ');
        Emit(expression.VariableName.Name);

        return expression;
    }
}
