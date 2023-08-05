using System;
using System.IO;

namespace E.Parsing;

internal struct TokenReader
{
    private readonly Tokenizer _tokenizer;

    public TokenReader(Tokenizer tokenizer)
    {
        _tokenizer = tokenizer;

        Current = tokenizer.Next();
    }

    public readonly int Line => Current.Start.Line;

    public Token Current { get; set; }

    public Token Read(TokenKind kind)
    {
        if (Current.Kind != kind)
        {
            throw new Exception($"Expected {kind}. Was {Current.Kind}");
        }

        Advance();

        return Current;
    }

    public Token Consume()
    {
        Token c = Current;

        Advance();

        return c;
    }

    public bool ConsumeIf(TokenKind kind)
    {
        if (Current.Kind == kind)
        {
            Advance();

            return true;
        }

        return false;
    }

    public Token Consume(string text)
    {
        if (!Current.Text.Equals(text, StringComparison.Ordinal))
        {
            throw new UnexpectedTokenException($"Expected {text}. Was {Current} @{Current.Start.Line}");
        }

        var c = Current;

        Advance();

        return c;
    }

    public Token Consume(TokenKind kind)
    {
        if (Current.Kind != kind)
        {
            throw new UnexpectedTokenException($"Expected {kind}. Was {Current} @{Current.Start.Line}:{Current.Start.Column}");
        }

        var c = Current;

        Advance();

        return c;
    }

    public readonly bool IsEof => Current.Kind is TokenKind.EOF;

    public void Advance()
    {
        if (IsEof) throw new EndOfStreamException("Cannot read past EOF");

        Current = _tokenizer.Next();
    }
}
