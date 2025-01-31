namespace E.Syntax;

public sealed class QuantitySyntax(
    ISyntaxNode expression,
    string unitName,
    int unitExponent) : ISyntaxNode
{
    public ISyntaxNode Expression { get; } = expression;

    public string UnitName { get; } = unitName;

    public int UnitExponent { get; } = unitExponent;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.QuantityLiteral;

    public override string ToString() => $"{Expression} {UnitName}";
}

// 5 m^2 | m²