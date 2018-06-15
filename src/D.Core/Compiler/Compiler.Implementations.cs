using System.Collections.Generic;

namespace D
{
    using Expressions;
    using Syntax;

    public partial class Compiler
    {
        public ImplementationExpression VisitImplementation(ImplementationDeclarationSyntax impl)
        {
            env = env.Nested("impl");

            var type = env.GetType(impl.Type);
            var protocol = impl.Protocol != null ? env.Get<ProtocolExpression>(impl.Protocol) : null;

            #region Setup environment

            env.Add("this", type);

            if (type.Properties != null)
            {
                foreach (var property in type.Properties)
                {
                    env.Add(property.Name, (Type)property.Type);
                }
            }

            #endregion

            var methods   = new List<FunctionExpression>();
            var variables = new List<VariableDeclaration>();

            foreach (var member in impl.Members)
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

            return new ImplementationExpression(protocol, type, variables.ToArray(), methods.ToArray());
        }
    }
}