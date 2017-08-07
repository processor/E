namespace D.Syntax
{
    public class ArgumentSyntax : SyntaxNode
    {
        public ArgumentSyntax(SyntaxNode value)
        {
            Value = value;
        }

        public ArgumentSyntax(string name, SyntaxNode value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public SyntaxNode Value { get; }

        public Kind Kind => Kind.Argument;
    }
}