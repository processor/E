namespace D.Transformations
{
    public struct Perspective : ITransform
    {
        public Perspective(INumeric<double> length)
        {
            Length = length;
        }

        public INumeric<double> Length { get; }
    }

}