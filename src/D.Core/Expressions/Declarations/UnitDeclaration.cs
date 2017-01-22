namespace D.Expressions
{
    // px unit
    // m unit

    // Radian : Angle = 1

    public class UnitDeclaration : IExpression
    {
        public UnitDeclaration(Symbol name, Symbol baseUnit, IExpression expression)
        {
            Name = name;
            BaseUnit = BaseUnit;
            Expression = expression;
        }

        public Symbol Name { get; }

        public Symbol BaseUnit { get; }

        public IExpression Expression { get; }

        Kind IObject.Kind => Kind.UnitDeclaration;
    }
}