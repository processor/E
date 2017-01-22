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
            Variables = new NamedType[tuple.Elements.Count];

            for (var i = 0; i < tuple.Elements.Count; i++)
            {
                var element = tuple.Elements[i];

                if (element is NamedElement)
                {
                    var v = (NamedElement)element;

                    Variables[i] = new NamedType(v.Name, (Symbol)v.Value);
                }
                else if (element is Symbol)
                {
                    var v = (Symbol)element;

                    Variables[i] = new NamedType(v, null);
                }
            }
        }

        public NamedType[] Variables { get; }

        Kind IObject.Kind => Kind.TuplePattern;
    }

    // Fruit fruit
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