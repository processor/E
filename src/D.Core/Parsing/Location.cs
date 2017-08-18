using System;

namespace D.Parsing
{
    public struct Location : IEquatable<Location>
    {
        public Location(int line, int column, int position)
        {
            Line = line;
            Column = column;
            Position = position;
        }

        public int Line { get; }

        public int Column { get; }

        public int Position { get; }

        public override bool Equals(object obj) => Equals((Location)obj);

        public bool Equals(Location other)
        {
            return Position == other.Position
                && Column == other.Column
                && Line == other.Line;
        }

        public static bool operator == (Location left, Location right)
            => left.Equals(right);

        public static bool operator != (Location left, Location right)
            => !left.Equals(right);

        public override string ToString()
            => "(" + Line + "," + Column + "," + Position + ")";

        public override int GetHashCode() => Position.GetHashCode();
    }
}
