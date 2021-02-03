using E.Symbols;

namespace E.Expressions
{
    // 1
    public sealed class ConstantPattern : IExpression
    {
        public ConstantPattern(IExpression constant)
        {
            Constant = constant;
        }
        
        public IExpression Constant { get; }

        ObjectType IObject.Kind => ObjectType.ConstantPattern;
    }

    // 0...10
    // 0..<10       // Half open
    public sealed class RangePattern : IExpression
    {
        public RangePattern(IExpression start, IExpression end)
        {
            Start = start;
            End   = end;
        }

        public IExpression Start { get; }

        public IExpression End { get; }

        ObjectType IObject.Kind => ObjectType.RangePattern;
    }

    // [ a, b ]
    public sealed class ArrayPattern : IExpression
    {
        ObjectType IObject.Kind => ObjectType.ArrayPattern;
    }

    // { a, b }
    public sealed class ObjectPattern : IExpression
    {
        ObjectType IObject.Kind => ObjectType.ObjectPattern;
    }

    // (i32, i32)
    // (a: 1, b: 2, c: 3)
    public sealed class TuplePattern : IExpression
    {
        public TuplePattern(TupleExpression tuple)
        {
            Variables = new TupleElement[tuple.Elements.Length];

            for (var i = 0; i < tuple.Elements.Length; i++)
            {
                var element = tuple.Elements[i];

                if (element is TupleElement v)
                {
                    Variables[i] = new TupleElement(v.Name, v.Value);
                }
                else if (element is Symbol symbol)
                {
                    Variables[i] = new TupleElement(symbol, null);
                }
            }
        }

        public TupleElement[] Variables { get; }

        ObjectType IObject.Kind => ObjectType.TuplePattern;
    }

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

    // _
    public sealed class AnyPattern : IExpression
    {
        ObjectType IObject.Kind => ObjectType.AnyPattern;
    }
}