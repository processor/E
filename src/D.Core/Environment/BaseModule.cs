namespace E.Modules
{
    public sealed class BaseModule : Module
    {
        public BaseModule()
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

            // Alises / Keywords
            AddExport("i32",  Type.Get(ObjectType.Int32));
            AddExport("i64",  Type.Get(ObjectType.Int64));
            AddExport("f32",  Type.Get(ObjectType.Float32));
            AddExport("f64",  Type.Get(ObjectType.Float64));
            AddExport("none", Type.Get(ObjectType.Void));
        }
    }
}