namespace D
{
    public readonly struct Annotation
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