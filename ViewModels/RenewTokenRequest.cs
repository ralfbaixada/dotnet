using MediatR;

namespace ViewModels
{
    public class RenewTokenRequest : IRequest<RenewTokenResponse>
    {
        public string Token { get; set; }
    }
}
