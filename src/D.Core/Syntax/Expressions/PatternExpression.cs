namespace D.Syntax
{
    // 1
    public class ConstantPatternSyntax : SyntaxNode
    {
        public ConstantPatternSyntax(SyntaxNode constant)
        {
            Constant = constant;
        }
        
        public SyntaxNode Constant { get; }

        Kind IObject.Kind => Kind.ConstantPattern;
    }

    // 0...10
    // 0..<10       // Half open
    public class RangePatternSyntax : SyntaxNode
    {
        public RangePatternSyntax(SyntaxNode start, SyntaxNode end)
        {
            Start = start;
            End   = end;
        }

        public SyntaxNode Start { get; }

        public SyntaxNode End { get; }

        Kind IObject.Kind => Kind.RangePattern;
    }

    // [ a, b ]
    public class ArrayPatternSyntax : SyntaxNode
    {
        Kind IObject.Kind => Kind.ArrayPattern;
    }

    // { a, b }
    public class ObjectPatternSyntax : SyntaxNode
    {
        Kind IObject.Kind => Kind.ObjectPattern;
    }

    // (a, b, c)
    // (a: 1, b: 2, c: 3 }
    public class TuplePatternSyntax : SyntaxNode
    {
        public TuplePatternSyntax(TupleExpressionSyntax tuple)
        {
            Variables = new NamedElementSyntax[tuple.Elements.Length];

            for (var i = 0; i < tuple.Elements.Length; i++)
            {
                var element = tuple.Elements[i];

                if (element is NamedElementSyntax namedElement)
                {
                    Variables[i] = new NamedElementSyntax(namedElement.Name, (Symbol)namedElement.Value);
                }
                else if (element is Symbol name)
                {
                    Variables[i] = new NamedElementSyntax(name, null);
                }
            }
        }

        public NamedElementSyntax[] Variables { get; }

        Kind IObject.Kind => Kind.TuplePattern;
    }

    // (fruit: Fruit)
    // Fruit | Walrus

    public class TypePatternSyntax : SyntaxNode
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
    public class AnyPatternSyntax : SyntaxNode
    {
        Kind IObject.Kind => Kind.AnyPattern;
    }
}