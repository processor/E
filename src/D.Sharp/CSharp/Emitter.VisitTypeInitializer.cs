namespace D.Compilation
{
    using Expressions;

    public partial class CSharpEmitter
    {       
        public override IExpression VisitTypeInitializer(TypeInitializer type)
        {
            Emit("new ");

            WriteTypeSymbol(type.Type);
            
            Emit('(');
            
            var i = 0;

            foreach (var member in type.Arguments)
            {
                if (++i != 1) Emit(", ");

                Emit(member.Name);

                Emit(": ");

                Visit((IObject)member.Value);
            }

            Emit(')');

            return type;
        }
    }
}