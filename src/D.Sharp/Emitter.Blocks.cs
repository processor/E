namespace E.Compilation;

using Expressions;

public partial class CSharpEmitter
{
    public override IExpression VisitBlock(BlockExpression block)
    {
        EmitLine("{", level);

        level++;

        foreach (var statement in block.Statements)
        {
            Visit(statement);

            EmitLine();
        }

        level--;

        Emit("}", level);

        return block;
    }
}
