using D.Symbols;

namespace D.Syntax
{
    public sealed class ModuleSyntax : ISyntaxNode
    {
        public ModuleSyntax(Symbol name, ISyntaxNode[] statements)
        {
            Name = name;
            Statements = statements;
        }

        public Symbol Name { get; }

        public ISyntaxNode[] Statements { get; }

        public ISyntaxNode this[int index] => Statements[index];

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ModuleStatement;
    }
}