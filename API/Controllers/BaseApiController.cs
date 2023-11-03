using Bases.Bases;
using MediatR;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public abstract class BaseApiController<TControler> : BaseController<TControler>
    {
        protected BaseApiController(ILogger<TControler> logger, IMediator mediator) : base(logger, mediator) { }
    }
}
