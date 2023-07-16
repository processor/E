namespace E.Symbols;

public sealed class ParameterSymbol(string name, Symbol? type) : Symbol(name)
{
    public new Symbol? Type { get; } = type;
}