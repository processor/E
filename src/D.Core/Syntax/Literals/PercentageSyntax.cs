using System;

namespace D.Syntax
{
    public class PercentageSyntax : ISyntaxNode
    {
        public PercentageSyntax(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));     
        }

        public string Text { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.Percentage;

        public override string ToString() => Text + "%";
    }
}

// e.g. 50%