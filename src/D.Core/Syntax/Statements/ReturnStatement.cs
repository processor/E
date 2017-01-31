using System;

namespace D.Syntax
{
    public class ReturnStatementSyntax : SyntaxNode
    {
        public ReturnStatementSyntax(SyntaxNode expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public SyntaxNode Expression { get; }

        Kind IObject.Kind => Kind.ReturnStatement;
    }
}