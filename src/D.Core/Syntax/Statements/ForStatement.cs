namespace D.Syntax
{
    public class ForStatement : SyntaxNode
    {
        public ForStatement(SyntaxNode variable, SyntaxNode generator, BlockExpressionSyntax body)
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

        public BlockExpressionSyntax Body { get; }

        SyntaxKind SyntaxNode.Kind => SyntaxKind.ForStatement;
    }
}