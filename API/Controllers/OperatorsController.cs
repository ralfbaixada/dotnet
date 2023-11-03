using Application.ViewModels;
using Exceptions;
using Helpers;
using Localization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Enum;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperatorsController : BaseApiController<OperatorsController>
    {
        private readonly Resources _resources;
        public OperatorsController(ILogger<OperatorsController> logger, IMediator mediator, Resources resources) : base(logger, mediator)
        {
            _resources = resources;
        }

        [Authorize]
        [HttpGet("getAllOperators")]
        [ProducesResponseType(typeof(List<GetAllOperatorsResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllOperators()
        {
            if (!RoleHelper.CheckRole(((int)EnumRoles.Admin).ToString(), User))
            //if (!RoleHelper.CheckRole((5).ToString(), User))
            {
                _logger.LogError(_resources.InvalidRole());
                throw new ConflictException(_resources.InvalidRole());
            }
            return await CreateActionResult(new GetAllOperatorsRequest());
        }

        [HttpPost("CreateOperator")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOperator([FromBody] CreateOperatorViewModel request) =>
                 await CreateActionResult(request);

        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginOperatorResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] LoginOperatorRequest request) =>
                 await CreateActionResult(request);

        [HttpPost("SendCodeResetPassword")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendCodeResetPassword([FromBody] SendCodeResetPasswordRequest request) =>
                 await CreateActionResult(request);

        [HttpPost("CheckCode")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CheckCode([FromBody] CheckCodeRequest request) =>
                 await CreateActionResult(request);

        [HttpPost("ResetPassword")]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request) =>
                 await CreateActionResult(request);

        [HttpPost("RenewToken")]
        [ProducesResponseType(typeof(RenewTokenResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RenewToken([FromBody] RenewTokenRequest request) =>
                 await CreateActionResult(request);
    }
}
