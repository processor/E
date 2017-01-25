using System.Collections.Generic;
using System.Linq;

namespace D.Syntax
{
    // Curve implementation for Bezier { }
    // Matrix4 implementation 
    // Int32 implementation : Addable, Subtractable, ...

    public class ImplementationDeclarationSyntax : ISyntax
    {
        public ImplementationDeclarationSyntax(Symbol protocal, Symbol type, ISyntax[] members)
        {
            Protocal = protocal;
            Type     = type;
            Members  = members;
        }

        public Symbol Protocal { get; }

        public Symbol Type { get; }

        // Protocals
        public ISyntax[] Members { get; set; }

        public ISyntax this[int index] => Members[index];

        #region Helpers

        public IEnumerable<FunctionDeclarationSyntax> Constructors
        {
            get
            {
                foreach (var method in Members.OfType<FunctionDeclarationSyntax>())
                {
                    if (method.IsInitializer)
                    {
                        yield return method;
                    }
                }
            }
        }

        public IEnumerable<FunctionDeclarationSyntax> Methods
        {
            get
            {
                foreach (var method in Members.OfType<FunctionDeclarationSyntax>())
                {
                    if (!method.IsInitializer)
                    {
                        yield return method;
                    }
                }
            }
        }

        public IEnumerable<FunctionDeclarationSyntax> Properties
        {
            get
            {
                foreach (var method in Members.OfType<FunctionDeclarationSyntax>())
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