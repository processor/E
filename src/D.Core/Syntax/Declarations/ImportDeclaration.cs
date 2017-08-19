namespace D.Syntax
{
    public class ImportDeclarationSyntax : SyntaxNode
    {
        SyntaxKind SyntaxNode.Kind => SyntaxKind.ImportDeclaration;
    }
}

// import Name