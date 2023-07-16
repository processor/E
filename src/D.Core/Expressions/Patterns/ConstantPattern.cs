namespace E.Expressions;

// 1
public sealed class ConstantPattern(IExpression constant) : IExpression
{
    public IExpression Constant { get; } = constant;

    ObjectType IObject.Kind => ObjectType.ConstantPattern;
}