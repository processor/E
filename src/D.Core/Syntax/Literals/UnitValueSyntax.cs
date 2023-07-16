namespace E.Syntax;

public sealed class UnitValueSyntax(ISyntaxNode expression, string unitName, int unitPower) : ISyntaxNode
{
    public ISyntaxNode Expression { get; } = expression;

    public string UnitName { get; } = unitName;

    public int UnitPower { get; } = unitPower;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.UnitValueLiteral;

    public override string ToString() => $"{Expression} {UnitName}";
}

// 5 m^2 | m²