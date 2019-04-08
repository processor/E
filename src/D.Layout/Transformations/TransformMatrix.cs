namespace D.Transformations
{
    public struct TransformMatrix : ITransform
    {
        // equivalent 4x4 transform matrix
        public double[] Value { get; set; }
    }
}