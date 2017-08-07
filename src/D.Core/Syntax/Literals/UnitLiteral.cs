using System;

namespace D.Syntax
{
    // 5 m^2
    
    public class UnitLiteralSyntax : SyntaxNode
    {
        public UnitLiteralSyntax(SyntaxNode expression, string unitName, int unitPower)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            UnitName = unitName;
            UnitPower = unitPower;
        }

        public SyntaxNode Expression { get; }

        public string UnitName { get; set; }

        public int UnitPower { get; set; }

        Kind IObject.Kind => Kind.UnitLiteral;

        public override string ToString() => 
            Expression.ToString() + " " + UnitName;
    }
}
