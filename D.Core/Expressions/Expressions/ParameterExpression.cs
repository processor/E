namespace D.Expressions
{
    public class ParameterExpression
    {
        public ParameterExpression(
            string name, 
            Symbol type = null, 
            IExpression defaultValue = null,
            IExpression predicate = null,
            ParameterFlags flags = ParameterFlags.None,
            int index = 0)
        {
            Name = name;
            Type = type;
            DefaultValue = defaultValue;
            Flags = flags;
            Predicate = predicate;
            Index = index;
        }

        public string Name { get; }

        public int Index { get; }

        public IExpression DefaultValue { get; }

        public IExpression Predicate { get; }

        public Symbol Type { get; }

        public ParameterFlags Flags { get; }

        public static ParameterExpression Ordinal(int index, Symbol type)
          => new ParameterExpression(null, type, flags: ParameterFlags.Nameless, index: index);
    }

    public enum ParameterFlags
    {
        None     = 0,
        Nameless = 1
    }

}