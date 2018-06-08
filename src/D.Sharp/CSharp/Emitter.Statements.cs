namespace D.Compilation
{
    using Expressions;

    public partial class CSharpEmitter
    {
        public override IExpression VisitReturn(ReturnStatement expression)
        {
            Indent(level);
            Emit("return ");

            Visit(expression.Expression);

            Emit(';');

            return expression;
        }

        public override IExpression VisitIf(IfStatement expression)
        {
            Indent(level);
            Emit("if (");

            Visit(expression.Condition);

            writer.WriteLine(")");

            VisitBlock(expression.Body);

            if (expression.ElseBranch != null)
            {
                EmitLine();

                Visit(expression.ElseBranch);
            }

            return expression;
        }

        public override IExpression VisitElseIf(ElseIfStatement type)
        {
            Indent(level);
            Emit("else if (");

            Visit(type.Condition);

            writer.WriteLine(")");

            VisitBlock(type.Body);

            if (type.ElseBranch != null)
            {
                writer.WriteLine();

                Visit(type.ElseBranch);
            }

            return type;
        }

        public override IExpression VisitElse(ElseStatement type)
        {
            Emit("else");
            writer.WriteLine();

            VisitBlock(type.Body);

            return type;
        }              
    }
}
