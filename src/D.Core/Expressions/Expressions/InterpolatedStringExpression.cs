using System;

namespace D.Expressions
{
    // $"{expression}text"
    public class InterpolatedStringExpression : IExpression
    {
        public InterpolatedStringExpression(IExpression[] children)
        {
            Children = children ?? throw new ArgumentNullException(nameof(children));
        }

        public IExpression[] Children { get; }

        public IExpression this[int index] => Children[index];

        Kind IObject.Kind => Kind.InterpolatedStringExpression;
    }
}