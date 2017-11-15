namespace D.Syntax
{
    public class ParameterSyntax
    {
        public ParameterSyntax(
            Symbol name, 
            Symbol type = null,
            ISyntaxNode defaultValue = null,
            ISyntaxNode predicate = null,
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

        public ISyntaxNode DefaultValue { get; }

        public ISyntaxNode Predicate { get; }

        public Symbol Type { get; }
    }
}