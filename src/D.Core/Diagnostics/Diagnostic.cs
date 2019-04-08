namespace D.Diagnostics
{
    public class Diagnostic
    {
        public Diagnostic(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
