using System;

namespace Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message, Exception exception = default) : base(message, exception) { }
    }
}
