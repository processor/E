namespace D.Expressions
{
    public class MatrixLiteral : IExpression
    {
        public MatrixLiteral(IObject[] elements, int stride)
        {
            Stride = stride;
            Elements = elements;
        }

        public IObject[] Elements { get; }

        public int Stride { get; }

        public int RowCount => Elements.Length / Stride;

        public int ColumnCount => Stride;

        public object this[int x, int y] => Elements[(x * Stride) + y];

        public Kind ElementType => Elements[0].Kind;

        public Kind Kind => Kind.MatrixLiteral;
    }
}