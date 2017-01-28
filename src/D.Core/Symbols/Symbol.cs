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

    public class Symbol : IExpression, Syntax.SyntaxNode
    {
        public static readonly Symbol Any    = Symbol.Type("Any");
        public static readonly Symbol String = Symbol.Type("String");
        public static readonly Symbol Number = Symbol.Type("Number");
        public static readonly Symbol Void   = Symbol.Type("Void");         // unit '()' in rust. Void in Swift / C#, ...

        public Symbol(string name, SymbolFlags flags)
        {
            Name = name;
            Flags = flags;
            Arguments = Array.Empty<Symbol>();
        }

        public Symbol(string domain, string name, SymbolFlags flags, Symbol[] arguments)
        {
            Domain      = domain;
            Name        = name;
            Arguments   = arguments;
            Flags       = flags;
        }

        public Symbol(string name, SymbolFlags flags, params Symbol[] arguments)
        {
            Name        = name;
            Arguments   = arguments;
            Flags       = flags;
        }

        public string Domain { get; }

        public string Name { get; }
        
        // Declaration?

        public Symbol[] Arguments { get; }

        public SymbolFlags Flags { get; }

        public SymbolFlags SymbolKind => Flags & SymbolFlags.Types;

        #region Initializization

        public void Initialize(IType type)
        {
            ResolvedTyped = type;

            Status = SymbolStatus.Resolved;
        }

        public SymbolStatus Status { get; set; } = SymbolStatus.Unresolved;

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
           => new Symbol(name, SymbolFlags.Label);

        public static Symbol Argument(string name)
            => new Symbol(name, SymbolFlags.Argument);

        public static Symbol Variable(string name)
            => new Symbol(name, SymbolFlags.Variable);

        public static Symbol Type(string name)
         => new Symbol(name, SymbolFlags.Type);

        public static Symbol Type(string name, params Symbol[] arguments)
         => new Symbol(name, SymbolFlags.Type, arguments);

        public static implicit operator string(Symbol symbol)
            => symbol?.ToString();
    }

    public enum SymbolFlags
    {
        None        = 0,
        Variable    = 1 << 0,
        Argument    = 1 << 1, // function scope
        Member      = 1 << 2,
        Type        = 1 << 3,       
        Function    = 1 << 4,   // instance vs global?
        Module      = 1 << 5,
        Label       = 1 << 6,

        Types = Variable | Member | Type | Function | Module | Label,

        // Variables
        Property    = 1 << 7, // type member
  

        Operator    = 1 << 8,

        Infix       = 1 << 9,
        Postfix     = 1 << 10,

        // Variable Scope...
        BlockScoped = 1 << 11,
        Local       = 1 << 12
    }


    public enum SymbolStatus
    {
        Unresolved = 0,
        Resoliving = 1,
        Resolved   = 2
    }
}