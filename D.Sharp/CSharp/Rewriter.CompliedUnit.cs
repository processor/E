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

            foreach (var impl in unit.Implementations)
            {
                if (++i > 1)
                {
                    EmitLine();
                    EmitLine();
                }

                WriteImplementation(impl.Key, impl.Value);

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
