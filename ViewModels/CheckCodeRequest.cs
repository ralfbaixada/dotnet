using MediatR;

namespace ViewModels
{
    public class CheckCodeRequest : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
