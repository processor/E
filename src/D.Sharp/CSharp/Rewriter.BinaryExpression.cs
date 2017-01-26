namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override IExpression VisitBinary(BinaryExpression be)
        {
            VisitBinary(be, false);

            return be;
        }

        public void VisitBinary(BinaryExpression be, bool a)
        {
            if (be.Operator.OpKind == Kind.ExponentiationExpression)
            {
                WriteCall("Math.Pow", be.Left, be.Right);
            }
            else
            {
                if (!a && be.Grouped)
                {
                    Emit("(");
                }

                Visit(be.Left);
                
                Emit(" ");

                Emit(be.Operator.Name);

                Emit(" ");

                Visit(be.Right);

                if (!a && be.Grouped)
                {
                    Emit(")");
                }
            }
        }
    }
}
