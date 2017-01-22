namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override void VisitDestructuringAssignment(DestructuringAssignment expression)
        {
            var i = 0;

            foreach(var a in expression.Variables)
            {
                if (i != 0) EmitLine();

                Indent(level);

                Emit("var ");

                Emit(a.Value.ToString());

                Emit(" = ");

                Emit(expression.Instance.ToString() + "." + ToPascalCase(a.Value.ToString()));
                Emit(";");

                i++;
            }
        }
    }
}
