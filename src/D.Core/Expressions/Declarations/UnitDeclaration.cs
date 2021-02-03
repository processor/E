using E.Symbols;

namespace E.Expressions
{
    // rad unit : Angle @name("Radian") = 1

    public sealed class UnitDeclaration : IExpression
    {
        public UnitDeclaration(Symbol name, Symbol baseType, IExpression expression)
        {
            Name = name;
            BaseType = baseType;
            Expression = expression;
        }

        public Symbol Name { get; }

        public Symbol BaseType { get; }

        public Symbol? Symbol { get; }

        // Arithmetic relationship to another unit
        
        // ml = cm**3

        public IExpression Expression { get; }

        ObjectType IObject.Kind => ObjectType.UnitDeclaration;
    }
}