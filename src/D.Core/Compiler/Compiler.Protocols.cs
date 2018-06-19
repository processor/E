namespace D
{
    using Expressions;
    using Syntax;

    public partial class Compiler
    {
        public ProtocolExpression VisitProtocol(ProtocolDeclarationSyntax protocol)
        {
            var functions = new FunctionExpression[protocol.Members.Length];

            for (var i = 0; i < functions.Length; i++)
            {
                functions[i] = VisitFunctionDeclaration(protocol.Members[i]);
            }

            var result = new ProtocolExpression(protocol.Name, functions);

            env.Add(protocol.Name, result);
            
            return result;
        }
    }
}
