using MediatR;

namespace Application.ViewModels
{
    public class LoginOperatorRequest : IRequest<LoginOperatorResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
