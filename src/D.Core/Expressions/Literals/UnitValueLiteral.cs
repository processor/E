namespace E.Expressions;

public sealed class UnitValueLiteral(
    IExpression expression,
    string unitName,
    int power = 1) : IExpression
{
    public IExpression Expression { get; } = expression;

    public string UnitName { get; } = unitName;

    public int UnitPower { get; } = power;

    ObjectType IObject.Kind => ObjectType.UnitValue;
}

// (4/5) px
// 5 m²