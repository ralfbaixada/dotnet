using MediatR;

namespace Application.ViewModels
{
    public class ResetPasswordRequest : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
