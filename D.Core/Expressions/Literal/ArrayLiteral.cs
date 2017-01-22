using System.Collections.Generic;

namespace D.Expressions
{
    public class ArrayLiteral : IExpression
    {
        public ArrayLiteral(IList<IObject> elements)
        {
            Elements = elements;
        }

        public IList<IObject> Elements { get; }

        public IObject this[int index] => Elements[index];

        public int Count => Elements.Count;

        // ElementKind

        public Kind Kind => Kind.ArrayLiteral;
    }
}