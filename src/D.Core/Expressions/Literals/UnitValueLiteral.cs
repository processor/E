using System;

namespace D.Expressions
{
    public readonly struct UnitValueLiteral : IExpression
    { 
        public UnitValueLiteral(IExpression expression, string unitName, int power = 1)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            UnitName = unitName ?? throw new ArgumentNullException(nameof(unitName));
            UnitPower = power;
        }

        public IExpression Expression { get; }
       
        public string UnitName { get; }

        public int UnitPower { get; }

        Kind IObject.Kind => Kind.UnitValue;
    }
}

// (4/5) px
// 5 m²