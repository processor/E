namespace D
{
    public class ModuleSymbol : Symbol
    {
        public ModuleSymbol(string name)
            : base(name) { }

        public override SymbolType SymbolType => SymbolType.Module;
    }
}