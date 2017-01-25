using System.Collections.Generic;

namespace D.Syntax
{
    public class ArrayLiteralSyntax : ISyntax
    {
        public ArrayLiteralSyntax(IList<ISyntax> elements)
        {
            Elements = elements;
        }

        public IList<ISyntax> Elements { get; }

        public IObject this[int index] => Elements[index];

        public int Count => Elements.Count;

        // ElementKind

        public Kind Kind => Kind.ArrayLiteral;
    }
}