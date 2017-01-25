namespace D.Syntax
{
    // px unit
    // m unit

    // Radian : Angle = 1

    public class UnitDeclaration : ISyntax
    {
        public UnitDeclaration(Symbol name, Symbol baseUnit, ISyntax expression)
        {
            Name = name;
            BaseUnit = BaseUnit;
            Expression = expression;
        }

        public Symbol Name { get; }

        public Symbol BaseUnit { get; }

        public ISyntax Expression { get; }

        Kind IObject.Kind => Kind.UnitDeclaration;
    }
}