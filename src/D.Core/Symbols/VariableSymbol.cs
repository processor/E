namespace E.Symbols;

public sealed class VariableSymbol(
    string name,
    SymbolFlags flags = default) : Symbol(name, flags)
{

    // Scope = Local OR Block

    public bool IsLocal => Flags.HasFlag(SymbolFlags.Local);
}