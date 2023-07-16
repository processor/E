using System.Collections.Generic;

namespace E.Syntax;

public sealed class BlockSyntax(IReadOnlyList<ISyntaxNode> statements) : ISyntaxNode
{
    public IReadOnlyList<ISyntaxNode> Statements { get; } = statements;

    public ISyntaxNode this[int index] => Statements[index];

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.Block;
}