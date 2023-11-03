using Extensions;
using Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;


namespace Bases.Bases
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public abstract class BaseController<TControler> : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly ILogger<TControler> _logger;

        protected BaseController(ILogger<TControler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected async Task<IActionResult> CreateActionResult<T>(T model)
        {
            try
            {
                var result = await _mediator.Send(model);
                if (result is FileContentResult fileStream)
                    return fileStream;

                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                _logger.LogInformation(ex, $"BadRequestException: {JsonSerializer.Serialize(ex.BadRequestObjectResult).FormatLogSize()} on CreateActionResult model: {JsonSerializer.Serialize(model).FormatLogSize()}");
                return new BadRequestObjectResult(GetErrorResult(ex));
            }
            catch (PreconditionFailedException ex)
            {
                _logger.LogInformation(ex, $"PreconditionFailedException: {JsonSerializer.Serialize(ex.BadRequestObjectResult).FormatLogSize()} on CreateActionResult model: {JsonSerializer.Serialize(model).FormatLogSize()}");
                return ex.BadRequestObjectResult;
            }
            catch (ConflictException ex)
            {
                _logger.LogInformation(ex, $"ConflictException: {ex.Message.FormatLogSize()} on CreateActionResult model: {JsonSerializer.Serialize(model).FormatLogSize()}");
                return new ConflictObjectResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
            }
            catch (UnprocessableException ex)
            {
                _logger.LogInformation(ex, $"ConflictException: {ex.Message.FormatLogSize()} on CreateActionResult model: {JsonSerializer.Serialize(model).FormatLogSize()}");
                return new ObjectResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.UnprocessableEntity
                };
            }
            catch (RedirectException ex)
            {
                _logger.LogInformation(ex, $"RedirectException: {ex.Message.FormatLogSize()} on CreateActionResult model: {JsonSerializer.Serialize(model).FormatLogSize()}");
                return new RedirectResult(ex.Message, false);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogInformation(ex, $"UnauthorizedAccessException: {ex.Message.FormatLogSize()} on CreateActionResult model: {JsonSerializer.Serialize(model).FormatLogSize()}");
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception: {ex.Message.FormatLogSize()} on CreateActionResult model: {JsonSerializer.Serialize(model).FormatLogSize()}");
                return new ObjectResult(GetErrorResult(ex))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        private string GetErrorResult(Exception Exception)
        {
            var result = $"{{\"requestId\":\"{HttpContext.TraceIdentifier}\"";
            if (HttpContext.Request.Host.Host.Contains("localhost") || HttpContext.Request.Host.Host.Contains("service-hml"))
                result += $",\"Exception\":{GetDataFromException(Exception)}";
            result += "}";
            return result;
        }

        private string GetDataFromException(Exception Exception)
        {
            if (Exception == null)
                return "null";
            return $"{{\"message\":\"{Exception.Message.FormatLogSize()}\",\"stackTrace\":\"{Exception.StackTrace.FormatLogSize()}\",\"innerException\":{GetDataFromException(Exception.InnerException)}}}";
        }
    }
}
