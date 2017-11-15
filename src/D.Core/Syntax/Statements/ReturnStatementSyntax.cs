using System;

namespace D.Syntax
{
    public class ReturnStatementSyntax : ISyntaxNode
    {
        public ReturnStatementSyntax(ISyntaxNode expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public ISyntaxNode Expression { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ReturnStatement;
    }
}