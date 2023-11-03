using MediatR;

namespace ViewModels
{
    public class CreateOperatorViewModel : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
