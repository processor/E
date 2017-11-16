using System;

namespace D.Syntax
{
    // Kelvin unit: Temperature { symbol: "K", value: 1 }
    public class UnitDeclarationSyntax : ISyntaxNode
    {
        public UnitDeclarationSyntax(
            Symbol name, 
            Symbol baseType, 
            ArgumentSyntax[] properties)
        {
            Name       = name ?? throw new ArgumentNullException(nameof(name));
            BaseType   = baseType;
            Properties = properties; 
        }

        public Symbol Name { get; }

        public Symbol BaseType { get; }
        
        public ArgumentSyntax[] Properties { get; }

        #region Property Helpers

        public ISyntaxNode Value => GetPropertyValue("value");
        
        public ISyntaxNode Symbol => GetPropertyValue("symbol");

        public ISyntaxNode GetPropertyValue(string name)
        {
            foreach (var property in Properties)
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