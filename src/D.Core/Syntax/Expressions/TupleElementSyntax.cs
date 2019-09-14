namespace D.Syntax
{
    // a: 100
    public sealed class TupleElementSyntax : ISyntaxNode
    {
        public TupleElementSyntax(Symbol name, ISyntaxNode value)
        {
            Name = name;
            Value = value;
        }

        public Symbol Name { get; }

        // type or constant
        public ISyntaxNode Value { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.TupleElement;
    }
}
