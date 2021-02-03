using E.Symbols;

namespace E.Syntax
{
    // Kelvin unit: Temperature { symbol: "K", value: 1 }
    public sealed class UnitDeclarationSyntax : ISyntaxNode
    {
        public UnitDeclarationSyntax(
            Symbol name,
            Symbol? baseType,
            ArgumentSyntax[] arguments)
        {
            Name = name;
            Arguments = arguments;
            BaseType = baseType;
        }

        public Symbol Name { get; }

        public Symbol? BaseType { get; }

        public ArgumentSyntax[] Arguments { get; }

        #region Property Helpers

        public ISyntaxNode? Value => GetArgumentValue("value");

        public ISyntaxNode? Symbol => GetArgumentValue("symbol");

        public ISyntaxNode? GetArgumentValue(string name)
        {
            foreach (var property in Arguments)
            {
                if (property.Name == name)
                {
                    return property.Value;
                }
            }

            return null;
        }

        #endregion

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.UnitDeclaration;
    }
}