namespace D.Syntax
{
    public sealed class ArgumentSyntax
    {
        public ArgumentSyntax(ISyntaxNode value)
        {
            Value = value;
        }

        public ArgumentSyntax(Symbol? name, ISyntaxNode value)
        {
            Name = name;
            Value = value;
        }

        public Symbol? Name { get; }

        public ISyntaxNode Value { get; }
    }
}