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

        SyntaxKind SyntaxNode.Kind => SyntaxKind.ConstantPattern;
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

        SyntaxKind SyntaxNode.Kind => SyntaxKind.RangePattern;
    }

    // [ a, b ]
    public class ArrayPatternSyntax : SyntaxNode
    {
        SyntaxKind SyntaxNode.Kind => SyntaxKind.ArrayPattern;
    }

    // { a, b }
    public class ObjectPatternSyntax : SyntaxNode
    {
        SyntaxKind SyntaxNode.Kind => SyntaxKind.ObjectPattern;
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
                    Variables[i] = new NamedElementSyntax(namedElement.Name, namedElement.Value);
                }
                else if (element is Symbol name)
                {
                    Variables[i] = new NamedElementSyntax(name, null);
                }
            }
        }

        public NamedElementSyntax[] Variables { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.TuplePattern;
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

        SyntaxKind SyntaxNode.Kind => SyntaxKind.TypePattern;
    }

    // _
    public class AnyPatternSyntax : SyntaxNode
    {
        SyntaxKind SyntaxNode.Kind => SyntaxKind.AnyPattern;
    }
}