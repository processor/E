namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override void VisitMatch(MatchExpression match)
        {
            Indent(level);
            Emit("switch (");
            Emit(match.Expression.ToString());
            EmitLine(")");
            EmitLine("{", level);

            level++;

            foreach (var c in match.Cases)
            {
                WriteMatchCase(c);
            }

            level--;

            Emit("}", level);
        }

        public void WriteMatchCase(MatchCase c)
        {
            Indent(level);
            Emit("case ");
            Visit(c.Pattern);
            Emit(": return ");
            Visit(c.Body.Expression);
            EmitLine(";");
        }
    }
}
