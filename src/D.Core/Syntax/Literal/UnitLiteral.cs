namespace D.Syntax
{
    // 5 m^2
    
    public class UnitLiteralSyntax : ISyntax
    {
        public UnitLiteralSyntax(ISyntax expression, string unitName, int unitPower)
        {
            Expression = expression;
            UnitName = unitName;
            UnitPower = unitPower;
        }

        public ISyntax Expression { get; }

        public string UnitName { get; set; }

        public int UnitPower { get; set; }

        Kind IObject.Kind => Kind.UnitLiteral;

        public override string ToString()
            => Expression.ToString() + " " + UnitName;
    }
}
