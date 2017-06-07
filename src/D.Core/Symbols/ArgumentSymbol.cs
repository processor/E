namespace D
{
    public class ArgumentSymbol : Symbol
    {
        public ArgumentSymbol(string name)
            : base(name) { }

        public override SymbolType SymbolType => SymbolType.Argument;
    }
}