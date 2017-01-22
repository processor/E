namespace D.Compiler
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public override void VisitIndexAccess(IndexAccessExpression access)
        {
            Visit(access.Left);
            Emit("[");
            Visit(access.Arguments[0]);
            Emit("]");
        }

        public override void VisitMemberAccess(MemberAccessExpression access)
        {
            Visit(access.Left);
            Emit(".");
            Emit(ToPascalCase(access.MemberName));
        }
    }
}