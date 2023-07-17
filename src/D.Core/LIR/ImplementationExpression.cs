using System.Collections.Generic;

using E.Expressions;

namespace E;

public sealed class ImplementationExpression(
    ProtocolExpression? protocol,
    Type type,
    IReadOnlyList<VariableDeclaration> variables,
    IReadOnlyList<FunctionExpression> members) : IExpression
{
    // Struct | Class

    public Type Type { get; } = type;

    public ProtocolExpression? Protocol { get; } = protocol;

    public IReadOnlyList<VariableDeclaration> Variables { get; } = variables;

    public IReadOnlyList<FunctionExpression> Methods { get; } = members;

    ObjectType IObject.Kind => ObjectType.Implementation;
}