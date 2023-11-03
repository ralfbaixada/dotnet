using Bases.Bases;
using System;
using System.Collections.Generic;

namespace Bases.Entities
{
    public class Operator : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string CodeResetPassword { get; set; }
        public DateTime? CodeResetPasswordExpiration { get; set; }
        public bool AcceptNewPassword { get; set; }

        public virtual List<OperatorRole> OperatorRoles { get; set; }
    }
}
