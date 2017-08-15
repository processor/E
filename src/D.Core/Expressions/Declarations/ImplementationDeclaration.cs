using System;
using System.Collections.Generic;

namespace D.Expressions
{
    public class ImplementationDeclaration : IExpression
    {
        public ImplementationDeclaration(Symbol protocol, TypeSymbol type, IExpression[] members)
        {
            Protocol = protocol ?? throw new ArgumentNullException(nameof(protocol));
            Type     = type     ?? throw new ArgumentNullException(nameof(type));
            Members  = members  ?? throw new ArgumentNullException(nameof(members));
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

        Kind IObject.Kind => Kind.ImplementationDeclaration;
    }
}