namespace D
{
    // Field?

    public class Variable : IObject
    {
        public Variable(IType type, object value, bool isMutable)
        {
            Type = type;
            Value = value;
            IsMutable = isMutable;
        }

        public bool IsMutable { get; }

        public IType Type { get; }
        
        public object Value { get; }

        Kind IObject.Kind => Kind.Variable;
    }   
}