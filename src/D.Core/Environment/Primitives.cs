namespace D.Modules
{
    public class Primitives : Module
    {
        public Primitives()
        {
            Add(Type.Get(Kind.Any));
            Add(Type.Get(Kind.Void));
            Add(Type.Get(Kind.Decimal));
            Add(Type.Get(Kind.String));
            Add(Type.Get(Kind.Integer));
            Add(Type.Get(Kind.Number));
            Add(Type.Get(Kind.Float32));
            Add(Type.Get(Kind.Float64));
            Add(Type.Get(Kind.Int16));
            Add(Type.Get(Kind.Int32));
            Add(Type.Get(Kind.Int64));
        }
    }
}
