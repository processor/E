namespace D
{
    public class LabelSymbol : Symbol
    {
        public LabelSymbol(string name)
            : base(name) { }

        public override SymbolType SymbolType => SymbolType.Label;
    }
}