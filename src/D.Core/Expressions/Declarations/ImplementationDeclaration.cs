using System.Collections.Generic;

using D.Symbols;

namespace D.Expressions
{
    public sealed class ImplementationDeclaration : IExpression
    {
        public ImplementationDeclaration(Symbol protocol, TypeSymbol type, IExpression[] members)
        {
            Protocol = protocol;
            Type = type;
            Members = members;
        }

        public Symbol Protocol { get; }

        public TypeSymbol Type { get; }

        // Protocols
        public IExpression[] Members { get; }

        public IExpression this[int index] => Members[index];

        #region Helpers

        public IEnumerable<FunctionExpression> Constructors
        {
            get
            {
                foreach (var method in Members)
                {
                    if (method is FunctionExpression func && func.IsInitializer)
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
}