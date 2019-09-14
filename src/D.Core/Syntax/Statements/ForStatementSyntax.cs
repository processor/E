namespace D.Syntax
{
    public sealed class ForStatementSyntax : ISyntaxNode
    {
        public ForStatementSyntax(ISyntaxNode variable, ISyntaxNode generator, BlockSyntax body)
        {
            VariableExpression = variable;
            GeneratorExpression = generator;
            Body = body;
        }

        // name | tuple pattern
        //  x   |    (x, x)
        public ISyntaxNode VariableExpression { get; set; }

        // variable |  range
        //    c     | 1...100
        public ISyntaxNode GeneratorExpression { get; }

        public BlockSyntax Body { get; }

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ForStatement;
    }
}