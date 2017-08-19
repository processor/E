namespace D.Syntax
{
    public class ForStatementSyntax : SyntaxNode
    {
        public ForStatementSyntax(SyntaxNode variable, SyntaxNode generator, BlockSyntax body)
        {
            VariableExpression = variable;
            GeneratorExpression = generator;
            Body = body;
        }

        // name | tuple pattern
        //  x   |    (x, x)
        public SyntaxNode VariableExpression { get; set; }

        // variable |  range
        //    c     | 1...100
        public SyntaxNode GeneratorExpression { get; }

        public BlockSyntax Body { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.ForStatement;
    }
}