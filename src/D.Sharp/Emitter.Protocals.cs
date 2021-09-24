using E.Expressions;

namespace E.Compilation;

public partial class CSharpEmitter
{
    public void VisitProtocal(ProtocolExpression protocol)
    {
        EmitLine($"public interface {protocol.Name}", level);
        EmitLine("{", level);

        level++;

        foreach (var member in protocol.Members)
        {
            Indent(level);

            if (member.IsProperty)
            {
                WriteTypeSymbol(member.ReturnType);

                Emit(' ');

                Emit(ToPascalCase(member.Name));

                Emit(" { get; }");
            }
            else
            {
                WriteTypeSymbol(member.ReturnType);

                Emit(' ');

                Emit(ToPascalCase(member.Name));

                WriteParameters(member.Parameters);

                Emit(';');
            }

            EmitLine();
        }

        level--;

        Emit("}", level);
    }
}
