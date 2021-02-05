using E.Symbols;

namespace E.Expressions
{
    // (i32, i32)
    // (a: 1, b: 2, c: 3)
    public sealed class TuplePattern : IExpression
    {
        public TuplePattern(TupleExpression tuple)
        {
            Variables = new TupleElement[tuple.Elements.Length];

            for (var i = 0; i < tuple.Elements.Length; i++)
            {
                var element = tuple.Elements[i];

                if (element is TupleElement v)
                {
                    Variables[i] = new TupleElement(v.Name, v.Value);
                }
                else if (element is Symbol symbol)
                {
                    Variables[i] = new TupleElement(symbol, null);
                }
            }
        }

        public TupleElement[] Variables { get; }

        ObjectType IObject.Kind => ObjectType.TuplePattern;
    }
}