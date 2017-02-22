using System;

namespace D.Syntax
{
    public class ModuleSyntax : SyntaxNode
    {
        public ModuleSyntax(Symbol name, SyntaxNode[] statements)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }

        public Symbol Name { get; }

        public SyntaxNode[] Statements { get; }

        public SyntaxNode this[int index] => Statements[index];

        Kind IObject.Kind => Kind.ModuleStatement;
    }
}