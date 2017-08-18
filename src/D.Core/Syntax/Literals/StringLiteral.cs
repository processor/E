using System;

namespace D.Syntax
{
    public class StringLiteralSyntax : SyntaxNode
    {
        public StringLiteralSyntax(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }

        public static implicit operator StringLiteralSyntax(string text) =>
            new StringLiteralSyntax(text);

        public static implicit operator string(StringLiteralSyntax text) =>
            text.Value;

        public override string ToString() => Value;
        
        SyntaxKind SyntaxNode.Kind => SyntaxKind.StringLiteral;
    }
}

// TODO: Multiline literals (""")