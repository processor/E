namespace E.Expressions;

public sealed class ForStatement(
    IExpression variable,
    IExpression generator,
    BlockExpression body) : IExpression
{

    // name | tuple pattern
    //  x   |    (x, x)
    public IExpression VariableExpression { get; set; } = variable;

    // variable |  range
    //    c     | 1...100
    public IExpression GeneratorExpression { get; } = generator;

    public BlockExpression Body { get; } = body;

    ObjectType IObject.Kind => ObjectType.ForStatement;
}