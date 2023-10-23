using System.Collections.Generic;

using E.Expressions;
using E.Syntax;

namespace E;

public partial class Compiler
{
    public ImplementationExpression VisitImplementation(ImplementationDeclarationSyntax syntax)
    {
        _env = _env.Nested("impl");

        var type = _env.GetType(syntax.Type);
        var protocol = syntax.Protocol is not null ? _env.Get<ProtocolExpression>(syntax.Protocol) : null;

        #region Flow

        flow.Define("this", type);

        if (type.Properties is not null)
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

        _env = _env.Parent!;

        var result = new ImplementationExpression(protocol, type, variables, methods);

        result.Type.Implementations.Add(result);

        return result;
    }
}