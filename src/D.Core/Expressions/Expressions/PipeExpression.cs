namespace D.Expressions
{
    // Applies a function to the preceding argument. 
    public class PipeStatement : IExpression
    {
        public PipeStatement(IExpression callee, IExpression expression)
        {
            Callee = callee;
            Expression = expression;
        }

        public IExpression Callee { get; }

        // CallExpression | MatchStatement
        public IExpression Expression { get; }

        public Kind Kind => Kind.PipeStatement;
    }
}

// |> call || match