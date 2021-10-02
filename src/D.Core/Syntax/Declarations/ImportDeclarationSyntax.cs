namespace E.Syntax;

public sealed class ImportDeclarationSyntax : ISyntaxNode
{
    SyntaxKind ISyntaxNode.Kind => SyntaxKind.ImportDeclaration;
}

// from Module import Name