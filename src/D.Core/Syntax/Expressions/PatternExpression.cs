using D.Symbols;

namespace D.Syntax
{
    // 1
    public sealed class ConstantPatternSyntax : ISyntaxNode
    {
        public ConstantPatternSyntax(ISyntaxNode constant)
        {
            Constant = constant;
        }
        
        public ISyntaxNode Constant { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ConstantPattern;
    }

    // 0...10
    // 0..<10       // Half open
    public sealed class RangePatternSyntax : ISyntaxNode
    {
        public RangePatternSyntax(ISyntaxNode start, ISyntaxNode end)
        {
            Start = start;
            End   = end;
        }

        public ISyntaxNode Start { get; }

        public ISyntaxNode End { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.RangePattern;
    }

    // [ a, b ]
    public sealed class ArrayPatternSyntax : ISyntaxNode
    {
        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ArrayPattern;
    }

    // { a, b }
    public sealed class ObjectPatternSyntax : ISyntaxNode
    {
        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ObjectPattern;
    }

    // (a, b, c)
    // (a: 1, b: 2, c: 3 }
    public sealed class TuplePatternSyntax : ISyntaxNode
    {
        public TuplePatternSyntax(TupleExpressionSyntax tuple)
        {
            Variables = new TupleElementSyntax[tuple.Elements.Length];

            for (var i = 0; i < tuple.Elements.Length; i++)
            {
                var element = tuple.Elements[i];

                if (element is TupleElementSyntax namedElement)
                {
                    Variables[i] = new TupleElementSyntax(namedElement.Name, namedElement.Value);
                }
                else if (element is Symbol name)
                {
                    Variables[i] = new TupleElementSyntax(name, null);
                }
            }
        }

        public TupleElementSyntax[] Variables { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.TuplePattern;
    }

    // (fruit: Fruit)
    // Fruit | Walrus

    public sealed class TypePatternSyntax : ISyntaxNode
    {
        public TypePatternSyntax(Symbol typeExpression, Symbol variable)
        {
            TypeExpression = typeExpression;
            VariableName = variable;
        }

        public Symbol TypeExpression { get; }

        public Symbol VariableName { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.TypePattern;
    }

    // _
    public sealed class AnyPatternSyntax : ISyntaxNode
    {
        SyntaxKind ISyntaxNode.Kind => SyntaxKind.AnyPattern;
    }
}