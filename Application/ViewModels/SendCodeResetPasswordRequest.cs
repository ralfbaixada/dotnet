using MediatR;

namespace Application.ViewModels
{
    public class SendCodeResetPasswordRequest : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
