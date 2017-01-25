using System.Collections.Generic;
using System.Linq;

namespace D.Expressions
{
    // Curve implementation for Bezier { }
    // Matrix4 implementation 
    // Int32 implementation : Addable, Subtractable, ...

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

        public IEnumerable<Function> Constructors
        {
            get
            {
                foreach (var method in Members.OfType<Function>())
                {
                    if (method.IsInitializer)
                    {
                        yield return method;
                    }
                }
            }
        }

        public IEnumerable<Function> Methods
        {
            get
            {
                foreach (var method in Members.OfType<Function>())
                {
                    if (!method.IsInitializer)
                    {
                        yield return method;
                    }
                }
            }
        }

        public IEnumerable<Function> Properties
        {
            get
            {
                foreach (var method in Members.OfType<Function>())
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