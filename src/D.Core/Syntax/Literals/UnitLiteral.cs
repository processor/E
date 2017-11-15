using System;

namespace D.Syntax
{
    // 5 m^2

    public class UnitLiteralSyntax : ISyntaxNode
    {
        public UnitLiteralSyntax(ISyntaxNode expression, string unitName, int unitPower)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            UnitName = unitName;
            UnitPower = unitPower;
        }

        public ISyntaxNode Expression { get; }

        public string UnitName { get; set; }

        public int UnitPower { get; set; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.UnitLiteral;

        public override string ToString() =>  Expression.ToString() + " " + UnitName;
    }
}
