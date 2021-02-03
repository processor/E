namespace E.Syntax
{
    public sealed class ForStatementSyntax : ISyntaxNode
    {
        public ForStatementSyntax(
            ISyntaxNode? variableExpression, 
            ISyntaxNode generatorExpression,
            BlockSyntax body)
        {
            VariableExpression = variableExpression;
            GeneratorExpression = generatorExpression;
            Body = body;
        }

        // name | tuple pattern
        //  x   |    (x, x)
        public ISyntaxNode? VariableExpression { get; set; }

        // variable |  range
        //    c     | 1...100
        public ISyntaxNode GeneratorExpression { get; }

        public BlockSyntax Body { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ForStatement;
    }
}