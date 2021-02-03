namespace E.Expressions
{
    public sealed class WhileStatement : IExpression
    {
        public WhileStatement(IExpression condition, BlockExpression body)
        {
            Condition = condition;
            Body = body;
        }

        public IExpression Condition { get; }

        public BlockExpression Body { get; }

        ObjectType IObject.Kind => ObjectType.WhileStatement;
    }
}

// rust: loop { } 
// swift: repeat { } while condition

// onIteration  (i++)
// precondition
// postcondition