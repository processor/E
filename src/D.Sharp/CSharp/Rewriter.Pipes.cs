namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        // a |> b |> c

        // a |> b(100) |> c(100, 18)

        // c(b(a, 100), 100, 18))

        public override IExpression VisitPipe(PipeStatement pipe)
        {
            var call = (CallExpression)pipe.Expression;
                
            Emit(call.FunctionName);

            Emit("(");

            if (pipe.Callee is PipeStatement)
            {
                // level++;

                VisitPipe((PipeStatement)pipe.Callee);
            }
            else
            {
                Visit(pipe.Callee);
            }

            foreach (var arg in call.Arguments)
            {
                Emit(", ");

                Visit(arg.Value);
            }

            writer.Write(")");

            return pipe;
        }
    }
}
