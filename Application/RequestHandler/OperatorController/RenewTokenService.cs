using Application.ViewModels;
using AutoMapper;
using Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestHandles.OperatorController
{
    public class RenewTokenService : IRequestHandler<RenewTokenRequest, RenewTokenResponse>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public RenewTokenService(IMediator mediator, IConfiguration configuration, IMapper mapper)
        {
            _mediator = mediator;
            _configuration = configuration;
            _mapper = mapper;
        }


        public Task<RenewTokenResponse> Handle(RenewTokenRequest request, CancellationToken cancellationToken)
        {
            var refreshToken = TokenHelper.RenewJwtToken(request.Token, _configuration);

            var token = _mapper.Map<RenewTokenResponse>(refreshToken);

            return Task.FromResult(token);

        }
    }
}
