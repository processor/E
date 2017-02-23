namespace D.Compilation
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public void VisitProtocal(ProtocalExpression protocal)
        {
            EmitLine($"public interface {protocal.Name}", level);
            EmitLine("{", level);

            level++;

            foreach (var member in protocal.Members)
            {
                Indent(level);

                if (member.IsProperty)
                {
                    WriteTypeSymbol(member.ReturnType);

                    Emit(" ");

                    Emit(ToPascalCase(member.Name));

                    Emit(" { get; }");
                }
                else
                {
                    WriteTypeSymbol(member.ReturnType);

                    Emit(" ");

                    Emit(ToPascalCase(member.Name));

                    WriteParameters(member.Parameters);

                    Emit(";");
                }

                EmitLine();
            }

            level--;
            
            Emit("}", level);
        }
    }
}