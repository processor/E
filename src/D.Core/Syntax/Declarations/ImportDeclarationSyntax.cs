namespace D.Syntax
{
    public class ImportDeclarationSyntax : SyntaxNode
    {
        SyntaxKind SyntaxNode.Kind => SyntaxKind.ImportDeclaration;
    }
}

// from Module import Name