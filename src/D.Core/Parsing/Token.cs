using System;

namespace E.Parsing;

public readonly struct Token(
    TokenKind kind,
    Location start,
    string? text = null,
    string? trailing = null)
{
    public readonly TokenKind Kind { get; } = kind;

    public readonly Location Start { get; } = start;

    // End (calculate)

    public readonly string Text { get; } = text!;

    public readonly string? Trailing { get; } = trailing;

    public readonly bool Is(TokenKind kind) => Kind == kind;

    public readonly override string ToString() => $"{Kind}:{Text}";

    public static implicit operator string(Token token) => token.Text!;

    public readonly bool Equals(string? value)
    {
        return string.Equals(Text, value, StringComparison.Ordinal);
    }
}