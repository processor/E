namespace D.Modules
{
    public sealed class Primitives : Module
    {
        public Primitives()
        {
            AddExport(Type.Get(Kind.Object));
            AddExport(Type.Get(Kind.Void));
            AddExport(Type.Get(Kind.Decimal));
            AddExport(Type.Get(Kind.String));
            AddExport(Type.Get(Kind.Number));
            AddExport(Type.Get(Kind.Float32));
            AddExport(Type.Get(Kind.Float64));
            AddExport(Type.Get(Kind.Int16));
            AddExport(Type.Get(Kind.Int32));
            AddExport(Type.Get(Kind.Int64));
        }
    }
}