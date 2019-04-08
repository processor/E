using System;

namespace D.Syntax
{
    public class YieldStatementSyntax : ISyntaxNode
    {
        public YieldStatementSyntax(ISyntaxNode expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public ISyntaxNode Expression { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.YieldStatement;
    }
}