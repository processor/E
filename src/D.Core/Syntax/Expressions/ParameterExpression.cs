namespace D.Syntax
{
    public class ParameterSyntax
    {
        public ParameterSyntax(
            Symbol name, 
            Symbol type = null,
            SyntaxNode defaultValue = null,
            SyntaxNode predicate = null,
            int index = 0)
        {
            Name         = name;
            Type         = type;
            DefaultValue = defaultValue;
            Predicate    = predicate;
            Index        = index;
        }

        public Symbol Name { get; }

        public int Index { get; }

        public SyntaxNode DefaultValue { get; }

        public SyntaxNode Predicate { get; }

        public Symbol Type { get; }
    }
}