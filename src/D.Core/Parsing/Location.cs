using System;
using System.Globalization;

namespace E.Parsing;

public readonly struct Location(int line, int column, int position) : IEquatable<Location>
{
    public int Line { get; } = line;

    public int Column { get; } = column;

    public int Position { get; } = position;

    public override bool Equals(object? obj) => obj is Location other && Equals(other);

    public readonly bool Equals(Location other)
    {
        return Position == other.Position
            && Column == other.Column
            && Line == other.Line;
    }

    public readonly override int GetHashCode() => Position.GetHashCode();

    public static bool operator ==(Location left, Location right)
        => left.Equals(right);

    public static bool operator !=(Location left, Location right)
        => !left.Equals(right);

    public readonly override string ToString()
        => string.Create(CultureInfo.InvariantCulture, $"({Line},{Column},{Position})");
}
