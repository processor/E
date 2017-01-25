namespace D.Syntax
{
    // $"{expression}text"

    public class InterpolatedStringExpressionSyntax : ISyntax
    {
        public InterpolatedStringExpressionSyntax(ISyntax[] children)
        {
            Children = children;
        }

        public ISyntax[] Children { get; }

        public ISyntax this[int index] => Children[index];

        Kind IObject.Kind => Kind.InterpolatedStringExpression;
    }
}