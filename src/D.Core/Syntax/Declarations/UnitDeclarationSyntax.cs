using E.Symbols;

namespace E.Syntax;

// Kelvin unit: Temperature { symbol: "K", value: 1 }
public sealed class UnitDeclarationSyntax(
    Symbol name,
    Symbol? baseType,
    ArgumentSyntax[] arguments) : ISyntaxNode
{
    public Symbol Name { get; } = name;

    public Symbol? BaseType { get; } = baseType;

    public ArgumentSyntax[] Arguments { get; } = arguments;

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