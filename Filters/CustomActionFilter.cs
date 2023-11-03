using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Filters
{
    public sealed class CustomActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                if (context.Result is BadRequestObjectResult result)
                    result.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
