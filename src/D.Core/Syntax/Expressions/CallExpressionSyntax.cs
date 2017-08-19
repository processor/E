namespace D.Syntax
{
    // |>  pipe
    // [ ] indexAccess
    // .   memberAccess
    // ()  invoke

    public class CallExpressionSyntax : SyntaxNode
    {
        public CallExpressionSyntax(SyntaxNode callee, Symbol name, ArgumentSyntax[] arguments)
        {
            Callee    = callee;
            Name      = name;
            Arguments = arguments;
        }

        // Nullable 
        public SyntaxNode Callee { get; }  // Piper
        
        public Symbol Name { get; }

        public ArgumentSyntax[] Arguments { get; }

        public bool IsPiped { get; set; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.CallExpression;
    }

    // e.g.

    // resize(100, 100)
    // String("hello")
}