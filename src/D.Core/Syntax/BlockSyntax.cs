using System.Collections.Generic;

namespace E.Syntax;

public sealed class BlockSyntax : ISyntaxNode
{
    public BlockSyntax(IReadOnlyList<ISyntaxNode> statements)
    {
        Statements = statements;
    }

    public IReadOnlyList<ISyntaxNode> Statements { get; }

    public ISyntaxNode this[int index] => Statements[index];

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.Block;
}