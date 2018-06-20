using System.Collections.Generic;

namespace D
{
    using Expressions;
    using Syntax;

    public partial class Compiler
    {
        public ImplementationExpression VisitImplementation(ImplementationDeclarationSyntax syntax)
        {
            env = env.Nested("impl");

            var type = env.GetType(syntax.Type);
            var protocol = syntax.Protocol != null ? env.Get<ProtocolExpression>(syntax.Protocol) : null;

            #region Flow

            flow.Define("this", type);

            if (type.Properties != null)
            {
                foreach (var property in type.Properties)
                {
                    flow.Define(property.Name, property.Type);
                }
            }

            #endregion

            var methods   = new List<FunctionExpression>();
            var variables = new List<VariableDeclaration>();

            foreach (var member in syntax.Members)
            {
                switch (member)
                {
                    case FunctionDeclarationSyntax function:
                        methods.Add(VisitFunctionDeclaration(function, type));
                        break;

                    case PropertyDeclarationSyntax variable:
                        variables.Add(VisitVariableDeclaration(variable));
                        break;
                }

                // Property       a =>
                // Initializer    from  
                // Converter      to
                // Operator       *
                // Method         a () =>
            }

            env = env.Parent;

            var result = new ImplementationExpression(protocol, type, variables.ToArray(), methods.ToArray());

            result.Type.Implementations.Add(result);

            return result;
        }
    }
}