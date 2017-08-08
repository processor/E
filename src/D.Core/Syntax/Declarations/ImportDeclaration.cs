namespace D.Syntax
{
    public class ImportDeclarationSyntax : SyntaxNode
    {
        Kind IObject.Kind => Kind.ImportDeclaration;
    }
}

// import Name