namespace D.Compiler
{
    using Compilation;

    public partial class CSharpTranspiler
    {
        public void WriteCompliationUnit(string module, CompliationUnit unit)
        {
            EmitLine("namespace " + module);
            EmitLine("{");

            level++;

            WriteCompliationUnit(unit);

            level--;

            EmitLine();

            Emit("}");
        }

        public void WriteCompliationUnit(CompliationUnit unit)
        {
            var i = 0;

            foreach (var protocal in unit.Protocals)
            {
                if (++i > 1)
                {
                    EmitLine();
                    EmitLine();
                }

                VisitProtocal(protocal);
            }

            foreach (var type in unit.Types)
            {
                if (++i > 1)
                {
                    EmitLine();
                    EmitLine();
                }

                WriteImplementation(type);

                // Write(statement.Key, statement.Value, 0);
            }

            foreach (var function in unit.Functions)
            {
                if (++i > 1)
                {
                    EmitLine();
                    EmitLine();
                }

                VisitFunction(function);
            }
        }        
    }
}
