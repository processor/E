namespace D.Syntax
{
    // |>  pipe
    // [ ] indexAccess
    // .   memberAccess
    // ()  invoke

    public class CallExpressionSyntax : ISyntaxNode
    {
        public CallExpressionSyntax(ISyntaxNode callee, Symbol name, ArgumentSyntax[] arguments)
        {
            Callee    = callee;
            Name      = name;
            Arguments = arguments;
        }

        // Nullable 
        public ISyntaxNode Callee { get; }  // Piper
        
        public Symbol Name { get; }

        public ArgumentSyntax[] Arguments { get; }

        public bool IsPiped { get; set; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.CallExpression;
    }
}

// e.g.

// resize(100, 100)
// String("hello")