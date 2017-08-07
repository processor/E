namespace D.Compilation
{
    using Expressions;

    public partial class CSharpTranspiler
    {       
        public override IExpression VisitTypeInitializer(ObjectInitializer type)
        {
            Emit("new ");

            WriteTypeSymbol(type.Type);
            
            Emit("(");
            
            var i = 0;

            foreach (var member in type.Properties)
            {
                if (++i != 1) Emit(", ");

                Emit(member.Name);

                Emit(": ");

                Visit(member.Value);
            }

            Emit(")");

            return type;
        }
    }
}