namespace E.Expressions;

// 1
public sealed class ConstantPattern : IExpression
{
    public ConstantPattern(IExpression constant)
    {
        Constant = constant;
    }
        
    public IExpression Constant { get; }

    ObjectType IObject.Kind => ObjectType.ConstantPattern;
}