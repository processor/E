using System;
using System.IO;
using System.Text;

namespace D
{
    using Expressions;

    public class FunctionExpression : INamedObject
    {
        public FunctionExpression(string name, IType returnType, params Parameter[] parameters)
        {
            Name = name;
            Parameters = parameters;
            ReturnType = returnType;
        }

        public FunctionExpression(
           Parameter[] parameters,
           IExpression body,
           FunctionFlags flags = FunctionFlags.None)
            : this(parameters, body, null, flags) { }

        public FunctionExpression(
           Parameter[] parameters,
           IExpression body,
           IType returnType,
           FunctionFlags flags = FunctionFlags.None)
        {
            Parameters = parameters;
            GenericParameters = Array.Empty<Parameter>();
            Body = body;
            ReturnType = returnType;
            Flags = flags;
        }

        public FunctionExpression(
            Symbol name,
            Parameter[] genericParameters,
            Parameter[] parameters,
            IType returnType,
            IExpression body,
            FunctionFlags flags = FunctionFlags.None)
        {
            Name = name;
            GenericParameters = genericParameters;
            Parameters = parameters;
            ReturnType = returnType;
            Body = body;
            Flags = flags;
        }
        
        public string Name { get; }

        public Parameter[] Parameters { get; }

        public IType ReturnType { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            {
                WriteTo(writer);    
            }

            return sb.ToString();
        }

        public virtual IObject Invoke(IArguments arguments)
        {
            return null;
        }

        #region Implementation

        public IType DeclaringType { get; set; }

        public Parameter[] GenericParameters { get; set; }

        // Block or lambda
        public IExpression Body { get; set; }

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

        public void WriteTo(TextWriter writer)
        {
            writer.Write("ƒ(");

            foreach (var parameter in Parameters)
            {
                writer.Write(parameter.Type);
            }

            writer.Write(")");

            writer.Write(Body.ToString());
        }

        Kind IObject.Kind => Kind.FunctionExpression;
    }
}


// a + 5    =  a → a + 5
// a + 5^2  =  a → a^2

// →
// f: X → Y 
// f(x) = 1/x

// ƒ(x, y) = 5x4

// ƒ(x, y) = x·y
