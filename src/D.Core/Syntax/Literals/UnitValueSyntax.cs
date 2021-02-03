namespace E.Syntax
{
    public sealed class UnitValueSyntax : ISyntaxNode
    {
        public UnitValueSyntax(ISyntaxNode expression, string unitName, int unitPower)
        {
            Expression = expression;
            UnitName = unitName;
            UnitPower = unitPower;
        }

        public ISyntaxNode Expression { get; }

        public string UnitName { get; }

        public int UnitPower { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.UnitValueLiteral;

        public override string ToString() => Expression.ToString() + " " + UnitName;
    }
}

// 5 m^2 | m²