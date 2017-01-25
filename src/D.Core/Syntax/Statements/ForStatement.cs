namespace D.Syntax
{
    public class ForStatement : ISyntax
    {
        public ForStatement(ISyntax variable, ISyntax generator, BlockStatementSyntax body)
        {
            VariableExpression = variable;
            GeneratorExpression = generator;
            Body = body;
        }

        // name | tuple pattern
        //  x   |    (x, x)
        public ISyntax VariableExpression { get; set; }

        // variable |  range
        //    c     | 1...100
        public ISyntax GeneratorExpression { get; }

        public BlockStatementSyntax Body { get; }

        Kind IObject.Kind => Kind.ForStatement;
    }
}