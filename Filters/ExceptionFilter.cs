using Exceptions;
using Extensions;
using Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Filters
{
    public sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly Resources _resources;

        public ExceptionFilter(ILogger<ExceptionFilter> logger, Resources resources)
        {
            _logger = logger;
            _resources = resources;
        }

        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ConflictException ex:
                    HandleConflictException(context, ex);
                    break;
                default:
                    break;
            }
        }

        private void HandleConflictException(ExceptionContext context, ConflictException ex)
        {
            _logger.LogInformation(ex, $"ConflictException: {ex.Message.FormatLogSize()}");

            var result = new ConflictObjectResult(ex.Message)
            {
                StatusCode = (int)HttpStatusCode.Conflict
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
