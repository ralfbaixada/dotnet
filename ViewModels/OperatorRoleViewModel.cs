namespace ViewModels
{
    public class OperatorRoleViewModel
    {
        public int OperatorId { get; set; }
        public OperatorViewModel Operator { get; set; }

        public int RoleId { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
