using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

// |>  pipe
// [ ] indexAccess
// .   memberAccess
// ()  invoke

public sealed class CallExpressionSyntax(
    ISyntaxNode? callee,
    Symbol name,
    ArgumentSyntax[] arguments) : ISyntaxNode
{
    public ISyntaxNode? Callee { get; } = callee;

    public Symbol Name { get; } = name;

    public ArgumentSyntax[] Arguments { get; } = arguments;

    public bool IsPiped { get; set; }

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.CallExpression;
}


// resize(100, 100)
// String("hello")