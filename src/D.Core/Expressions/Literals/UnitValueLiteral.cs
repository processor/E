namespace D.Expressions
{
    public sealed class UnitValueLiteral : IExpression
    { 
        public UnitValueLiteral(IExpression expression, string unitName, int power = 1)
        {
            Expression = expression;
            UnitName = unitName;
            UnitPower  = power;
        }

        public IExpression Expression { get; }
       
        public string UnitName { get; }

        public int UnitPower { get; }

        ObjectType IObject.Kind => ObjectType.UnitValue;
    }
}

// (4/5) px
// 5 m²