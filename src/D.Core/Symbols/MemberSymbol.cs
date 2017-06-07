using System;

namespace D
{
    public class MemberSymbol : Symbol
    {
        public MemberSymbol(string name)
            : base(name) { }

        public MemberSymbol(string module, string name)
            : base(module, name, Array.Empty<Symbol>()) { }

        public override SymbolType SymbolType => SymbolType.Member;
    }
}