namespace D.Syntax
{
    // 1
    public class ConstantPatternSyntax : ISyntax
    {
        public ConstantPatternSyntax(ISyntax constant)
        {
            Constant = constant;
        }
        
        public ISyntax Constant { get; }

        Kind IObject.Kind => Kind.ConstantPattern;
    }

    // 0...10
    // 0..<10       // Half open
    public class RangePatternSyntax : ISyntax
    {
        public RangePatternSyntax(ISyntax start, ISyntax end)
        {
            Start = start;
            End   = end;
        }

        public ISyntax Start { get; }

        public ISyntax End { get; }

        Kind IObject.Kind => Kind.RangePattern;
    }

    // [ a, b ]
    public class ArrayPatternSyntax : ISyntax
    {
        Kind IObject.Kind => Kind.ArrayPattern;
    }

    // { a, b }
    public class RecordPattern : ISyntax
    {
        Kind IObject.Kind => Kind.RecordPattern;
    }

    // (a, b, c)
    public class TuplePattern : ISyntax
    {
        public TuplePattern(TupleExpressionSyntax tuple)
        {
            Variables = new NamedType[tuple.Elements.Length];

            for (var i = 0; i < tuple.Elements.Length; i++)
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

    // (fruit: Fruit)
    // Fruit | Walrus

    public class TypePatternSyntax : ISyntax
    {
        public TypePatternSyntax(Symbol typeExpression, Symbol variable)
        {
            TypeExpression = typeExpression;
            VariableName = variable;
        }

        public Symbol TypeExpression { get; }

        public Symbol VariableName { get; }

        Kind IObject.Kind => Kind.TypePattern;
    }

    // _
    public class AnyPatternSyntax : ISyntax
    {
        Kind IObject.Kind => Kind.AnyPattern;
    }
}