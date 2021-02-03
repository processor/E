using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax
{
    public sealed class ModuleSyntax : ISyntaxNode
    {
        public ModuleSyntax(Symbol name, IReadOnlyList<ISyntaxNode> statements)
        {
            Name = name;
            Statements = statements;
        }

        public Symbol Name { get; }

        public IReadOnlyList<ISyntaxNode> Statements { get; }

        public ISyntaxNode this[int index] => Statements[index];

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ModuleStatement;
    }
}