using E.Symbols;

namespace E.Expressions;

public sealed class ProtocolExpression(Symbol name, FunctionExpression[] members) : IExpression, INamedObject
{
    public Symbol Name { get; set; } = name;

    public FunctionExpression[] Members { get; } = members;

    // public IMessageDeclaration[] Channel { get; set; }

    ObjectType IObject.Kind => ObjectType.Protocol;

    string INamedObject.Name => Name;

    #region Helpers

    public bool Contains(string name)
    {
        foreach (var member in Members)
        {
            if (member.Name == name) return true;
        }

        return false;
    }

    #endregion
}