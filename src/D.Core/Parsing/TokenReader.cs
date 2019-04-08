using System;
using System.IO;

namespace D.Parsing
{
    internal class TokenReader : IDisposable
    {
        private readonly Tokenizer tokenizer;

        public TokenReader(Tokenizer tokenizer)
        {
            this.tokenizer = tokenizer;

            Current = tokenizer.Next();
        }

        public int Line => Current.Start.Line;

        public Token Current { get; set; }

        public Token Read(TokenKind kind)
        {
            if (Current.Kind != kind)
            {
                throw new Exception($"Expected {kind}. Was {Current.Kind}");
            }

            return Next();
        }

        public Token Consume()
        {
            var c = Current;

            Next();

            return c;
        }

        public bool ConsumeIf(TokenKind kind)
        {
            if (Current.Kind == kind)
            {
                Next();

                return true;
            }

            return false;
        }

        public Token Consume(string text)
        {
            if (Current.Text != text)
            {
                throw new UnexpectedTokenException($"Expected {text}. Was {Current} @{Current.Start.Line}");
            }

            var c = Current;

            Next();

            return c;
        }

        public Token Consume(TokenKind kind)
        {
            if (Current.Kind != kind)
            {
                throw new UnexpectedTokenException($"Expected {kind}. Was {Current} @{Current.Start.Line}:{Current.Start.Column}");
            }

            var c = Current;

            Next();

            return c;
        }

        public bool IsEof => Current.Kind == TokenKind.EOF;

        public Token Next()
        {
            if (IsEof) throw new EndOfStreamException("Cannot read past EOF");

            Current = tokenizer.Next();

            return Current;
        }

        public void Dispose()
        {
            tokenizer.Dispose();
        }
    }
}