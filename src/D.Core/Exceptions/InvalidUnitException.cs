using System;

namespace E.Exceptions;

public sealed class InvalidUnitException : Exception
{
    public InvalidUnitException(ReadOnlySpan<char> unitName)
        : base($"Invalid unit. Was '{unitName}'") { }
}