using MediatR;

namespace Application.ViewModels
{
    public class RenewTokenRequest : IRequest<RenewTokenResponse>
    {
        public string Token { get; set; }
    }
}
