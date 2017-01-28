using System;

namespace D.Syntax
{
    public class ReturnStatementSyntax : SyntaxNode
    {
        public ReturnStatementSyntax(SyntaxNode expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Expression = expression;
        }

        public SyntaxNode Expression { get; }

        Kind IObject.Kind => Kind.ReturnStatement;
    }
}