using System;

namespace D.Syntax
{
    public class IfStatementSyntax : ISyntaxNode
    {
        public IfStatementSyntax(ISyntaxNode condition, BlockSyntax body, ISyntaxNode elseBranch)
        {
            Condition  = condition ?? throw new ArgumentNullException(nameof(condition));
            Body       = body ?? throw new ArgumentNullException(nameof(body));
            ElseBranch = elseBranch;
        }

        public ISyntaxNode Condition { get; }

        public BlockSyntax Body { get; }

        // Else | ElseIf
        public ISyntaxNode ElseBranch { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.IfStatement;
    }
}