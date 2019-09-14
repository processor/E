namespace D.Syntax
{
    public sealed class OperatorDeclarationSyntax : ISyntaxNode
    {
        public OperatorDeclarationSyntax(
            Symbol name, 
            ArgumentSyntax[] properties)
        {
            Name = name;
            Properties = properties;
        }

        public Symbol Name { get; }
        
        public ArgumentSyntax[] Properties { get; }

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
}

// _ "+" operator { associtivity: "left" }
