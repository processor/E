using System;
using System.Text;

namespace D
{
    using Expressions; 

    // Array        [ ] Type
    // Dictionary   [ key`Type: value`Type ]
    // Function     (parameter`Type) -> return`Type 
    // Tuple        ()
    // Optional     Type?       Optional<T>
    // Any

    // physics::Momentum
    // Array<String>
    // Array<physics::Momentum>

    public class Symbol : IExpression, Syntax.ISyntax
    {
        public static readonly Symbol Any    = Symbol.Type("Any");
        public static readonly Symbol String = Symbol.Type("String");
        public static readonly Symbol Number = Symbol.Type("Number");
        public static readonly Symbol Void   = Symbol.Type("Void");         // unit '()' in rust. Void in Swift / C#, ...

        public Symbol(string name, SymbolKind kind)
        {
            Name = name;
            SymbolKind = kind;
            Arguments = Array.Empty<Symbol>();
        }

        public Symbol(string domain, string name, SymbolKind kind, Symbol[] arguments)
        {
            Domain      = domain;
            Name        = name;
            Arguments   = arguments;
            SymbolKind  = kind;
        }

        public Symbol(string name, SymbolKind kind, params Symbol[] arguments)
        {
            Name        = name;
            Arguments   = arguments;
            SymbolKind  = kind;
        }

        public string Domain { get; }

        public string Name { get; }

        public Symbol[] Arguments { get; }

        public SymbolKind SymbolKind { get; }

        #region Initializization

        public void Initialize(IType type)
        {
            ResolvedTyped = type;

            Status = SymbolStatus.Initialized;
        }

        public SymbolStatus Status { get; set; } = SymbolStatus.Uninitialized;

        public IType ResolvedTyped { get; set; }

        #endregion

        Kind IObject.Kind => Kind.Symbol;

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Domain != null)
            {
                sb.Append(Domain);
                sb.Append("::");
            }

            sb.Append(Name);

            if (Arguments.Length > 0)
            {
                sb.Append("<");

                var i = 0;

                foreach (var arg in Arguments)
                {
                    if (++i > 1) sb.Append(",");

                    sb.Append(arg.ToString());
                }

                sb.Append(">");
            }

            return sb.ToString();
        }

        public static Symbol Label(string name)
           => new Symbol(name, SymbolKind.Label);

        public static Symbol Argument(string name)
            => new Symbol(name, SymbolKind.Argument);

        public static Symbol Local(string name)
            => new Symbol(name, SymbolKind.LocalVariable);

        public static Symbol Instance(string name)
            => new Symbol(name, SymbolKind.Property);

        public static Symbol Type(string name)
         => new Symbol(name, SymbolKind.Type);

        public static Symbol Type(string name, params Symbol[] arguments)
         => new Symbol(name, SymbolKind.Type, arguments);

        public static implicit operator string(Symbol symbol)
            => symbol?.ToString();
    }

    public enum SymbolKind
    {
        Member,
        Type,
        Function,   // instance vs global?
        Domain,
        Label,

        // Variables
        Argument,
        Property, // type member
        LocalVariable
    }

    public enum SymbolFlags 
    {
        InfixOperator   = 1,
        PrefixOperator  = 2,
        PostfixOperator = 3
    }

    public enum SymbolStatus
    {
        Uninitialized,
        Initializing,
        Initialized
    }
}