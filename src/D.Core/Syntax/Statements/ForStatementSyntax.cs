namespace E.Syntax;

public sealed class ForStatementSyntax(
    ISyntaxNode? variableExpression,
    ISyntaxNode generatorExpression,
    BlockSyntax body) : ISyntaxNode
{

    // name | tuple pattern
    //  x   |    (x, x)
    public ISyntaxNode? VariableExpression { get; set; } = variableExpression;

    // variable |  range
    //    c     | 1...100
    public ISyntaxNode GeneratorExpression { get; } = generatorExpression;

    public BlockSyntax Body { get; } = body;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ForStatement;
}