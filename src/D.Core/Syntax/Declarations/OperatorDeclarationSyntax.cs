using System.Collections.Generic;

using E.Symbols;

namespace E.Syntax;

public sealed class OperatorDeclarationSyntax(
    Symbol name,
    ArgumentSyntax[] properties) : ISyntaxNode
{
    public Symbol Name { get; } = name;

    public ArgumentSyntax[] Properties { get; } = properties;

    #region Property Helpers

    public ISyntaxNode? Precedence => GetPropertyValue("precedence");
    
    public ISyntaxNode? Associativity => GetPropertyValue("associativity");

    public ISyntaxNode? GetPropertyValue(string name)
    {
        foreach (ArgumentSyntax property in Properties)
        {
            if (property.Name == name)
            {
                return property.Value;
            }
        }

        return null;
    }

    #endregion

    SyntaxKind ISyntaxNode.Kind => SyntaxKind.OperatorDeclaration;
}

// _ "+" operator { associativity: "left" }
