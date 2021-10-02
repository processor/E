using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

// |>  pipe
// [ ] indexAccess
// .   memberAccess
// ()  invoke

public sealed class CallExpressionSyntax : ISyntaxNode
{
    public CallExpressionSyntax(
        ISyntaxNode? callee, 
        Symbol name, 
        IReadOnlyList<ArgumentSyntax> arguments)
    {
        Callee    = callee;
        Name      = name;
        Arguments = arguments;
    }

    public ISyntaxNode? Callee { get; } // The piper
        
    public Symbol Name { get; }

    public IReadOnlyList<ArgumentSyntax> Arguments { get; }

    public bool IsPiped { get; set; }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.CallExpression;
}


// resize(100, 100)
// String("hello")