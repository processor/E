using System.Collections.Generic;
using System.Linq;

namespace D.Expressions
{
    public class ImplementationDeclaration : IExpression
    {
        public ImplementationDeclaration(Symbol protocal, Symbol type, IExpression[] members)
        {
            Protocal = protocal;
            Type     = type;
            Members  = members;
        }

        public Symbol Protocal { get; }

        public Symbol Type { get; }

        // Protocals
        public IExpression[] Members { get; set; }

        public IExpression this[int index] => Members[index];

        #region Helpers

        public IEnumerable<FunctionExpression> Constructors
        {
            get
            {
                foreach (var method in Members.OfType<FunctionExpression>())
                {
                    if (method.IsInitializer)
                    {
                        yield return method;
                    }
                }
            }
        }

        public IEnumerable<FunctionExpression> Methods
        {
            get
            {
                foreach (var method in Members.OfType<FunctionExpression>())
                {
                    if (!method.IsInitializer)
                    {
                        yield return method;
                    }
                }
            }
        }

        public IEnumerable<FunctionExpression> Properties
        {
            get
            {
                foreach (var method in Members.OfType<FunctionExpression>())
                {
                    if (method.IsProperty)
                    {
                        yield return method;
                    }
                }
            }
        }

        #endregion

        Kind IObject.Kind => Kind.ImplementationDeclaration;
    }
}