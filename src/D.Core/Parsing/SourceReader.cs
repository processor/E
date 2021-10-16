using System.IO;

namespace E.Parsing;

internal class SourceReader
{
    private readonly string text;

    private char current;

    private int line = 1;
    private int column = 0;
    private int position = 0;

    public SourceReader(string text)
    {
        this.text = text;
        this.current = text[0];
    }

    public char Peek()
    {
        if (position + 1 == text.Length)
        {
            return '\0';
        }

        return text[position + 1];
    }

    
    public int Line => line;

    public Location Location => new (line, column, position);

    public char Current => current;

    public bool PeekConsumeIf(char value)
    {
        if (Peek() != value)
            return false;

        Consume();

        return true;
    }

    public string Consume(int count)
    {
        var result = new char[count];

        for (var i = 0; i < count; i++)
        {
            result[i] = Consume();
        }

        return new string(result, 0, count);
    }

    public char Consume()
    {
        var c = current;

        Next();

        return c;
    }

    public char Next()
    {
        if (IsEof)
        {
            throw new EndOfStreamException("Cannot read past EOF");
        }

        if ((position + 1) == text.Length)
        {
            IsEof = true;

            return '\0';
        }

        current = text[position + 1];

        if (current == '\n')
        {
            column = -2;

            line++;
        }

        column++;
        position++;

        return current;
    }

    public bool IsEof { get; private set; }
}