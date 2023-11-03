using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
    public class OperatorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }


        [JsonIgnore]
        public List<OperatorRoleViewModel> OperatorRoles { get; set; }
    }
}
