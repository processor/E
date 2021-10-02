namespace E.Expressions;

public sealed class ForStatement : IExpression
{
    public ForStatement(IExpression variable, IExpression generator, BlockExpression body)
    {
        VariableExpression = variable;
        GeneratorExpression = generator;
        Body = body;
    }

    // name | tuple pattern
    //  x   |    (x, x)
    public IExpression VariableExpression { get; set; }

    // variable |  range
    //    c     | 1...100
    public IExpression GeneratorExpression { get; }

    public BlockExpression Body { get; }

    ObjectType IObject.Kind => ObjectType.ForStatement;
}