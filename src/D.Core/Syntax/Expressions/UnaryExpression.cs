namespace E.Syntax;

public sealed class UnaryExpressionSyntax(Operator op, ISyntaxNode arg) : ISyntaxNode
{

    // Change to symbol
    public Operator Operator { get; } = op;

    public ISyntaxNode Argument { get; } = arg;

    #region ToString

    public override string ToString()
    {
        return $"{Operator.Name}({Argument})";
    }

    #endregion

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.FunctionDeclaration;
}
