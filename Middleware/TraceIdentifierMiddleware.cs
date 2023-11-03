using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware
{
    public class TraceIdentifierMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceIdentifierMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var traceIdentifier = context.TraceIdentifier;

            context.Items["TraceIdentifier"] = traceIdentifier;
            await _next(context);
        }
    }
}
