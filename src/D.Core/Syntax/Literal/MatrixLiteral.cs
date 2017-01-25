namespace D.Syntax
{
    public class MatrixLiteralSyntax : ISyntax
    {
        public MatrixLiteralSyntax(ISyntax[] elements, int stride)
        {
            Stride = stride;
            Elements = elements;
        }

        public ISyntax[] Elements { get; }

        public int Stride { get; }

        public int RowCount => Elements.Length / Stride;

        public int ColumnCount => Stride;

        public ISyntax this[int x, int y] => Elements[(x * Stride) + y];

        public Kind ElementType => Elements[0].Kind;

        public Kind Kind => Kind.MatrixLiteral;
    }
}