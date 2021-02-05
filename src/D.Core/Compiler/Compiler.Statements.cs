namespace E
{
    using Expressions;
    using Syntax;

    public partial class Compiler
    {
        public virtual IfStatement VisitIf(IfStatementSyntax syntax) =>
           new IfStatement(
               condition  : Visit(syntax.Condition), 
               body       : VisitBlock(syntax.Body),
               elseBranch : syntax.ElseBranch is not null ? Visit(syntax.ElseBranch) : null
            );

        public virtual ElseStatement VisitElse(ElseStatementSyntax syntax) =>
            new ElseStatement(VisitBlock(syntax.Body));

        public virtual ElseIfStatement VisitElseIf(ElseIfStatementSyntax syntax) =>
            new ElseIfStatement(Visit(syntax.Condition), VisitBlock(syntax.Body), Visit(syntax.ElseBranch));

        public virtual ReturnStatement VisitReturnStatement(ReturnStatementSyntax syntax)
        {
            return new ReturnStatement(Visit(syntax.Expression));
        }

        public virtual ForStatement VisitFor(ForStatementSyntax syntax)
        {
            return new ForStatement(
                variable  : Visit(syntax.VariableExpression),
                generator : Visit(syntax.GeneratorExpression),
                body      : VisitBlock(syntax.Body)
            );
        }

    }
}
