namespace D.Syntax
{
    public class IfStatementSyntax : ISyntax
    {
        public IfStatementSyntax(ISyntax condition, BlockStatementSyntax body, ISyntax elseBranch)
        {
            Condition = condition;
            Body = body;
            ElseBranch = elseBranch;
        }

        public ISyntax Condition { get; }

        public BlockStatementSyntax Body { get; }

        // Else | ElseIf
        public ISyntax ElseBranch { get; }

        Kind IObject.Kind => Kind.IfStatement;
    }
}