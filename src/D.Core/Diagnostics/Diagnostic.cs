namespace E.Diagnostics;

public sealed class Diagnostic(string message)
{
    public string Message { get; } = message;
}