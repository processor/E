using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

public sealed class ModuleSyntax(
    Symbol name,
    IReadOnlyList<ISyntaxNode> statements) : ISyntaxNode
{
    public Symbol Name { get; } = name;

    public IReadOnlyList<ISyntaxNode> Statements { get; } = statements;

    public ISyntaxNode this[int index] => Statements[index];

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ModuleStatement;
}