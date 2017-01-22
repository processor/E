using System;
using System.IO;
using System.Text;

namespace D.Expressions
{
    public class FunctionDeclaration : IExpression
    {
        public FunctionDeclaration(
            ParameterExpression[] parameters,
            IExpression body,
            FunctionFlags flags = FunctionFlags.None)
            : this(parameters, body, null, flags) { }

        public FunctionDeclaration(
           ParameterExpression[] parameters,
           IExpression body,
           Symbol returnType,
           FunctionFlags flags = FunctionFlags.None)
        {
            Parameters = parameters;
            GenericParameters = Array.Empty<ParameterExpression>();
            Body = body;
            ReturnType = returnType;
            Flags = flags;
        }

        public FunctionDeclaration(
            Symbol name, 
            ParameterExpression[] genericParameters,
            ParameterExpression[] parameters,
            Symbol returnType,
            IExpression body,
            FunctionFlags flags = FunctionFlags.None)
        {
            Name              = name;
            GenericParameters = genericParameters;
            Parameters        = parameters;
            ReturnType        = returnType;
            Body              = body;
            Flags             = flags;
        }

        public Symbol Name { get; }

        public ParameterExpression[] GenericParameters { get; }

        public ParameterExpression[] Parameters { get; }

        public Symbol ReturnType { get; }

        // Class Or Interface

        public IExpression DeclaringType { get; }

        // Block or lambda
        public IExpression Body { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            {
                WriteTo(writer);    
            }

            return sb.ToString();
        }


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

        #region Flags

        public FunctionFlags Flags { get; }

        // Mutating?

        public bool IsStatic
            => IsOperator || !Flags.HasFlag(FunctionFlags.Instance);

        public bool IsAbstract      => Flags.HasFlag(FunctionFlags.Abstract);
        public bool IsOperator      => Flags.HasFlag(FunctionFlags.Operator);
        public bool IsAnonymous     => Flags.HasFlag(FunctionFlags.Anonymous);
        public bool IsInitializer   => Flags.HasFlag(FunctionFlags.Initializer);
        public bool IsProperty      => Flags.HasFlag(FunctionFlags.Property);
        public bool IsIndexer       => Flags.HasFlag(FunctionFlags.Indexer);
        public bool IsConverter     => Flags.HasFlag(FunctionFlags.Converter);

        #endregion

        Kind IObject.Kind => Kind.FunctionDeclaration;
    }

    public enum FunctionFlags
    {
        None        = 0,
        Abstract    = 1 << 1,
        Instance    = 1 << 2,
        Operator    = 1 << 3,
        Anonymous   = 1 << 4,  // a => a + 1
        Initializer = 1 << 5,
        Converter   = 1 << 6,
        Indexer     = 1 << 7,
        Property    = 1 << 8

        // Lambda = 1 << 3
    }
}

// a + 5    =  a → a + 5
// a + 5^2  =  a → a^2

// →
// f: X → Y 
// f(x) = 1/x

// ƒ(x, y) = 5x4

// ƒ(x, y) = x·y