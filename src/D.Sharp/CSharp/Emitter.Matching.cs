namespace E.Compilation
{
    using Expressions;

    public partial class CSharpEmitter
    {
        public override IExpression VisitMatch(MatchExpression match)
        {
            Indent(level);
            Emit("return ");
            Emit(match.Expression.ToString());
            Emit(" switch");
            EmitLine();
            EmitLine("{", level);

            level++;

            foreach (var c in match.Cases)
            {
                WriteMatchCase(c);
            }

            level--;

            Emit("}", level);

            return match;
        }

        public void WriteMatchCase(MatchCase c)
        {
            Indent(level);
            Visit(c.Pattern);
            Emit(" => ");
            Visit(c.Body.Expression);
            EmitLine(",");
        }
    }
}
