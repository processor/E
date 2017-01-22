using D.Expressions;

namespace D
{
    public class Function : INamedObject
    {
        public Function(string name, IType returnType, params Parameter[] parameters)
        {
            Name = name;
            Parameters = parameters;
            ReturnType = returnType;
        }

        public string Name { get; }

        public Parameter[] Parameters { get; }

        public IType ReturnType { get; }

        public virtual IObject Invoke(IArguments arguments)
        {
            return null;
        }

        #region Implementation

        public IType DeclaringType { get; set; }

        public Parameter[] GenericParameters { get; set; }

        public BlockStatement Body { get; set; }

        public FunctionFlags Flags { get; set; }

        #endregion

        #region Flags

        public bool IsStatic
           => IsOperator || !Flags.HasFlag(FunctionFlags.Instance);

        public bool IsAbstract
            => Flags.HasFlag(FunctionFlags.Abstract);

        public bool IsOperator
            => Flags.HasFlag(FunctionFlags.Operator);

        public bool IsAnonymous
            => Flags.HasFlag(FunctionFlags.Anonymous);

        public bool IsInitializer
            => Flags.HasFlag(FunctionFlags.Initializer);

        public bool IsProperty
            => Flags.HasFlag(FunctionFlags.Property);

        public bool IsIndexer
            => Flags.HasFlag(FunctionFlags.Indexer);

        public bool IsConverter
            => Flags.HasFlag(FunctionFlags.Converter);

        #endregion

        // Body

        Kind IObject.Kind => Kind.Function;
    }
}
