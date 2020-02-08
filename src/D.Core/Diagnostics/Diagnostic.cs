namespace D.Diagnostics
{
    public sealed class Diagnostic
    {
        public Diagnostic(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
