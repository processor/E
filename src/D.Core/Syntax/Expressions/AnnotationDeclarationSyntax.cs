using System;

namespace D.Syntax
{
    public class AnnotationExpressionSyntax : SyntaxNode
    {
        public AnnotationExpressionSyntax(Symbol name, ArgumentSyntax[] arguments)
        {
            #region Preconditions

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            #endregion

            Name = name;
            Arguments = arguments;
        }

        public Symbol Name { get; }

        public ArgumentSyntax[] Arguments { get; }

        Kind IObject.Kind => Kind.AnnotationExpression;
    }
}
