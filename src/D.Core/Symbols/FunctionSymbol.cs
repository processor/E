namespace D
{
    public class FunctionSymbol : Symbol
    {
        public FunctionSymbol(string name, SymbolFlags flags = SymbolFlags.None)
            : base(name, flags) { }

        public FunctionSymbol(string module, string name, Symbol[] arguments)
            : base(module, name, arguments) { }

        public FunctionSymbol(string name, params Symbol[] arguments)
            : base(name, arguments) { }

        public override SymbolType SymbolType => SymbolType.Function;
    }
}