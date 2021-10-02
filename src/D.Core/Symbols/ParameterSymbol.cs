namespace E.Symbols;

public sealed class ParameterSymbol : Symbol
{
    public ParameterSymbol(string name, Symbol? type)
        : base(name)
    {
        Type = type;
    }

    public new Symbol? Type { get; }
}