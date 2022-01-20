using System.Collections.Generic;

using E.Expressions;

namespace E;

public sealed class ImplementationExpression : IExpression
{
    public ImplementationExpression(
        ProtocolExpression? protocol,
        Type type,
        IReadOnlyList<VariableDeclaration> variables,
        IReadOnlyList<FunctionExpression> members)
    {
        Protocol = protocol;
        Type = type;
        Variables = variables;
        Methods = members;
    }

    // Struct | Class

    public Type Type { get; }

    public ProtocolExpression? Protocol { get; }

    public IReadOnlyList<VariableDeclaration> Variables { get; }

    public IReadOnlyList<FunctionExpression> Methods { get; }

    ObjectType IObject.Kind => ObjectType.Implementation;
}