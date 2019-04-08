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
            Emit("else if (", level);

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

        public override IExpression VisitFor(ForStatement statement)
        {
            Indent(level);
            Emit("foreach (var ");

            Visit(statement.VariableExpression);

            writer.Write(" in ");
            writer.Write(statement.GeneratorExpression);
            writer.WriteLine(")");

            VisitBlock(statement.Body);
            
            return statement;
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
