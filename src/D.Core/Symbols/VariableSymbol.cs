namespace D
{
    public class VariableSymbol : Symbol
    {
        public VariableSymbol(string name, SymbolFlags flags = SymbolFlags.None)
            : base(name, flags) { }

        // Scope = Local OR Block

        public bool IsLocal => Flags.HasFlag(SymbolFlags.Local);

        public override SymbolType SymbolType => SymbolType.Variable;
    }
}