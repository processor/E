using System;

namespace D.Parsing
{
    internal class UnexpectedTokenException : Exception
    {
        public UnexpectedTokenException(string message, Token token)
           : base(message + $". Was {token} @ {token.Start.Line}")
        {
        }

        public UnexpectedTokenException(string message) 
            : base(message)
        {
        }

        public UnexpectedTokenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}