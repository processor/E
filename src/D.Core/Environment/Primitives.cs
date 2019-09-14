namespace D.Modules
{
    public sealed class Primitives : Module
    {
        public Primitives()
        {
            AddExport(Type.Get(ObjectType.Object));
            AddExport(Type.Get(ObjectType.Void));
            AddExport(Type.Get(ObjectType.Decimal));
            AddExport(Type.Get(ObjectType.String));
            AddExport(Type.Get(ObjectType.Number));
            AddExport(Type.Get(ObjectType.Float32));
            AddExport(Type.Get(ObjectType.Float64));
            AddExport(Type.Get(ObjectType.Int16));
            AddExport(Type.Get(ObjectType.Int32));
            AddExport(Type.Get(ObjectType.Int64));
        }
    }
}