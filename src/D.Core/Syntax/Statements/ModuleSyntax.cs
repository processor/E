using System;

namespace D.Syntax
{
    public class ModuleSyntax : ISyntaxNode
    {
        public ModuleSyntax(Symbol name, ISyntaxNode[] statements)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }

        public Symbol Name { get; }

        public ISyntaxNode[] Statements { get; }

        public ISyntaxNode this[int index] => Statements[index];

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ModuleStatement;
    }
}