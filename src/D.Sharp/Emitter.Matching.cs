namespace E.Compilation;

using Expressions;

public partial class CSharpEmitter
{
    public override IExpression VisitMatch(MatchExpression match)
    {
        Indent(level);
        Emit("return ");
        Visit(match.Expression);
        Emit(" switch");
        EmitLine();
        EmitLine("{", level);

        level++;

        for (var i = 0; i < match.Cases.Length; i++)
        {
            if (i > 0)
            {
                EmitLine(",");
            }

            WriteMatchCase(match.Cases[i]);
        }

        EmitLine();

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
    }
}
