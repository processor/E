using System.Collections.Generic;

using E.Symbols;

namespace E.Expressions;

public sealed class ImplementationDeclaration(
    Symbol protocol,
    TypeSymbol type,
    IExpression[] members) : IExpression
{
    public Symbol Protocol { get; } = protocol;

    public TypeSymbol Type { get; } = type;

    // Protocols
    public IExpression[] Members { get; } = members;

    public IExpression this[int index] => Members[index];

    #region Helpers

    public IEnumerable<FunctionExpression> Constructors
    {
        get
        {
            foreach (var method in Members)
            {
                if (method is FunctionExpression { IsInitializer: true } func)
                {
                    yield return func;
                }
            }
        }
    }

    // Functions?
    public IEnumerable<FunctionExpression> Methods
    {
        get
        {
            foreach (var method in Members)
            {
                if (method is FunctionExpression func && !func.IsInitializer)
                {
                    yield return func;
                }
            }
        }
    }

    public IEnumerable<FunctionExpression> Properties
    {
        get
        {
            foreach (var method in Members)
            {
                if (method is FunctionExpression func && !func.IsProperty)
                {
                    yield return func;
                }
            }
        }
    }

    #endregion

    ObjectType IObject.Kind => ObjectType.ImplementationDeclaration;
}