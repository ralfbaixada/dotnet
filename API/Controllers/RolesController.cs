using API.Controllers;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace

    Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : BaseApiController<RolesController>
    {
        public RolesController(ILogger<RolesController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpPost("InsertRoleInOperator")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> InsertRoleInOperator([FromBody] InsertRoleInOperatorRequest request) =>
                 await CreateActionResult(request);
    }
}
