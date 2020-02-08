using System;
using System.IO;

namespace D.Parsing
{
    internal class SourceReader : IDisposable
    {
        private readonly TextReader reader;

        private char current;

        private int line = 1;
        private int column = 0;
        private int position = 0;

        public SourceReader(string text)
        {
            this.reader = new StringReader(text);
        }

        public SourceReader(TextReader reader)
        {
            this.reader = reader;
        }

        public int Line => line;

        public Location Location => 
            new Location(line, column - 1, position - 1);

        public char Current => current;

        public char Peek() => (char)reader.Peek();

        public bool PeekConsumeIf(char value)
        {
            if (reader.Peek() != value)
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

            var value = reader.Read();

            if (value == -1)
            {
                IsEof = true;

                return '\0';
            }

            current = (char)value;

            if (current == '\n')
            {
                column = -1;
                line++;
            }

            column++;
            position++;

            return current;
        }

        public void Dispose()
        {
            reader.Dispose();
        }

        public bool IsEof { get; private set; }
    }
}