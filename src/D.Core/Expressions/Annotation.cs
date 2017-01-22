namespace D
{
    public class Annotation
    {
        public Annotation(string name, IObject value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public IObject Value { get; }
    }
}