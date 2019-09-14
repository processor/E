using System;
using System.Text;

namespace D
{
    using Expressions;
    using Syntax;

    public abstract class Symbol : IExpression, ISyntaxNode
    {
        public Symbol(string name, SymbolFlags flags = SymbolFlags.None)
        {
            Name = name;
            Flags = flags;
            Arguments = Array.Empty<ArgumentSymbol>();
        }

        public Symbol(string name, Symbol[] arguments, SymbolFlags flags = SymbolFlags.None)
        {
            Name = name;
            Arguments = arguments;
            Flags = flags;
        }

        public Symbol(
            ModuleSymbol? module,
            string name,
            Symbol[] arguments,
            SymbolFlags flags = SymbolFlags.None)
        {
            Module = module;
            Name = name;
            Arguments = arguments;
            Flags = flags;
        }

        public ModuleSymbol? Module { get; }

        public string Name { get; }

        public Symbol[]? Arguments { get; }

        public SymbolFlags Flags { get; }

        // TODO: Scope

        // Constructor + Self

        #region Initializization / Binding

        public SymbolStatus Status { get; protected set; } = SymbolStatus.Unresolved;
        
        public Symbol? ContainingType { get; set; } // if a member of a type

        public Symbol? ContainingModule { get; set; } // if a member of a module

        #endregion

        ObjectType IObject.Kind => ObjectType.Symbol;

        public override string ToString()
        {
            if (Module is null && (Arguments == null || Arguments.Length == 0))
            {
                return Name;
            }

            var sb = new StringBuilder();

            if (Module != null)
            {
                sb.Append(Module);
                sb.Append("::");
            }

            sb.Append(Name);

            if (Arguments != null && Arguments.Length > 0)
            {
                sb.Append('<');

                var i = 0;

                foreach (var arg in Arguments)
                {
                    if (++i > 1)
                    {
                        sb.Append(',');
                    }

                    sb.Append(arg.ToString());
                }

                sb.Append('>');
            }

            return sb.ToString();
        }


        public virtual bool TryGetValue(string name, out Symbol? value)
        {
            value = default;

            return false;
        }

        public virtual void Add(Symbol child)
        {
            throw new NotImplementedException();
        }

        public static LabelSymbol Label(string name) =>
            new LabelSymbol(name);

        public static VariableSymbol Variable(string name) =>
            new VariableSymbol(name);

        public static Symbol Argument(string name) =>
            new ArgumentSymbol(name);

        public static TypeSymbol Type(string name) => new TypeSymbol(name);

        public static Symbol Type(string name, params Symbol[] arguments) =>
            new TypeSymbol(name, arguments);

        public static implicit operator string(Symbol symbol) => symbol?.ToString();

        SyntaxKind ISyntaxNode.Kind => SyntaxKind.Symbol;
    }
}

// Symbols include:
// - Types: Object, Decimal, Array<string>
// - Parameter Names
// - Property Names
// - Function Names
// - Variable Names (locals)

// Symbol scopes
// - Immediate block
// - Module