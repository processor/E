using System;
using System.IO;
using System.Text;

namespace D.Syntax
{
    public class FunctionDeclarationSyntax : ISyntaxNode, SyntaxNode
    {
        // TODO: Module

        public FunctionDeclarationSyntax(
            ParameterSyntax[] parameters,
            SyntaxNode body,
            ObjectFlags flags = ObjectFlags.None)
            : this(parameters, body, null, flags) { }

        public FunctionDeclarationSyntax(
           ParameterSyntax[] parameters,
           SyntaxNode body,
           Symbol returnType,
           ObjectFlags flags = ObjectFlags.None)
        {
            Parameters        = parameters;
            GenericParameters = Array.Empty<ParameterSyntax>();
            Body              = body;
            ReturnType        = returnType;
            Flags             = flags;
        }

        public FunctionDeclarationSyntax(
            Symbol name, 
            ParameterSyntax[] genericParameters,
            ParameterSyntax[] parameters,
            Symbol returnType,
            SyntaxNode body,
            ObjectFlags flags = ObjectFlags.None)
        {
            Name              = name;
            GenericParameters = genericParameters;
            Parameters        = parameters;
            ReturnType        = returnType;
            Body              = body;
            Flags             = flags;
        }

        public Symbol Name { get; }

        public ParameterSyntax[] GenericParameters { get; }

        public ParameterSyntax[] Parameters { get; }

        public Symbol ReturnType { get; }

        // Class Or Interface

        public SyntaxNode DeclaringType { get; internal set; }

        // Block or lambda
        public SyntaxNode Body { get; }

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

        public ObjectFlags Flags { get; }

        // Mutating?

        public bool IsStatic
            => IsOperator || !Flags.HasFlag(ObjectFlags.Instance);

        public bool IsAbstract      => Flags.HasFlag(ObjectFlags.Abstract);
        public bool IsOperator      => Flags.HasFlag(ObjectFlags.Operator);
        public bool IsAnonymous     => Flags.HasFlag(ObjectFlags.Anonymous);
        public bool IsInitializer   => Flags.HasFlag(ObjectFlags.Initializer);
        public bool IsProperty      => Flags.HasFlag(ObjectFlags.Property);
        public bool IsIndexer       => Flags.HasFlag(ObjectFlags.Indexer);
        public bool IsConverter     => Flags.HasFlag(ObjectFlags.Converter);

        #endregion


        #region Helpers

        TypeSymbol ISyntaxNode.Type => new TypeSymbol("Function", GetParameterTypeSymbols(this.Parameters));

        private static Symbol[] GetParameterTypeSymbols(ParameterSyntax[] parameters)
        {
            var typeSymbols = new Symbol[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                typeSymbols[i] = parameters[i].Type;
            }

            return typeSymbols;
        }


        #endregion

        Kind IObject.Kind => Kind.FunctionDeclaration;
    }
}

// a + 5    =  a → a + 5
// a + 5^2  =  a → a^2

// →
// f: X → Y 
// f(x) = 1/x

// ƒ(x, y) = 5x4

// ƒ(x, y) = x·y