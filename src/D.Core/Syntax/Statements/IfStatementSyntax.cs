using System;

namespace D.Syntax
{
    public class IfStatementSyntax : SyntaxNode
    {
        public IfStatementSyntax(SyntaxNode condition, BlockSyntax body, SyntaxNode elseBranch)
        {
            Condition  = condition ?? throw new ArgumentNullException(nameof(condition));
            Body       = body ?? throw new ArgumentNullException(nameof(body));
            ElseBranch = elseBranch;
        }

        public SyntaxNode Condition { get; }

        public BlockSyntax Body { get; }

        // Else | ElseIf
        public SyntaxNode ElseBranch { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.IfStatement;
    }
}