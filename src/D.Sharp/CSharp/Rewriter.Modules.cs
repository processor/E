namespace D.Compilation
{
    using Expressions;

    public partial class CSharpTranspiler
    {
        public void WriteModule(Module module)
        {
            EmitLine("namespace " + module.Name);
            EmitLine("{");

            level++;

            WriteModuleMembers(module);

            level--;

            EmitLine();

            Emit("}");
        }

        public void WriteModuleMembers(Module module)
        {
            var i = 0;

            foreach (var (name, member) in module)
            {
                if (++i > 1)
                {
                    EmitLine();
                    EmitLine();
                }

                switch (member)
                {
                    case ProtocolExpression protocol : VisitProtocal(protocol);   break;
                    case Type type                   : WriteImplementation(type); break;
                    case FunctionExpression func     : VisitFunction(func);       break;
                }
            }
        }        
    }
}
