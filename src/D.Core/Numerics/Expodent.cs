using System;
using System.Collections.Generic;
using System.Text;

namespace E;

public readonly struct Superscript
{
    private static readonly char[] digits = { '⁰', '¹', '²', '³', '⁴', '⁵', '⁶', '⁷', '⁸', '⁹' };

    public Superscript(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static int Parse(ReadOnlySpan<char> text)
    {
        int value = 0;
        int multiplier = 1;

        for (int i = text.Length - 1; i >= 0; i--)
        {
            char digit = text[i];

            int number = Array.IndexOf(digits, digit);

            value += number * multiplier;

            multiplier *= 10;
        }

        return value;
    }

    public override string ToString()
    {
        if (Value < 10)
        {
            return GetChar(Value).ToString();
        }

        var sb = new Stack<char>();

        int v = Value;
        int r;

        while (v > 0)
        {
            r = v % 10;

            v -= r;
            v /= 10;

            sb.Push(GetChar(r));
        }

        return new string(sb.ToArray());
    }

    internal void WriteTo(ref ValueStringBuilder output)
    {
        if (Value < 10)
        {
            output.Append(GetChar(Value));

            return;
        }

        var sb = new Stack<char>();

        int v = Value;
        int r;

        while (v > 0)
        {
            r = v % 10;

            v -= r;
            v /= 10;

            sb.Push(GetChar(r));
        }

        output.Append(sb.ToArray());
    }


    public static char GetChar(int exponent) => exponent >= 0 && exponent <= 9
        ? digits[exponent]
        : throw new ArgumentOutOfRangeException(nameof(exponent), exponent, "Must be >= 0 && <= 9");

    // TODO: divide by 100 and append if greater then 10
}
