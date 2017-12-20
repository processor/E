namespace D.Syntax
{
    public class ParameterSyntax
    {
        public ParameterSyntax(
            Symbol name, 
            Symbol type = null,
            ISyntaxNode defaultValue = null,
            ISyntaxNode condition = null,
            AnnotationExpressionSyntax[] annotations = null,
            int index = 0)
        {
            Name         = name;
            Type         = type;
            DefaultValue = defaultValue;
            Condition    = condition;
            Index        = index;
            Annotations  = annotations;
        }

        public Symbol Name { get; }

        public Symbol Type { get; }

        public int Index { get; }

        public ISyntaxNode DefaultValue { get; }

        public ISyntaxNode Condition { get; }

        public AnnotationExpressionSyntax[] Annotations { get; }
    }
}