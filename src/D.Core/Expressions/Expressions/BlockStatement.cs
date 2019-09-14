namespace D.Expressions
{
    public class BlockExpression : IExpression
    {
        public BlockExpression(params IExpression[] statements)
        {
            Statements = statements;
        }

        public IExpression[] Statements { get; }

        public IExpression this[int index] => Statements[index];

        public int Count => Statements.Length;

        ObjectType IObject.Kind => ObjectType.BlockStatement;
    }
}