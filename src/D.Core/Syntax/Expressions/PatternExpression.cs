using E.Symbols;

namespace E.Syntax;

// 1
public sealed class ConstantPatternSyntax(ISyntaxNode constant) : ISyntaxNode
{
    public ISyntaxNode Constant { get; } = constant;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ConstantPattern;
}

// 0...10
// 0..<10       // Half open
public sealed class RangePatternSyntax(ISyntaxNode start, ISyntaxNode end) : ISyntaxNode
{
    public ISyntaxNode Start { get; } = start;

    public ISyntaxNode End { get; } = end;

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

public sealed class TypePatternSyntax(Symbol typeExpression, Symbol variable) : ISyntaxNode
{
    public Symbol TypeExpression { get; } = typeExpression;

    public Symbol VariableName { get; } = variable;

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.TypePattern;
}

// _
public sealed class AnyPatternSyntax : ISyntaxNode
{
    SyntaxKind ISyntaxNode.Kind => SyntaxKind.AnyPattern;
}