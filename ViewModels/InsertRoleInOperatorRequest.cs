using MediatR;

namespace ViewModels
{
    public class InsertRoleInOperatorRequest : IRequest<Unit>
    {
        public int RoleId { get; set; }
        public string Email { get; set; }
    }
}
