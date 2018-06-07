using System;

namespace D.Syntax
{
    // 5 m^2 | m²

    public class UnitValueSyntax : ISyntaxNode
    {
        public UnitValueSyntax(ISyntaxNode expression, string unitName, int unitPower)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
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