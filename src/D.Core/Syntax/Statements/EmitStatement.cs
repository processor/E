namespace D.Syntax
{
    public class EmitStatement : ISyntax
    {
        public EmitStatement(ISyntax expression)
        {
            Expression = expression;
        }

        public ISyntax Expression { get; }

        Kind IObject.Kind => Kind.EmitStatement;
    }
}