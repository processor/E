using System.Collections.Generic;
using System.Linq;

namespace D.Syntax
{
    // Curve implementation for Bezier { }
    // Matrix4 implementation 
    // Int32 implementation : Addable, Subtractable, ...

    public class ImplementationDeclarationSyntax : SyntaxNode
    {
        public ImplementationDeclarationSyntax(
            Symbol protocol,
            Symbol type, 
            SyntaxNode[] members)
        {
            Protocol = protocol;
            Type     = type;
            Members  = members;
        }

        public Symbol Protocol { get; }

        public Symbol Type { get; }

        // Protocols
        public SyntaxNode[] Members { get; }

        public SyntaxNode this[int index] => Members[index];

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

        SyntaxKind SyntaxNode.Kind => SyntaxKind.ImplementationDeclaration;
    }
}