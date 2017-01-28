using System;

namespace D.Expressions
{
    public class UnitLiteral : IExpression
    { 
        public UnitLiteral(IExpression expression, string unitName, int power = 1)
        {
            #region Preconditions

            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (unitName == null)
                throw new ArgumentNullException(nameof(unitName));

            #endregion

            Expression = expression;
            UnitName = unitName;
            UnitPower = power;
        }

        public IExpression Expression { get; }
       
        public string UnitName { get; }

        public int UnitPower { get; }

        Kind IObject.Kind => Kind.UnitLiteral;
    }
}

// (4/5) px