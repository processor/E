﻿using System;

namespace D.Syntax
{
    public class OperatorDeclarationSyntax : SyntaxNode
    {
        public OperatorDeclarationSyntax(
            Symbol name, 
            ArgumentSyntax[] properties)
        {
            Name       = name       ?? throw new ArgumentNullException(nameof(name));
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public Symbol Name { get; }
        
        public ArgumentSyntax[] Properties { get; }

        #region Property Helpers
        
        public SyntaxNode Precedence => GetPropertyValue("precedence");
        
        public SyntaxNode Associativity => GetPropertyValue("associativity");

        public SyntaxNode GetPropertyValue(string name)
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

        SyntaxKind SyntaxNode.Kind => SyntaxKind.OperatorDeclaration;
    }
}

// _ "+" operator { associtivity: "left" }
