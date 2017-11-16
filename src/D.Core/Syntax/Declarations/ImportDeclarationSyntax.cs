namespace D.Syntax
{
    public class ImportDeclarationSyntax : ISyntaxNode
    {
        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ImportDeclaration;
    }
}

// from Module import Name