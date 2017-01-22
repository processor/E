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
            Add(Type.Get(Kind.Float));
        }
    }
}
