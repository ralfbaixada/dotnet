using Bases.Bases;

namespace Entities
{
    public class OperatorRole : BaseEntity<int>
    {
        public int RoleId { get; set; }
        public int OperatorId { get; set; }
        public Operator Operator { get; set; }
    }
}
