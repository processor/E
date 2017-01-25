namespace D.Syntax
{
    public class ElseStatementSyntax : ISyntax
    {
        public ElseStatementSyntax(BlockStatementSyntax body)
        {
            Body = body;
        }

        public BlockStatementSyntax Body { get; }

        Kind IObject.Kind => Kind.ElseStatement;
    }
}