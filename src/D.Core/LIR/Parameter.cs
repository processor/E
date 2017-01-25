namespace D
{
    public class Parameter
    {
        public static readonly Parameter Any     = Get(Kind.Any);
        public static readonly Parameter String  = Get(Kind.String);
        public static readonly Parameter Byte    = Get(Kind.Byte);
        public static readonly Parameter Float   = Get(Kind.Float);
        public static readonly Parameter Decimal = Get(Kind.Decimal);
        public static readonly Parameter Integer = Get(Kind.Integer);

        public Parameter(string name)
        {
            Name = name;
            Type = new Type(Kind.Any);
        }

        public Parameter(string name, Kind kind)
        {
            Name = name;
            Type = new Type(kind);
        }

        public Parameter(string name, IType type, bool optional = false, object defaultValue = null)
        {
            Name = name;
            Type = type;
            IsOptional = optional;
            DefaultValue = defaultValue;
        }

        public Parameter(IType type)
        {
            Type = type;
        }

        public string Name { get; }

        public bool IsOptional { get; }

        public object DefaultValue { get; }

        public IType Type { get; }

        public Predicate[] Conditions { get; set; }

        public ParameterDirection Direction { get; set; }

        // TODO: cache on kind
        public static Parameter Get(Kind kind)
            => new Parameter(new Type(kind));
    }

    public enum ParameterDirection
    {
        In,
        Out,
        InOut
    }

}