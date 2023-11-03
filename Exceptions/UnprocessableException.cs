using System;

namespace Exceptions
{
    public class UnprocessableException : Exception
    {
        public UnprocessableException(string message, Exception exception = default) : base(message, exception) { }
    }
}
