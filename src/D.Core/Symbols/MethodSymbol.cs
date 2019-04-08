using System.Collections.Generic;

namespace D
{
    public sealed class MethodSymbol : Symbol
    {
        public MethodSymbol(string name)
            : base(name) { }
       
        public IReadOnlyList<ParameterSymbol> Parameters { get; set; }

        public TypeSymbol ReturnType { get; set; }

        // Constructor
        // Destructor
        // Function
    }
}