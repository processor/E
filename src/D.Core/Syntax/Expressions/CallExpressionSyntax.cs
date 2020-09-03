using D.Symbols;

namespace D.Syntax
{
    // |>  pipe
    // [ ] indexAccess
    // .   memberAccess
    // ()  invoke

    public sealed class CallExpressionSyntax : ISyntaxNode
    {
        public CallExpressionSyntax(ISyntaxNode? callee, Symbol name, ArgumentSyntax[] arguments)
        {
            Callee    = callee;
            Name      = name;
            Arguments = arguments;
        }

        public ISyntaxNode? Callee { get; } // The piper
        
        public Symbol Name { get; }

        public ArgumentSyntax[] Arguments { get; }

        public bool IsPiped { get; set; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.CallExpression;
    }
}

// resize(100, 100)
// String("hello")