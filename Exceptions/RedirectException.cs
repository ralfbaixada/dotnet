using System;

namespace Exceptions
{
    public class RedirectException : Exception
    {
        public RedirectException(string url, Exception exception = default) : base(url, exception) { }

        public static void Throw(string url, Exception innerException = default)
        {
            throw new RedirectException(url, innerException);
        }
    }
}
