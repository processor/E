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
            AddExport(Type.Get(ObjectType.Float16));
            AddExport(Type.Get(ObjectType.Float32));
            AddExport(Type.Get(ObjectType.Float64));
            AddExport(Type.Get(ObjectType.Byte));
            AddExport(Type.Get(ObjectType.Int8));
            AddExport(Type.Get(ObjectType.Int16));
            AddExport(Type.Get(ObjectType.Int32));
            AddExport(Type.Get(ObjectType.Int64));
            AddExport(Type.Get(ObjectType.BFloat16));


            // Alises / Keywords
            AddExport("u8",     Type.Get(ObjectType.Byte));
            AddExport("i8",     Type.Get(ObjectType.Int8));
            AddExport("i16",    Type.Get(ObjectType.Int16));
            AddExport("i32",    Type.Get(ObjectType.Int32));
            AddExport("i64",    Type.Get(ObjectType.Int64));
            AddExport("f16",    Type.Get(ObjectType.Float16));
            AddExport("f32",    Type.Get(ObjectType.Float32));
            AddExport("f64",    Type.Get(ObjectType.Float64));
            AddExport("bf16",   Type.Get(ObjectType.BFloat16));
            AddExport("string", Type.Get(ObjectType.String));
            AddExport("none",   Type.Get(ObjectType.Void));
        }
    }
}