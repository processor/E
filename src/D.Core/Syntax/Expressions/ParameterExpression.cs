namespace D.Syntax
{
    public class ParameterSyntax
    {
        public ParameterSyntax(
            string name, 
            Symbol type = null,
            ISyntax defaultValue = null,
            ISyntax predicate = null,
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

        public ISyntax DefaultValue { get; }

        public ISyntax Predicate { get; }

        public Symbol Type { get; }

        public ParameterFlags Flags { get; }

        public static ParameterSyntax Ordinal(int index, Symbol type)
          => new ParameterSyntax(null, type, flags: ParameterFlags.Nameless, index: index);
    }

    public enum ParameterFlags
    {
        None     = 0,
        Nameless = 1
    }

}