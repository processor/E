using System.Collections.Generic;
using System.Linq;

using D.Symbols;

namespace D.Syntax
{
    public sealed class ImplementationDeclarationSyntax : ISyntaxNode
    {
        public ImplementationDeclarationSyntax(
            Symbol protocol,
            Symbol type, 
            ISyntaxNode[] members)
        {
            Protocol = protocol;
            Type     = type;
            Members  = members;
        }

        public Symbol Protocol { get; }

        public Symbol Type { get; }

        // Protocols
        public ISyntaxNode[] Members { get; }

        public ISyntaxNode this[int index] => Members[index];

        #region Helpers

        public IEnumerable<FunctionDeclarationSyntax> Constructors
        {
            get
            {
                foreach (var method in Members)
                {
                    if (method is FunctionDeclarationSyntax func && func.IsInitializer)
                    {
                        yield return func;
                    }
                }
            }
        }

        public IEnumerable<FunctionDeclarationSyntax> Methods
        {
            get
            {
                foreach (var method in Members)
                {
                    if (method is FunctionDeclarationSyntax func && !func.IsInitializer)
                    {
                        yield return func;
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

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.ImplementationDeclaration;
    }
}

// Curve implementation for Bezier { }
// Matrix4 implementation 
// Int32 implementation : Addable, Subtractable, ...