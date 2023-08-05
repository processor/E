using E.Symbols;

namespace E.Expressions;

public sealed class UsingStatement(Symbol[] domains) : IExpression
{
    public Symbol[] Domains { get; } = domains;

    public Symbol this[int i] => Domains[i];

    ObjectType IObject.Kind => ObjectType.UsingStatement;
}