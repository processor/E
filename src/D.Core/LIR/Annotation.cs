namespace D
{
    public sealed class Annotation /* readonly struct? */
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