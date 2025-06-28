using System;
using System.Buffers;
using System.IO;

namespace E.Parsing;

internal sealed class SourceReader
{
    private readonly string _text;

    private char _current;

    private int line = 1;
    private int column = 0;
    private int position = 0;

    public SourceReader(string text)
    {
        _text = text;
        _current = text.Length > 0 ? text[0] : '\0';
    }

    public char Peek()
    {
        if (position + 1 == _text.Length)
        {
            return '\0';
        }

        return _text[position + 1];
    }
    
    public int Line => line;

    public Location Location => new (line, column, position);

    public char Current => _current;
    
    public ReadOnlySpan<char> Consume(SearchValues<char> values)
    {
        var remaining = _text.AsSpan(position);

        int index = _text.AsSpan(position).IndexOfAnyExcept(values);

        var consumed = index is -1 ? remaining : remaining[..index];

        position += consumed.Length;
        column += consumed.Length;

        _current = position < _text.Length ? _text[position] : '\0';

        if (_current is '\n')
        {
            column = -1;

            line++;
        }

        return consumed;
    }

    public bool PeekConsumeIf(char value)
    {
        if (Peek() != value)
            return false;

        Consume();

        return true;
    }

    public string Consume(int count)
    {
        Span<char> result = count > 16
            ? stackalloc char[count]
            : new char[count];

        for (var i = 0; i < count; i++)
        {
            result[i] = Consume();
        }

        return new string(result);
    }

    public char Consume()
    {
        var c = _current;

        Advance();

        return c;
    }

    internal void Advance(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Advance();
        }
    }

    public void Advance()
    {
        if (IsEof)
        {
            throw new EndOfStreamException("Cannot read past EOF");
        }

        position++;

        if (position == _text.Length) // EOF
        {
            _current = '\0';

            return;
        }

        _current = _text[position];

        if (_current is '\n')
        {
            column = -1;

            line++;

            return;
        }

        column++;
    }

    public char Next()
    {
        Advance();

        return _current;
    }

    public bool IsEof => _current is '\0';
}