namespace D.Syntax
{
    public class ParameterSyntax
    {
        public ParameterSyntax(
            Symbol name, 
            Symbol type = null,
            ISyntaxNode defaultValue = null,
            ISyntaxNode condition = null,
            int index = 0)
        {
            Name         = name;
            Type         = type;
            DefaultValue = defaultValue;
            Condition    = condition;
            Index        = index;
        }

        public Symbol Name { get; }

        public int Index { get; }

        public ISyntaxNode DefaultValue { get; }

        public ISyntaxNode Condition { get; }

        public Symbol Type { get; }
    }
}