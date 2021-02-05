using E.Symbols;

namespace E.Expressions
{
    // (fruit: Fruit)
    // Fruit | Walrus

    public sealed class TypePattern : IExpression
    {
        public TypePattern(Symbol typeExpression, Symbol variable)
        {
            TypeExpression = typeExpression;
            VariableName = variable;
        }

        public IExpression TypeExpression { get; }

        public Symbol VariableName { get; }

        ObjectType IObject.Kind => ObjectType.TypePattern;
    }
}