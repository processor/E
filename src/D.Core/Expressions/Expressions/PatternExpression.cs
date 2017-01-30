namespace D.Expressions
{
    // 1
    public class ConstantPattern : IExpression
    {
        public ConstantPattern(IExpression constant)
        {
            Constant = constant;
        }
        
        public IExpression Constant { get; }

        Kind IObject.Kind => Kind.ConstantPattern;
    }

    // 0...10
    // 0..<10       // Half open
    public class RangePattern : IExpression
    {
        public RangePattern(IExpression start, IExpression end)
        {
            Start = start;
            End   = end;
        }

        public IExpression Start { get; }

        public IExpression End { get; }

        Kind IObject.Kind => Kind.RangePattern;
    }

    // [ a, b ]
    public class ArrayPattern : IExpression
    {
        Kind IObject.Kind => Kind.ArrayPattern;
    }

    // { a, b }
    public class RecordPattern : IExpression
    {
        Kind IObject.Kind => Kind.RecordPattern;
    }

    // (a, b, c)
    public class TuplePattern : IExpression
    {
        public TuplePattern(TupleExpression tuple)
        {
            Variables = new NamedType[tuple.Elements.Length];

            for (var i = 0; i < tuple.Elements.Length; i++)
            {
                var element = tuple.Elements[i];

                if (element is NamedElement v)
                {
                    Variables[i] = new NamedType(v.Name, (Symbol)v.Value);
                }
                else if (element is Symbol symbol)
                {
                    Variables[i] = new NamedType(symbol, null);
                }
            }
        }

        public NamedType[] Variables { get; }

        Kind IObject.Kind => Kind.TuplePattern;
    }

    // (fruit: Fruit)
    // Fruit | Walrus

    public class TypePattern : IExpression
    {
        public TypePattern(Symbol typeExpression, Symbol variable)
        {
            TypeExpression = typeExpression;
            VariableName = variable;
        }

        public IExpression TypeExpression { get; }

        public Symbol VariableName { get; }

        Kind IObject.Kind => Kind.TypePattern;
    }

    // _
    public class AnyPattern : IExpression
    {
        Kind IObject.Kind => Kind.AnyPattern;
    }
}